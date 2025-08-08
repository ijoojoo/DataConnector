using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataConnector.Core
{
    public class SyncStateManager
    {
        private const string StateFileName = "sync_state.json";
        private Dictionary<string, DateTime> _syncStates;

        public SyncStateManager()
        {
            LoadState();
        }

        private void LoadState()
        {
            if (File.Exists(StateFileName))
            {
                string json = File.ReadAllText(StateFileName);
                _syncStates = JsonConvert.DeserializeObject<Dictionary<string, DateTime>>(json) ?? new Dictionary<string, DateTime>();
            }
            else
            {
                _syncStates = new Dictionary<string, DateTime>();
            }
        }

        private void SaveState()
        {
            string json = JsonConvert.SerializeObject(_syncStates, Formatting.Indented);
            File.WriteAllText(StateFileName, json);
        }

        public DateTime GetLastSyncTime(string taskName)
        {
            return _syncStates.TryGetValue(taskName, out DateTime lastSync) ? lastSync : new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        // --- ↓↓↓ 核心修改：重载 UpdateLastSyncTime 方法 ↓↓↓ ---
        /// <summary>
        /// 将指定任务的同步时间点更新为当前UTC时间
        /// </summary>
        public void UpdateLastSyncTime(string taskName)
        {
            _syncStates[taskName] = DateTime.UtcNow;
            SaveState();
        }

        /// <summary>
        /// 将指定任务的同步时间点更新为一个精确的时间
        /// </summary>
        public void UpdateLastSyncTime(string taskName, DateTime specificTime)
        {
            // 确保存入的是UTC时间
            _syncStates[taskName] = specificTime.ToUniversalTime();
            SaveState();
        }
        // --- ↑↑↑ 修改结束 ↑↑↑ ---

        public void ResetSyncTime(string taskName)
        {
            if (_syncStates.ContainsKey(taskName))
            {
                _syncStates.Remove(taskName);
                SaveState();
            }
        }
    }
}
