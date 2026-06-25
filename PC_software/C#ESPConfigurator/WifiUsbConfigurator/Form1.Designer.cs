namespace WifiUsbConfigurator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rbA = new System.Windows.Forms.CheckBox();
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.rbB = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnReboot = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.btn_ReadConfig = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PCIPO = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtSsid = new System.Windows.Forms.TextBox();
            this.txtPcPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPcIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWifiList = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // rbA
            // 
            this.rbA.AutoSize = true;
            this.rbA.Location = new System.Drawing.Point(64, 318);
            this.rbA.Name = "rbA";
            this.rbA.Size = new System.Drawing.Size(38, 20);
            this.rbA.TabIndex = 4;
            this.rbA.Text = "A";
            this.rbA.UseVisualStyleBackColor = true;
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(92, 17);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(244, 24);
            this.cmbComPorts.TabIndex = 5;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(406, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(117, 26);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Kapcsolódás";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // rbB
            // 
            this.rbB.AutoSize = true;
            this.rbB.Location = new System.Drawing.Point(64, 359);
            this.rbB.Name = "rbB";
            this.rbB.Size = new System.Drawing.Size(38, 20);
            this.rbB.TabIndex = 9;
            this.rbB.Text = "B";
            this.rbB.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(31, 263);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(737, 22);
            this.txtLog.TabIndex = 10;
            // 
            // btnReboot
            // 
            this.btnReboot.Location = new System.Drawing.Point(484, 202);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(158, 27);
            this.btnReboot.TabIndex = 13;
            this.btnReboot.Text = "ReBoot";
            this.btnReboot.UseVisualStyleBackColor = true;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(484, 121);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(151, 27);
            this.btnInfo.TabIndex = 14;
            this.btnInfo.Text = "Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(484, 158);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(157, 29);
            this.btnSaveConfig.TabIndex = 15;
            this.btnSaveConfig.Text = "SaveConfig";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.Location = new System.Drawing.Point(529, 12);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(161, 26);
            this.btnRefreshPorts.TabIndex = 16;
            this.btnRefreshPorts.Text = "Refres COM";
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
            // 
            // btn_ReadConfig
            // 
            this.btn_ReadConfig.Location = new System.Drawing.Point(484, 88);
            this.btn_ReadConfig.Name = "btn_ReadConfig";
            this.btn_ReadConfig.Size = new System.Drawing.Size(96, 24);
            this.btn_ReadConfig.TabIndex = 17;
            this.btn_ReadConfig.Text = "ReadConfig";
            this.btn_ReadConfig.UseVisualStyleBackColor = true;
            this.btn_ReadConfig.Click += new System.EventHandler(this.btn_ReadConfig_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Com Ports";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnRefreshPorts);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.cmbComPorts);
            this.groupBox1.Location = new System.Drawing.Point(57, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 47);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Com";
            // 
            // PCIPO
            // 
            this.PCIPO.AutoSize = true;
            this.PCIPO.Location = new System.Drawing.Point(42, 104);
            this.PCIPO.Name = "PCIPO";
            this.PCIPO.Size = new System.Drawing.Size(43, 16);
            this.PCIPO.TabIndex = 11;
            this.PCIPO.Text = "PC IP:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(141, 115);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(246, 22);
            this.txtPassword.TabIndex = 8;
            // 
            // txtSsid
            // 
            this.txtSsid.Location = new System.Drawing.Point(143, 82);
            this.txtSsid.Name = "txtSsid";
            this.txtSsid.Size = new System.Drawing.Size(244, 22);
            this.txtSsid.TabIndex = 7;
            // 
            // txtPcPort
            // 
            this.txtPcPort.Location = new System.Drawing.Point(141, 187);
            this.txtPcPort.Name = "txtPcPort";
            this.txtPcPort.Size = new System.Drawing.Size(246, 22);
            this.txtPcPort.TabIndex = 2;
            this.txtPcPort.Text = "4210";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "WiFi jelszó:";
            // 
            // txtPcIp
            // 
            this.txtPcIp.Location = new System.Drawing.Point(141, 152);
            this.txtPcIp.Name = "txtPcIp";
            this.txtPcIp.Size = new System.Drawing.Size(246, 22);
            this.txtPcIp.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "WiFi SSID:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnWifiList);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.PCIPO);
            this.groupBox2.Location = new System.Drawing.Point(31, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 203);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WiFi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "PC Port:";
            // 
            // btnWifiList
            // 
            this.btnWifiList.Location = new System.Drawing.Point(30, 174);
            this.btnWifiList.Name = "btnWifiList";
            this.btnWifiList.Size = new System.Drawing.Size(75, 23);
            this.btnWifiList.TabIndex = 21;
            this.btnWifiList.Text = "PC WIFI";
            this.btnWifiList.UseVisualStyleBackColor = true;
            this.btnWifiList.Click += new System.EventHandler(this.btnWifiList_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtPcIp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPcPort);
            this.Controls.Add(this.btn_ReadConfig);
            this.Controls.Add(this.txtSsid);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.rbB);
            this.Controls.Add(this.rbA);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.CheckBox rbA;
        private System.Windows.Forms.ComboBox cmbComPorts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox rbB;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.Button btn_ReadConfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label PCIPO;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtSsid;
        private System.Windows.Forms.TextBox txtPcPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPcIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWifiList;
    }
}

