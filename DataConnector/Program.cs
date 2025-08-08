using System;
using System.Threading;
using System.Windows.Forms;

namespace DataConnector
{
    static class Program
    {
        // 为应用程序创建一个全局唯一的互斥体名称
        private const string AppMutexName = "Global\\{E38D62B1-1B34-4A1B-8239-62E3A6B4E9A4}-DataConnector";

        [STAThread]
        static void Main()
        {
            bool createdNew;
            // 尝试创建一个系统级的互斥体。
            // 如果创建成功 (createdNew = true)，说明是第一个实例。
            // 如果失败，说明已有实例在运行。
            using (Mutex mutex = new Mutex(true, AppMutexName, out createdNew))
            {
                if (createdNew)
                {
                    // 正常运行程序
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                    // 程序退出时，using语句会自动释放互斥体
                }
                else
                {
                    // 弹出提示并退出
                    MessageBox.Show("应用程序已经在运行中。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
