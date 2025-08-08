using DataConnector.Core;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace DataConnector
{
    public partial class Form1 : Form
    {
        private AppConfig _appConfig = new AppConfig();
        private bool _isProgrammaticChange = false;
        private readonly ConfigManager _configManager = new ConfigManager();
        private readonly SyncService _syncService = new SyncService();

        private readonly List<string> _supportedDataTypes = new List<string>
        {
            "商品信息", "门店信息", "供应商信息", "采购记录",
            "销售记录", "库存快照", "会员信息", "职员信息"
        };

        private readonly Dictionary<SyncMode, string> _syncModeTranslations = new Dictionary<SyncMode, string>
        {
            { SyncMode.MergeUpdate, "合并更新" },
            { SyncMode.AppendOnly, "仅追加" },
            { SyncMode.FullSnapshot, "全量快照" }
        };

        public Form1()
        {
            InitializeComponent();
            InitializeDataBinding();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void InitializeDataBinding()
        {
            taskListbox.DataSource = _appConfig.Tasks;
            taskListbox.DisplayMember = "TaskName";

            foreach (var val in _syncModeTranslations.Values)
            {
                syncModeComboBox.Items.Add(val);
            }

            // 订阅所有控件的事件
            taskListbox.SelectedIndexChanged += TaskListbox_SelectedIndexChanged;
            dbTypeComboBox.SelectedIndexChanged += OnControlValueChanged;
            connectionStringTextBox.TextChanged += OnControlValueChanged;
            apiEndpointTextBox.TextChanged += OnControlValueChanged;
            apiKeyTextBox.TextChanged += OnControlValueChanged;
            sqlQueryTextBox.TextChanged += OnControlValueChanged;
            dataTypeComboBox.DataSource = _supportedDataTypes;
            dataTypeComboBox.SelectedIndexChanged += OnControlValueChanged;
            syncModeComboBox.SelectedIndexChanged += OnControlValueChanged;
            incrementalFieldTextBox.TextChanged += OnControlValueChanged;
            forceFullSyncCheckBox.CheckedChanged += OnControlValueChanged;
            syncIntervalNumericUpDown.ValueChanged += OnControlValueChanged;

            btnAddTask.Click += BtnAddTask_Click;
            btnDeleteTask.Click += BtnDeleteTask_Click;
            btnSaveConfig.Click += BtnSaveConfig_Click;
            btnTestConnection.Click += BtnTestConnection_Click;
            btnRunTask.Click += BtnRunTask_Click;

            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            showMenuItem.Click += ShowMenuItem_Click;
            exitMenuItem.Click += ExitMenuItem_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _appConfig = _configManager.Load(Log);
            taskListbox.DataSource = _appConfig.Tasks;
            this.BeginInvoke(new MethodInvoker(this.Hide));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                notifyIcon.ShowBalloonTip(1000, "提示", "程序已最小化到系统托盘", ToolTipIcon.Info);
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowMainFormWithPassword();
        }

        private void ShowMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainFormWithPassword();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (VerifyPassword("退出程序需要验证密码"))
            {
                Application.Exit();
            }
        }

        private void ShowMainFormWithPassword()
        {
            if (this.Visible)
            {
                this.Activate();
                return;
            }

            if (VerifyPassword("显示配置窗口需要验证密码"))
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private bool VerifyPassword(string prompt)
        {
            using (var passwordForm = new PasswordForm())
            {
                passwordForm.Text = prompt;
                if (passwordForm.ShowDialog(this) == DialogResult.OK)
                {
                    if (passwordForm.Password == _appConfig.VerificationPassword)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("密码错误！", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return false;
            }
        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            string taskName = Interaction.InputBox("请输入新任务的名称:", "新增任务", $"新任务 {_appConfig.Tasks.Count + 1}");
            if (!string.IsNullOrWhiteSpace(taskName))
            {
                TaskConfig newTask = new TaskConfig { TaskName = taskName };
                _appConfig.Tasks.Add(newTask);
                taskListbox.SelectedItem = newTask;
            }
        }

        private void BtnSaveConfig_Click(object sender, EventArgs e)
        {
            if (_configManager.Save(_appConfig, Log))
            {
                MessageBox.Show("所有配置已成功保存！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存配置文件时出错，请查看日志获取详细信息。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnTestConnection_Click(object sender, EventArgs e)
        {
            var currentTask = new TaskConfig
            {
                SourceDbType = dbTypeComboBox.SelectedItem as string,
                SourceConnectionString = connectionStringTextBox.Text
            };

            if (string.IsNullOrEmpty(currentTask.SourceDbType) || string.IsNullOrEmpty(currentTask.SourceConnectionString))
            {
                MessageBox.Show("请先选择数据库类型并填写连接字符串。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetButtonsEnabled(false);
            bool isSuccess = await _syncService.TestConnectionAsync(currentTask, Log);
            SetButtonsEnabled(true);

            if (isSuccess)
            {
                MessageBox.Show("数据库连接成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("数据库连接失败，请查看日志获取详细信息。", "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnRunTask_Click(object sender, EventArgs e)
        {
            if (taskListbox.SelectedItem is TaskConfig selectedTask)
            {
                SetButtonsEnabled(false);
                bool configWasChanged = false;

                try
                {
                    configWasChanged = await _syncService.RunTaskAsync(selectedTask, Log);
                    MessageBox.Show("数据同步任务成功完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"执行任务时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SetButtonsEnabled(true);
                }

                if (configWasChanged)
                {
                    Log("检测到配置已更改，将自动保存。");
                    _configManager.SaveSilently(_appConfig, Log);
                    LoadTaskDetails(selectedTask);
                }
            }
            else
            {
                MessageBox.Show("请先在左侧列表中选择一个要执行的任务。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TaskListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTaskDetails(taskListbox.SelectedItem as TaskConfig);
        }

        private void LoadTaskDetails(TaskConfig task)
        {
            _isProgrammaticChange = true;
            if (task != null)
            {
                // 加载基础配置
                dbTypeComboBox.SelectedItem = task.SourceDbType;
                dataTypeComboBox.SelectedItem = task.DataType;
                connectionStringTextBox.Text = task.SourceConnectionString;
                apiEndpointTextBox.Text = task.DestinationApiEndpoint;
                apiKeyTextBox.Text = task.DestinationApiKey;
                sqlQueryTextBox.Text = task.ExtractionSql;

                // 加载高级配置
                syncModeComboBox.SelectedItem = _syncModeTranslations[task.SyncMode];
                incrementalFieldTextBox.Text = task.IncrementalTimestampField;
                syncIntervalNumericUpDown.Value = task.SyncIntervalMinutes;
                forceFullSyncCheckBox.Checked = task.ForceFullSyncNextRun;

                taskTabControl.Enabled = true;
            }
            else
            {
                // 清空所有控件
                dbTypeComboBox.SelectedItem = null;
                dataTypeComboBox.SelectedItem = null;
                connectionStringTextBox.Text = "";
                apiEndpointTextBox.Text = "";
                apiKeyTextBox.Text = "";
                sqlQueryTextBox.Text = "";
                syncModeComboBox.SelectedItem = null;
                incrementalFieldTextBox.Text = "";
                syncIntervalNumericUpDown.Value = 60;
                forceFullSyncCheckBox.Checked = false;
                taskTabControl.Enabled = false;
            }
            _isProgrammaticChange = false;
        }

        private void OnControlValueChanged(object sender, EventArgs e)
        {
            if (_isProgrammaticChange) return;
            SaveCurrentTaskDetails();
        }

        private void SaveCurrentTaskDetails()
        {
            if (taskListbox.SelectedItem is TaskConfig selectedTask)
            {
                // 保存基础配置
                selectedTask.SourceDbType = dbTypeComboBox.SelectedItem as string;
                selectedTask.DataType = dataTypeComboBox.SelectedItem as string;
                selectedTask.SourceConnectionString = connectionStringTextBox.Text;
                selectedTask.DestinationApiEndpoint = apiEndpointTextBox.Text;
                selectedTask.DestinationApiKey = apiKeyTextBox.Text;
                selectedTask.ExtractionSql = sqlQueryTextBox.Text;

                // 保存高级配置
                var selectedDescription = syncModeComboBox.SelectedItem as string;
                if (selectedDescription != null)
                {
                    selectedTask.SyncMode = _syncModeTranslations.FirstOrDefault(x => x.Value == selectedDescription).Key;
                }

                selectedTask.IncrementalTimestampField = incrementalFieldTextBox.Text;
                selectedTask.SyncIntervalMinutes = (int)syncIntervalNumericUpDown.Value;
                selectedTask.ForceFullSyncNextRun = forceFullSyncCheckBox.Checked;

                _appConfig.Tasks.ResetItem(taskListbox.SelectedIndex);
            }
        }

        private void BtnDeleteTask_Click(object sender, EventArgs e)
        {
            if (taskListbox.SelectedItem is TaskConfig selectedTask)
            {
                DialogResult result = MessageBox.Show($"您确定要删除任务 '{selectedTask.TaskName}' 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _appConfig.Tasks.Remove(selectedTask);
                }
            }
            else
            {
                MessageBox.Show("请先在左侧列表中选择一个要删除的任务。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnRunTask.Enabled = enabled;
            btnAddTask.Enabled = enabled;
            btnDeleteTask.Enabled = enabled;
            btnSaveConfig.Enabled = enabled;
            btnTestConnection.Enabled = enabled;
        }

        private void Log(string message)
        {
            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.Invoke(new Action(() => Log(message)));
            }
            else
            {
                textBoxLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
            }
        }
    }
}
