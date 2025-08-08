using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
// --- 核心修改：移除了 using System.Windows.Forms; ---

namespace DataConnector.Core
{
    public class ConfigManager
    {
        private const string ConfigFileName = "config.json";
        // Ensure configuration path resolves relative to the application's base directory.
        private static readonly string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);

        public AppConfig Load(Action<string> logCallback)
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    var loadedConfig = JsonConvert.DeserializeObject<AppConfig>(json);
                    logCallback("配置加载成功。");
                    return loadedConfig ?? new AppConfig();
                }
                else
                {
                    logCallback("未找到配置文件，将创建新的默认配置。");
                }
            }
            catch (Exception ex)
            {
                // --- 核心修改：不再显示MessageBox，只记录日志 ---
                logCallback($"加载配置文件时出错: {ex.Message}");
            }
            return new AppConfig();
        }

        public bool Save(AppConfig config, Action<string> logCallback)
        {
            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json);
                logCallback($"配置已保存到 {ConfigFilePath}。");
                return true; // 返回true表示成功
            }
            catch (Exception ex)
            {
                // --- 核心修改：不再显示MessageBox，只记录日志 ---
                logCallback($"保存配置文件时出错: {ex.Message}");
                return false; // 返回false表示失败
            }
        }

        public bool SaveSilently(AppConfig config, Action<string> logCallback)
        {
            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json);
                logCallback($"配置已自动保存到 {ConfigFilePath}。");
                return true;
            }
            catch (Exception ex)
            {
                logCallback($"自动保存配置文件时出错: {ex.Message}");
                return false;
            }
        }
    }
}
