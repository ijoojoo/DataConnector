// 目的：创建一个顶层配置类，用于统一管理任务列表和全局设置（如密码）
using System.ComponentModel;

namespace DataConnector.Core
{
    public class AppConfig
    {
        public string VerificationPassword { get; set; }
        public BindingList<TaskConfig> Tasks { get; set; }

        public AppConfig()
        {
            // 设置一个默认的初始密码
            VerificationPassword = "admin@123";
            Tasks = new BindingList<TaskConfig>();
        }
    }
}