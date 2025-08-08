using System.Collections.Generic;
using System.ServiceProcess;
using System.Timers;
using DataConnector.Core;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Collections.Concurrent; // 1. 导入并发集合命名空间

namespace DataConnectorService
{
    public partial class Service1 : ServiceBase
    {
        private readonly List<Timer> _taskTimers = new List<Timer>();
        private readonly ConfigManager _configManager;
        private readonly SyncService _syncService;
        private readonly string _logFilePath;

        // --- ↓↓↓ 核心修改：创建一个线程安全的字典来跟踪正在运行的任务 ↓↓↓ ---
        private readonly ConcurrentDictionary<string, bool> _runningTasks = new ConcurrentDictionary<string, bool>();
        // --- ↑↑↑ 修改结束 ↑↑↑ ---

        public Service1()
        {
            InitializeComponent();
            this.ServiceName = "DataConnectorService";
            _configManager = new ConfigManager();
            _syncService = new SyncService();

            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            _logFilePath = Path.Combine(exePath, "service_log.txt");
        }

        protected override void OnStart(string[] args)
        {
            Log("服务正在启动...");
            try
            {
                var appConfig = _configManager.Load(Log);

                if (appConfig.Tasks == null || appConfig.Tasks.Count == 0)
                {
                    Log("警告：在 config.json 中没有找到任何要调度的任务。");
                    return;
                }

                foreach (var task in appConfig.Tasks)
                {
                    if (task.SyncIntervalMinutes > 0)
                    {
                        var timer = new Timer(task.SyncIntervalMinutes * 60 * 1000);
                        timer.Elapsed += (sender, e) => OnTimerElapsed(task);
                        timer.AutoReset = true;
                        timer.Enabled = true;
                        _taskTimers.Add(timer);
                        Log($"已为任务 '{task.TaskName}' 创建定时器，执行周期: {task.SyncIntervalMinutes} 分钟。");
                    }
                    else
                    {
                        Log($"任务 '{task.TaskName}' 的同步周期为0，将不会被自动调度。");
                    }
                }

                Log("服务启动成功，所有任务已开始调度。");
            }
            catch (Exception ex)
            {
                Log($"服务启动时发生严重错误: {ex.Message}");
                this.Stop();
            }
        }

        private void OnTimerElapsed(TaskConfig task)
        {
            // --- ↓↓↓ 核心修改：在执行任务前检查“任务锁” ↓↓↓ ---
            // 2. 尝试将任务标记为“正在运行”。如果标记成功，则执行；如果失败，说明已有实例在运行，则跳过。
            if (!_runningTasks.TryAdd(task.TaskName, true))
            {
                Log($"任务 '{task.TaskName}' 的上一个实例仍在运行中，本次调度已跳过。");
                return;
            }
            // --- ↑↑↑ 修改结束 ↑↑↑ ---

            try
            {
                Log($"定时器触发，开始执行任务: '{task.TaskName}'...");

                Task.Run(async () => {
                    try
                    {
                        bool configChanged = await _syncService.RunTaskAsync(task, Log);
                        if (configChanged)
                        {
                            var currentConfig = _configManager.Load(Log);
                            _configManager.SaveSilently(currentConfig, Log);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"执行任务 '{task.TaskName}' 时发生线程内错误: {ex.Message}");
                    }
                    finally
                    {
                        // --- ↓↓↓ 核心修改：在任务完成后，无论成功或失败，都释放“任务锁” ↓↓↓ ---
                        _runningTasks.TryRemove(task.TaskName, out _);
                        Log($"任务 '{task.TaskName}' 执行完毕，已释放锁定。");
                        // --- ↑↑↑ 修改结束 ↑↑↑ ---
                    }
                });
            }
            catch (Exception ex)
            {
                Log($"启动任务 '{task.TaskName}' 时发生严重错误: {ex.Message}");
                // 确保即使启动失败也释放锁
                _runningTasks.TryRemove(task.TaskName, out _);
            }
        }

        protected override void OnStop()
        {
            foreach (var timer in _taskTimers)
            {
                timer.Stop();
                timer.Dispose();
            }
            _taskTimers.Clear();
            Log("服务已停止。");
        }

        private void Log(string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);

                if (!System.Diagnostics.EventLog.SourceExists("DataConnectorServiceSource"))
                {
                    System.Diagnostics.EventLog.CreateEventSource("DataConnectorServiceSource", "DataConnectorServiceLog");
                }
                System.Diagnostics.EventLog.WriteEntry("DataConnectorServiceSource", message);
            }
            catch (Exception)
            {
                // 忽略日志写入错误
            }
        }
    }
}
