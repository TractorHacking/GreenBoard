namespace polyCan_jCOM
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCOMPort = new System.Windows.Forms.ComboBox();
            this.timerLoop = new System.Windows.Forms.Timer(this.components);
            this.txtGatewayLog = new System.Windows.Forms.TextBox();
            this.btnRequestStatus = new System.Windows.Forms.Button();
            this.btnClearGatewayLog = new System.Windows.Forms.Button();
            this.btnClaimAddr = new System.Windows.Forms.Button();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.btnDelFilter = new System.Windows.Forms.Button();
            this.btnTransmit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.foundCmd = new System.Windows.Forms.TextBox();
            this.updateLoop = new System.Windows.Forms.Timer(this.components);
            this.addr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.loadLog = new System.Windows.Forms.Button();
            this.TrLog = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdsGotten = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(9, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboCOMPort);
            this.groupBox1.Location = new System.Drawing.Point(83, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(589, 55);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Gateway ";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(281, 23);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 21);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop COM";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(185, 23);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 21);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start COM";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM Port:";
            // 
            // cboCOMPort
            // 
            this.cboCOMPort.FormattingEnabled = true;
            this.cboCOMPort.Location = new System.Drawing.Point(68, 24);
            this.cboCOMPort.Name = "cboCOMPort";
            this.cboCOMPort.Size = new System.Drawing.Size(111, 21);
            this.cboCOMPort.TabIndex = 0;
            this.cboCOMPort.Text = "Select COM Port";
            // 
            // timerLoop
            // 
            this.timerLoop.Interval = 1;
            this.timerLoop.Tick += new System.EventHandler(this.timerLoop_Tick);
            // 
            // txtGatewayLog
            // 
            this.txtGatewayLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGatewayLog.Location = new System.Drawing.Point(268, 73);
            this.txtGatewayLog.Multiline = true;
            this.txtGatewayLog.Name = "txtGatewayLog";
            this.txtGatewayLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGatewayLog.Size = new System.Drawing.Size(404, 558);
            this.txtGatewayLog.TabIndex = 38;
            // 
            // btnRequestStatus
            // 
            this.btnRequestStatus.Enabled = false;
            this.btnRequestStatus.Location = new System.Drawing.Point(12, 112);
            this.btnRequestStatus.Name = "btnRequestStatus";
            this.btnRequestStatus.Size = new System.Drawing.Size(90, 21);
            this.btnRequestStatus.TabIndex = 53;
            this.btnRequestStatus.Text = "Request Status";
            this.btnRequestStatus.UseVisualStyleBackColor = true;
            this.btnRequestStatus.Click += new System.EventHandler(this.btnRequestStatus_Click);
            // 
            // btnClearGatewayLog
            // 
            this.btnClearGatewayLog.Location = new System.Drawing.Point(12, 85);
            this.btnClearGatewayLog.Name = "btnClearGatewayLog";
            this.btnClearGatewayLog.Size = new System.Drawing.Size(90, 21);
            this.btnClearGatewayLog.TabIndex = 52;
            this.btnClearGatewayLog.Text = "Clear Log";
            this.btnClearGatewayLog.UseVisualStyleBackColor = true;
            this.btnClearGatewayLog.Click += new System.EventHandler(this.btnClearGatewayLog_Click);
            // 
            // btnClaimAddr
            // 
            this.btnClaimAddr.Enabled = false;
            this.btnClaimAddr.Location = new System.Drawing.Point(12, 139);
            this.btnClaimAddr.Name = "btnClaimAddr";
            this.btnClaimAddr.Size = new System.Drawing.Size(90, 20);
            this.btnClaimAddr.TabIndex = 54;
            this.btnClaimAddr.Text = "Claim Address";
            this.btnClaimAddr.UseVisualStyleBackColor = true;
            this.btnClaimAddr.Click += new System.EventHandler(this.btnClaimAddr_Click);
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.Enabled = false;
            this.btnAddFilter.Location = new System.Drawing.Point(12, 165);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(90, 20);
            this.btnAddFilter.TabIndex = 55;
            this.btnAddFilter.Text = "Add Filter";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // btnDelFilter
            // 
            this.btnDelFilter.Enabled = false;
            this.btnDelFilter.Location = new System.Drawing.Point(12, 191);
            this.btnDelFilter.Name = "btnDelFilter";
            this.btnDelFilter.Size = new System.Drawing.Size(90, 20);
            this.btnDelFilter.TabIndex = 56;
            this.btnDelFilter.Text = "Delete Filter";
            this.btnDelFilter.UseVisualStyleBackColor = true;
            this.btnDelFilter.Click += new System.EventHandler(this.btnDelFilter_Click);
            // 
            // btnTransmit
            // 
            this.btnTransmit.Enabled = false;
            this.btnTransmit.Location = new System.Drawing.Point(12, 217);
            this.btnTransmit.Name = "btnTransmit";
            this.btnTransmit.Size = new System.Drawing.Size(90, 20);
            this.btnTransmit.TabIndex = 57;
            this.btnTransmit.Text = "Transmit PGN";
            this.btnTransmit.UseVisualStyleBackColor = true;
            this.btnTransmit.Click += new System.EventHandler(this.btnTransmit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 328);
            this.label2.MinimumSize = new System.Drawing.Size(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Commands Recieved:";
            // 
            // foundCmd
            // 
            this.foundCmd.Location = new System.Drawing.Point(129, 325);
            this.foundCmd.Name = "foundCmd";
            this.foundCmd.ReadOnly = true;
            this.foundCmd.Size = new System.Drawing.Size(133, 20);
            this.foundCmd.TabIndex = 60;
            this.foundCmd.Text = "0";
            // 
            // updateLoop
            // 
            this.updateLoop.Enabled = true;
            this.updateLoop.Interval = 50;
            this.updateLoop.Tick += new System.EventHandler(this.updateLoop_Tick);
            // 
            // addr
            // 
            this.addr.Location = new System.Drawing.Point(145, 140);
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(100, 20);
            this.addr.TabIndex = 61;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 143);
            this.label3.MinimumSize = new System.Drawing.Size(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Addr:";
            // 
            // loadLog
            // 
            this.loadLog.Enabled = false;
            this.loadLog.Location = new System.Drawing.Point(12, 243);
            this.loadLog.Name = "loadLog";
            this.loadLog.Size = new System.Drawing.Size(90, 20);
            this.loadLog.TabIndex = 63;
            this.loadLog.Text = "Load Log";
            this.loadLog.UseVisualStyleBackColor = true;
            this.loadLog.Click += new System.EventHandler(this.loadLogAction);
            // 
            // TrLog
            // 
            this.TrLog.Enabled = false;
            this.TrLog.Location = new System.Drawing.Point(14, 269);
            this.TrLog.Name = "TrLog";
            this.TrLog.Size = new System.Drawing.Size(90, 20);
            this.TrLog.TabIndex = 64;
            this.TrLog.Text = "Transmit Log";
            this.TrLog.UseVisualStyleBackColor = true;
            this.TrLog.Click += new System.EventHandler(this.trLogFile);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 302);
            this.label4.MinimumSize = new System.Drawing.Size(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Commands Transmitted:";
            // 
            // cmdsGotten
            // 
            this.cmdsGotten.Location = new System.Drawing.Point(129, 299);
            this.cmdsGotten.Name = "cmdsGotten";
            this.cmdsGotten.ReadOnly = true;
            this.cmdsGotten.Size = new System.Drawing.Size(133, 20);
            this.cmdsGotten.TabIndex = 66;
            this.cmdsGotten.Text = "0";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 643);
            this.Controls.Add(this.cmdsGotten);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TrLog);
            this.Controls.Add(this.loadLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addr);
            this.Controls.Add(this.foundCmd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTransmit);
            this.Controls.Add(this.btnDelFilter);
            this.Controls.Add(this.btnAddFilter);
            this.Controls.Add(this.btnClaimAddr);
            this.Controls.Add(this.btnRequestStatus);
            this.Controls.Add(this.btnClearGatewayLog);
            this.Controls.Add(this.txtGatewayLog);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "PolyCan jCOM";
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCOMPort;
        private System.Windows.Forms.Timer timerLoop;
        private System.Windows.Forms.TextBox txtGatewayLog;
        private System.Windows.Forms.Button btnRequestStatus;
        private System.Windows.Forms.Button btnClearGatewayLog;
        private System.Windows.Forms.Button btnClaimAddr;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.Button btnDelFilter;
        private System.Windows.Forms.Button btnTransmit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox foundCmd;
        private System.Windows.Forms.Timer updateLoop;
        private System.Windows.Forms.TextBox addr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button loadLog;
        private System.Windows.Forms.Button TrLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cmdsGotten;
    }
}

