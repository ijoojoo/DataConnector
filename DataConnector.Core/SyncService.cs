using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataConnector.Core
{
    /// <summary>
    /// 负责执行数据同步任务的核心服务
    /// </summary>
    public class SyncService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly SyncStateManager _stateManager;

        public SyncService()
        {
            _stateManager = new SyncStateManager();
        }

        /// <summary>
        /// 运行指定的同步任务，并返回配置是否被修改过
        /// </summary>
        public async Task<bool> RunTaskAsync(TaskConfig task, Action<string> logCallback)
        {
            logCallback($"开始执行任务: {task.TaskName}");
            return await ExtractAndLoadInBatchesAsync(task, logCallback);
        }

        /// <summary>
        /// 核心方法：以流式读取和分批上传的方式处理数据
        /// </summary>
        private async Task<bool> ExtractAndLoadInBatchesAsync(TaskConfig task, Action<string> logCallback)
        {
            logCallback("步骤1/2: 开始从源数据库提取数据...");
            DbConnection connection = null;
            int totalRowsProcessed = 0;
            int batchNumber = 1;
            bool configChanged = false;

            try
            {
                connection = CreateDbConnection(task.SourceDbType, task.SourceConnectionString);
                if (connection == null) throw new Exception($"不支持的数据库类型 '{task.SourceDbType}'");

                await connection.OpenAsync();
                logCallback("数据库连接成功。");

                using (var command = connection.CreateCommand())
                {
                    string finalSql = task.ExtractionSql;
                    DateTime lastSyncTimeLocal = new DateTime(1970, 1, 1);

                    if (task.SyncMode == SyncMode.AppendOnly || task.SyncMode == SyncMode.MergeUpdate)
                    {
                        DateTime lastSyncTimeUtc;
                        if (task.ForceFullSyncNextRun)
                        {
                            logCallback("检测到强制全量同步标志，本次将同步所有数据。");
                            lastSyncTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            _stateManager.ResetSyncTime(task.TaskName);
                        }
                        else
                        {
                            lastSyncTimeUtc = _stateManager.GetLastSyncTime(task.TaskName);
                        }

                        lastSyncTimeLocal = lastSyncTimeUtc.ToLocalTime();
                        finalSql = finalSql.Replace("@LastSyncTime", lastSyncTimeLocal.ToString("yyyy-MM-dd HH:mm:ss"));
                        logCallback($"将从 {lastSyncTimeLocal:yyyy-MM-dd HH:mm:ss} (本地时间) 开始增量同步。");
                    }
                    else
                    {
                        logCallback("当前为全量快照模式，将同步所有数据。");
                    }
                    command.CommandText = finalSql;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                        var batch = new List<Dictionary<string, object>>();
                        DateTime maxTimestampInBatch = lastSyncTimeLocal;

                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < columns.Count; i++)
                            {
                                var value = reader.GetValue(i);
                                row[columns[i]] = value == DBNull.Value ? null : value;
                            }
                            batch.Add(row);
                            totalRowsProcessed++;

                            if (task.SyncMode != SyncMode.FullSnapshot && !string.IsNullOrEmpty(task.IncrementalTimestampField))
                            {
                                object timestampValue = row.ContainsKey(task.IncrementalTimestampField) ? row[task.IncrementalTimestampField] : null;
                                if (timestampValue is DateTime dt && dt > maxTimestampInBatch)
                                {
                                    maxTimestampInBatch = dt;
                                }
                            }

                            if (batch.Count >= task.BatchSize)
                            {
                                logCallback($"已读取 {batch.Count} 条记录，准备上传第 {batchNumber} 批...");
                                bool success = await LoadBatchAsync(task, batch, batchNumber, logCallback);
                                if (!success) throw new Exception($"第 {batchNumber} 批上传失败，任务中止。");

                                if (task.SyncMode != SyncMode.FullSnapshot)
                                {
                                    _stateManager.UpdateLastSyncTime(task.TaskName, maxTimestampInBatch);
                                    logCallback($"同步点已更新至: {maxTimestampInBatch:yyyy-MM-dd HH:mm:ss}");
                                }

                                batch.Clear();
                                batchNumber++;
                            }
                        }

                        if (batch.Count > 0)
                        {
                            logCallback($"已读取最后 {batch.Count} 条记录，准备上传第 {batchNumber} 批...");
                            bool success = await LoadBatchAsync(task, batch, batchNumber, logCallback);
                            if (!success) throw new Exception($"第 {batchNumber} 批上传失败，任务中止。");

                            if (task.SyncMode != SyncMode.FullSnapshot)
                            {
                                _stateManager.UpdateLastSyncTime(task.TaskName, maxTimestampInBatch);
                                logCallback($"最终同步点已更新至: {maxTimestampInBatch:yyyy-MM-dd HH:mm:ss}");
                            }
                        }
                    }
                }

                if (totalRowsProcessed == 0)
                {
                    logCallback("未发现需要同步的新数据。任务正常结束。");
                }
                else
                {
                    logCallback($"数据提取和上传全部完成，共处理 {totalRowsProcessed} 条记录。");
                }

                if (task.ForceFullSyncNextRun)
                {
                    task.ForceFullSyncNextRun = false;
                    configChanged = true;
                    logCallback("强制全量同步标志已重置。请记得保存配置。");
                }
            }
            finally
            {
                connection?.Close();
            }
            return configChanged;
        }

        private async Task<bool> LoadBatchAsync(TaskConfig task, List<Dictionary<string, object>> batch, int batchNumber, Action<string> logCallback)
        {
            logCallback($"步骤2/2: 正在上传第 {batchNumber} 批数据...");

            try
            {
                string jsonData = JsonConvert.SerializeObject(batch);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                string endpointPath = GetEndpointForDataType(task.DataType);
                string fullUrl = task.DestinationApiEndpoint.TrimEnd('/') + endpointPath;

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, fullUrl)
                {
                    Content = content
                };

                requestMessage.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                requestMessage.Headers.Add("Api-Key", task.DestinationApiKey);

                var response = await client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    logCallback($"第 {batchNumber} 批 ({batch.Count}条) 数据上传成功！");
                    return true;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    logCallback($"第 {batchNumber} 批数据上传失败。状态码: {response.StatusCode}, 错误信息: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                logCallback($"第 {batchNumber} 批数据上传时发生网络错误: {ex.Message}");
                return false;
            }
        }

        private string GetEndpointForDataType(string dataType)
        {
            switch (dataType)
            {
                case "商品信息": return "/api/data/products/sync/";
                case "门店信息": return "/api/data/stores/sync/";
                case "供应商信息": return "/api/data/suppliers/sync/";
                case "采购记录": return "/api/data/purchases/sync/";
                case "销售记录": return "/api/data/sales/sync/";
                case "库存快照": return "/api/data/inventory/sync/";
                case "会员信息": return "/api/data/members/sync/";
                case "职员信息": return "/api/data/employees/sync/";
                default: throw new ArgumentException($"不支持的数据类型: {dataType}");
            }
        }

        public async Task<bool> TestConnectionAsync(TaskConfig task, Action<string> logCallback)
        {
            logCallback($"开始测试连接 ({task.SourceDbType})...");
            DbConnection connection = null;

            try
            {
                connection = CreateDbConnection(task.SourceDbType, task.SourceConnectionString);
                if (connection == null)
                {
                    logCallback($"不支持的数据库类型: {task.SourceDbType}");
                    return false;
                }
                await connection.OpenAsync();
                logCallback("数据库连接成功！");
                return true;
            }
            catch (Exception ex)
            {
                logCallback($"数据库连接失败: {ex.Message}");
                return false;
            }
            finally
            {
                connection?.Close();
            }
        }

        private DbConnection CreateDbConnection(string dbType, string connectionString)
        {
            switch (dbType)
            {
                case "Microsoft SQL Server":
                    return new SqlConnection(connectionString);
                case "Oracle":
                    return new OracleConnection(connectionString);
                default:
                    return null;
            }
        }
    }
}
