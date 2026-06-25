namespace Newton
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnStart = new Button();
            btnStop = new Button();
            panelPlot = new Panel();
            lblStatusA = new Label();
            lblStatusB = new Label();
            btnTareA = new Button();
            btnTareB = new Button();
            groupBoxControls = new GroupBox();
            groupBox2 = new GroupBox();
            txtFactorB = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtFactorA = new TextBox();
            groupBox1 = new GroupBox();
            comboTimeWindow = new ComboBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            clearGraphToolStripMenuItem = new ToolStripMenuItem();
            saveAsCSVToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            lblIpAddress = new Label();
            groupBoxControls.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(139, 14);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(48, 28);
            btnStart.TabIndex = 6;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(139, 44);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(48, 28);
            btnStop.TabIndex = 7;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // panelPlot
            // 
            panelPlot.Location = new Point(12, 143);
            panelPlot.Name = "panelPlot";
            panelPlot.Size = new Size(1010, 421);
            panelPlot.TabIndex = 8;
            // 
            // lblStatusA
            // 
            lblStatusA.AutoSize = true;
            lblStatusA.Location = new Point(278, 17);
            lblStatusA.Name = "lblStatusA";
            lblStatusA.Size = new Size(15, 20);
            lblStatusA.TabIndex = 10;
            lblStatusA.Text = "-";
            // 
            // lblStatusB
            // 
            lblStatusB.AutoSize = true;
            lblStatusB.Location = new Point(278, 44);
            lblStatusB.Name = "lblStatusB";
            lblStatusB.Size = new Size(15, 20);
            lblStatusB.TabIndex = 11;
            lblStatusB.Text = "-";
            // 
            // btnTareA
            // 
            btnTareA.Location = new Point(192, 14);
            btnTareA.Name = "btnTareA";
            btnTareA.Size = new Size(70, 28);
            btnTareA.TabIndex = 12;
            btnTareA.Text = "Tare A";
            btnTareA.UseVisualStyleBackColor = true;
            btnTareA.Click += btnTareA_Click;
            // 
            // btnTareB
            // 
            btnTareB.Location = new Point(192, 44);
            btnTareB.Name = "btnTareB";
            btnTareB.Size = new Size(70, 28);
            btnTareB.TabIndex = 13;
            btnTareB.Text = "Tare B";
            btnTareB.UseVisualStyleBackColor = true;
            btnTareB.Click += btnTareB_Click;
            // 
            // groupBoxControls
            // 
            groupBoxControls.Controls.Add(groupBox2);
            groupBoxControls.Controls.Add(groupBox1);
            groupBoxControls.Location = new Point(12, 31);
            groupBoxControls.Name = "groupBoxControls";
            groupBoxControls.Size = new Size(1010, 115);
            groupBoxControls.TabIndex = 14;
            groupBoxControls.TabStop = false;
            groupBoxControls.Text = "-";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtFactorB);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(txtFactorA);
            groupBox2.Controls.Add(btnTareB);
            groupBox2.Controls.Add(btnTareA);
            groupBox2.Controls.Add(lblStatusB);
            groupBox2.Controls.Add(btnStop);
            groupBox2.Controls.Add(btnStart);
            groupBox2.Controls.Add(lblStatusA);
            groupBox2.Location = new Point(207, 14);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(785, 83);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Sensors";
            // 
            // txtFactorB
            // 
            txtFactorB.Location = new Point(83, 45);
            txtFactorB.Name = "txtFactorB";
            txtFactorB.Size = new Size(50, 27);
            txtFactorB.TabIndex = 17;
            txtFactorB.Text = "1,00";
            txtFactorB.TextChanged += txtFactorB_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 23);
            label2.Name = "label2";
            label2.Size = new Size(68, 20);
            label2.TabIndex = 16;
            label2.Text = "Factror A";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 45);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 15;
            label1.Text = "Factror B";
            // 
            // txtFactorA
            // 
            txtFactorA.Location = new Point(83, 15);
            txtFactorA.Name = "txtFactorA";
            txtFactorA.Size = new Size(50, 27);
            txtFactorA.TabIndex = 14;
            txtFactorA.Text = "1,00";
            txtFactorA.TextChanged += txtFactorA_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblIpAddress);
            groupBox1.Controls.Add(comboTimeWindow);
            groupBox1.Location = new Point(6, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(195, 83);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Time Base [s]";
            // 
            // comboTimeWindow
            // 
            comboTimeWindow.FormattingEnabled = true;
            comboTimeWindow.Location = new Point(6, 26);
            comboTimeWindow.Name = "comboTimeWindow";
            comboTimeWindow.Size = new Size(105, 28);
            comboTimeWindow.TabIndex = 14;
            comboTimeWindow.SelectedIndexChanged += comboTimeWindow_SelectedIndexChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1392, 28);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearGraphToolStripMenuItem, saveAsCSVToolStripMenuItem, exitToolStripMenuItem, helpToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // clearGraphToolStripMenuItem
            // 
            clearGraphToolStripMenuItem.Name = "clearGraphToolStripMenuItem";
            clearGraphToolStripMenuItem.Size = new Size(173, 26);
            clearGraphToolStripMenuItem.Text = "Clear Graph";
            clearGraphToolStripMenuItem.Click += clearGraphToolStripMenuItem_Click;
            // 
            // saveAsCSVToolStripMenuItem
            // 
            saveAsCSVToolStripMenuItem.Name = "saveAsCSVToolStripMenuItem";
            saveAsCSVToolStripMenuItem.Size = new Size(173, 26);
            saveAsCSVToolStripMenuItem.Text = "Save As CSV";
            saveAsCSVToolStripMenuItem.Click += saveAsCSVToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(173, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(173, 26);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // lblIpAddress
            // 
            lblIpAddress.AutoSize = true;
            lblIpAddress.Location = new Point(6, 60);
            lblIpAddress.Name = "lblIpAddress";
            lblIpAddress.Size = new Size(15, 20);
            lblIpAddress.TabIndex = 16;
            lblIpAddress.Text = "-";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1392, 634);
            Controls.Add(groupBoxControls);
            Controls.Add(panelPlot);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Live Force Display";
            Load += MainForm_Load;
            groupBoxControls.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnStart;
        private Button btnStop;
        private Panel panelPlot;
        private Label lblStatusA;
        private Label lblStatusB;
        private Button btnTareA;
        private Button btnTareB;
        private GroupBox groupBoxControls;
        private ComboBox comboTimeWindow;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem clearGraphToolStripMenuItem;
        private ToolStripMenuItem saveAsCSVToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label label1;
        private TextBox txtFactorA;
        private TextBox txtFactorB;
        private Label label2;
        private ToolStripMenuItem helpToolStripMenuItem;
        private Label lblIpAddress;
    }
}
