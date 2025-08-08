using System;
using System.ComponentModel;

namespace DataConnector.Core
{
    /// <summary>
    /// 定义数据同步的策略模式
    /// </summary>
    public enum SyncMode
    {
        MergeUpdate,  // 合并更新 (用于主数据)
        AppendOnly,   // 仅追加 (用于交易记录)
        FullSnapshot  // 全量快照 (用于库存等)
    }

    /// <summary>
    /// 用于描述单个数据同步任务的配置信息
    /// </summary>
    public class TaskConfig
    {
        public string TaskName { get; set; } = "新任务";
        public string SourceDbType { get; set; }
        public string SourceConnectionString { get; set; }
        public string ExtractionSql { get; set; }
        public string DestinationApiEndpoint { get; set; }
        public string DestinationApiKey { get; set; }
        public int BatchSize { get; set; } = 500;
        public SyncMode SyncMode { get; set; } = SyncMode.MergeUpdate;
        public bool ForceFullSyncNextRun { get; set; }
        public string DataType { get; set; } = "商品信息";
        public string IncrementalTimestampField { get; set; } = "last_modified_at";

        // --- ↓↓↓ 新增内容开始：恢复同步周期配置 ↓↓↓ ---
        /// <summary>
        /// 任务的自动执行周期（分钟）
        /// </summary>
        public int SyncIntervalMinutes { get; set; } = 60;
        // --- ↑↑↑ 新增内容结束 ↑↑↑ ---

        public override string ToString()
        {
            return TaskName;
        }
    }
}
