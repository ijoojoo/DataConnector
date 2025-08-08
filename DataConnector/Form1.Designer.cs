namespace DataConnector
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.taskListbox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.taskTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dbTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.apiKeyTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.apiEndpointTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.sqlQueryTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.forceFullSyncCheckBox = new System.Windows.Forms.CheckBox();
            this.incrementalFieldTextBox = new System.Windows.Forms.TextBox();
            this.labelIncrementalField = new System.Windows.Forms.Label();
            this.syncModeComboBox = new System.Windows.Forms.ComboBox();
            this.labelSyncMode = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRunTask = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.labelInterval = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.taskTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.syncIntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.taskListbox);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.taskTabControl);
            this.splitContainer1.Size = new System.Drawing.Size(784, 311);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // taskListbox
            // 
            this.taskListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListbox.FormattingEnabled = true;
            this.taskListbox.ItemHeight = 12;
            this.taskListbox.Location = new System.Drawing.Point(0, 0);
            this.taskListbox.Name = "taskListbox";
            this.taskListbox.Size = new System.Drawing.Size(200, 271);
            this.taskListbox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteTask);
            this.panel1.Controls.Add(this.btnAddTask);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 271);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 40);
            this.panel1.TabIndex = 0;
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Location = new System.Drawing.Point(103, 8);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTask.TabIndex = 1;
            this.btnDeleteTask.Text = "删除任务";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(22, 8);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.btnAddTask.TabIndex = 0;
            this.btnAddTask.Text = "新增任务";
            this.btnAddTask.UseVisualStyleBackColor = true;
            // 
            // taskTabControl
            // 
            this.taskTabControl.Controls.Add(this.tabPage1);
            this.taskTabControl.Controls.Add(this.tabPage2);
            this.taskTabControl.Controls.Add(this.tabPage3);
            this.taskTabControl.Controls.Add(this.tabPageAdvanced);
            this.taskTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskTabControl.Location = new System.Drawing.Point(0, 0);
            this.taskTabControl.Name = "taskTabControl";
            this.taskTabControl.SelectedIndex = 0;
            this.taskTabControl.Size = new System.Drawing.Size(580, 311);
            this.taskTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataTypeComboBox);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnTestConnection);
            this.tabPage1.Controls.Add(this.connectionStringTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dbTypeComboBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(572, 285);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据源 (客户数据库)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataTypeComboBox
            // 
            this.dataTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataTypeComboBox.FormattingEnabled = true;
            this.dataTypeComboBox.Items.AddRange(new object[] {
            "Microsoft SQL Server",
            "Oracle",
            "MySQL",
            "PostgreSQL"});
            this.dataTypeComboBox.Location = new System.Drawing.Point(120, 60);
            this.dataTypeComboBox.Name = "dataTypeComboBox";
            this.dataTypeComboBox.Size = new System.Drawing.Size(400, 20);
            this.dataTypeComboBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "数据类型:";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(120, 212);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(100, 30);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "测试连接";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.Location = new System.Drawing.Point(120, 99);
            this.connectionStringTextBox.Multiline = true;
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.connectionStringTextBox.Size = new System.Drawing.Size(400, 100);
            this.connectionStringTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "连接字符串:";
            // 
            // dbTypeComboBox
            // 
            this.dbTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbTypeComboBox.FormattingEnabled = true;
            this.dbTypeComboBox.Items.AddRange(new object[] {
            "Microsoft SQL Server",
            "Oracle",
            "MySQL",
            "PostgreSQL"});
            this.dbTypeComboBox.Location = new System.Drawing.Point(120, 22);
            this.dbTypeComboBox.Name = "dbTypeComboBox";
            this.dbTypeComboBox.Size = new System.Drawing.Size(400, 20);
            this.dbTypeComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库类型:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.apiKeyTextBox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.apiEndpointTextBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(572, 285);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据目的地 (我方API)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Location = new System.Drawing.Point(120, 62);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            this.apiKeyTextBox.PasswordChar = '*';
            this.apiKeyTextBox.Size = new System.Drawing.Size(400, 21);
            this.apiKeyTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "API 密钥:";
            // 
            // apiEndpointTextBox
            // 
            this.apiEndpointTextBox.Location = new System.Drawing.Point(120, 22);
            this.apiEndpointTextBox.Name = "apiEndpointTextBox";
            this.apiEndpointTextBox.Size = new System.Drawing.Size(400, 21);
            this.apiEndpointTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "API 端点:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.sqlQueryTextBox);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(572, 285);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "数据提取SQL";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // sqlQueryTextBox
            // 
            this.sqlQueryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sqlQueryTextBox.Location = new System.Drawing.Point(20, 50);
            this.sqlQueryTextBox.Multiline = true;
            this.sqlQueryTextBox.Name = "sqlQueryTextBox";
            this.sqlQueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sqlQueryTextBox.Size = new System.Drawing.Size(532, 215);
            this.sqlQueryTextBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(239, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "SQL 查询脚本 (查询结果列名需与API一致):";
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.forceFullSyncCheckBox);
            this.tabPageAdvanced.Controls.Add(this.syncIntervalNumericUpDown);
            this.tabPageAdvanced.Controls.Add(this.labelInterval);
            this.tabPageAdvanced.Controls.Add(this.incrementalFieldTextBox);
            this.tabPageAdvanced.Controls.Add(this.labelIncrementalField);
            this.tabPageAdvanced.Controls.Add(this.syncModeComboBox);
            this.tabPageAdvanced.Controls.Add(this.labelSyncMode);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageAdvanced.Size = new System.Drawing.Size(572, 285);
            this.tabPageAdvanced.TabIndex = 3;
            this.tabPageAdvanced.Text = "高级设置";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // forceFullSyncCheckBox
            // 
            this.forceFullSyncCheckBox.AutoSize = true;
            this.forceFullSyncCheckBox.Location = new System.Drawing.Point(22, 185);
            this.forceFullSyncCheckBox.Name = "forceFullSyncCheckBox";
            this.forceFullSyncCheckBox.Size = new System.Drawing.Size(156, 16);
            this.forceFullSyncCheckBox.TabIndex = 8;
            this.forceFullSyncCheckBox.Text = "下次运行时强制全量同步";
            this.forceFullSyncCheckBox.UseVisualStyleBackColor = true;
            // 
            // incrementalFieldTextBox
            // 
            this.incrementalFieldTextBox.Location = new System.Drawing.Point(140, 62);
            this.incrementalFieldTextBox.Name = "incrementalFieldTextBox";
            this.incrementalFieldTextBox.Size = new System.Drawing.Size(250, 21);
            this.incrementalFieldTextBox.TabIndex = 3;
            // 
            // labelIncrementalField
            // 
            this.labelIncrementalField.AutoSize = true;
            this.labelIncrementalField.Location = new System.Drawing.Point(20, 65);
            this.labelIncrementalField.Name = "labelIncrementalField";
            this.labelIncrementalField.Size = new System.Drawing.Size(107, 12);
            this.labelIncrementalField.TabIndex = 2;
            this.labelIncrementalField.Text = "增量同步字段名称:";
            // 
            // syncModeComboBox
            // 
            this.syncModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.syncModeComboBox.FormattingEnabled = true;
            this.syncModeComboBox.Location = new System.Drawing.Point(140, 22);
            this.syncModeComboBox.Name = "syncModeComboBox";
            this.syncModeComboBox.Size = new System.Drawing.Size(250, 20);
            this.syncModeComboBox.TabIndex = 1;
            // 
            // labelSyncMode
            // 
            this.labelSyncMode.AutoSize = true;
            this.labelSyncMode.Location = new System.Drawing.Point(20, 25);
            this.labelSyncMode.Name = "labelSyncMode";
            this.labelSyncMode.Size = new System.Drawing.Size(59, 12);
            this.labelSyncMode.TabIndex = 0;
            this.labelSyncMode.Text = "上传策略:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 361);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态日志";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(3, 17);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(778, 80);
            this.textBoxLog.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRunTask);
            this.panel2.Controls.Add(this.btnSaveConfig);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 50);
            this.panel2.TabIndex = 2;
            // 
            // btnRunTask
            // 
            this.btnRunTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunTask.Location = new System.Drawing.Point(642, 12);
            this.btnRunTask.Name = "btnRunTask";
            this.btnRunTask.Size = new System.Drawing.Size(130, 28);
            this.btnRunTask.TabIndex = 1;
            this.btnRunTask.Text = "立即执行选中任务";
            this.btnRunTask.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConfig.Location = new System.Drawing.Point(506, 12);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(130, 28);
            this.btnSaveConfig.TabIndex = 0;
            this.btnSaveConfig.Text = "保存所有配置";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "数据连接器";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMenuItem,
            this.exitMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 48);
            // 
            // showMenuItem
            // 
            this.showMenuItem.Name = "showMenuItem";
            this.showMenuItem.Size = new System.Drawing.Size(124, 22);
            this.showMenuItem.Text = "显示窗口";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exitMenuItem.Text = "退出程序";
            // 
            // syncIntervalNumericUpDown
            // 
            this.syncIntervalNumericUpDown.Location = new System.Drawing.Point(140, 142);
            this.syncIntervalNumericUpDown.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.syncIntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.syncIntervalNumericUpDown.Name = "syncIntervalNumericUpDown";
            this.syncIntervalNumericUpDown.Size = new System.Drawing.Size(120, 21);
            this.syncIntervalNumericUpDown.TabIndex = 7;
            this.syncIntervalNumericUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(20, 145);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(95, 12);
            this.labelInterval.TabIndex = 6;
            this.labelInterval.Text = "同步周期(分钟):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据连接器配置工具 V1.0";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.taskTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.syncIntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.ListBox taskListbox;
        private System.Windows.Forms.TabControl taskTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox dbTypeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox apiKeyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox apiEndpointTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox sqlQueryTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRunTask;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.Label labelSyncMode;
        private System.Windows.Forms.ComboBox syncModeComboBox;
        private System.Windows.Forms.Label labelIncrementalField;
        private System.Windows.Forms.TextBox incrementalFieldTextBox;
        private System.Windows.Forms.CheckBox forceFullSyncCheckBox;
        private System.Windows.Forms.ComboBox dataTypeComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown syncIntervalNumericUpDown;
        private System.Windows.Forms.Label labelInterval;
    }
}
