namespace MailFingerPrinter
{
    partial class FingerPrinterForm
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
            this.WhereMailIn = new System.Windows.Forms.FolderBrowserDialog();
            this.WhereToCreateMFG = new System.Windows.Forms.FolderBrowserDialog();
            this.WhereTrainMFGIn = new System.Windows.Forms.FolderBrowserDialog();
            this.WhichMFG = new System.Windows.Forms.OpenFileDialog();
            this.tabCompare = new System.Windows.Forms.TabPage();
            this.pB_resultimgbox = new System.Windows.Forms.PictureBox();
            this.bt_painter = new System.Windows.Forms.Button();
            this.cb_results = new System.Windows.Forms.ComboBox();
            this.dGV_resulttable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_getresult = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_trainfrom = new System.Windows.Forms.TextBox();
            this.tb_mfgfrom = new System.Windows.Forms.TextBox();
            this.bt_trainfrom = new System.Windows.Forms.Button();
            this.bt_testfingerprinterfrom = new System.Windows.Forms.Button();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.cb_hashead = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_tomfg = new System.Windows.Forms.Button();
            this.bt_mailsfrom = new System.Windows.Forms.Button();
            this.tb_mfgto = new System.Windows.Forms.TextBox();
            this.tb_mailfrom = new System.Windows.Forms.TextBox();
            this.bt_create = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCompare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_resultimgbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_resulttable)).BeginInit();
            this.tabCreate.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WhichMFG
            // 
            this.WhichMFG.FileName = "SelectedMFG";
            // 
            // tabCompare
            // 
            this.tabCompare.Controls.Add(this.pB_resultimgbox);
            this.tabCompare.Controls.Add(this.bt_painter);
            this.tabCompare.Controls.Add(this.cb_results);
            this.tabCompare.Controls.Add(this.dGV_resulttable);
            this.tabCompare.Controls.Add(this.bt_getresult);
            this.tabCompare.Controls.Add(this.label6);
            this.tabCompare.Controls.Add(this.label5);
            this.tabCompare.Controls.Add(this.label4);
            this.tabCompare.Controls.Add(this.tb_trainfrom);
            this.tabCompare.Controls.Add(this.tb_mfgfrom);
            this.tabCompare.Controls.Add(this.bt_trainfrom);
            this.tabCompare.Controls.Add(this.bt_testfingerprinterfrom);
            this.tabCompare.Location = new System.Drawing.Point(4, 25);
            this.tabCompare.Name = "tabCompare";
            this.tabCompare.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompare.Size = new System.Drawing.Size(996, 550);
            this.tabCompare.TabIndex = 1;
            this.tabCompare.Text = "Compare mail fingerprints";
            this.tabCompare.UseVisualStyleBackColor = true;
            // 
            // pB_resultimgbox
            // 
            this.pB_resultimgbox.Location = new System.Drawing.Point(119, 170);
            this.pB_resultimgbox.Name = "pB_resultimgbox";
            this.pB_resultimgbox.Size = new System.Drawing.Size(350, 350);
            this.pB_resultimgbox.TabIndex = 11;
            this.pB_resultimgbox.TabStop = false;
            // 
            // bt_painter
            // 
            this.bt_painter.Location = new System.Drawing.Point(20, 265);
            this.bt_painter.Name = "bt_painter";
            this.bt_painter.Size = new System.Drawing.Size(93, 50);
            this.bt_painter.TabIndex = 10;
            this.bt_painter.Text = "Graph It";
            this.bt_painter.UseVisualStyleBackColor = true;
            this.bt_painter.Click += new System.EventHandler(this.bt_painter_Click);
            // 
            // cb_results
            // 
            this.cb_results.FormattingEnabled = true;
            this.cb_results.Location = new System.Drawing.Point(184, 131);
            this.cb_results.Name = "cb_results";
            this.cb_results.Size = new System.Drawing.Size(575, 23);
            this.cb_results.TabIndex = 9;
            // 
            // dGV_resulttable
            // 
            this.dGV_resulttable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_resulttable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dGV_resulttable.Location = new System.Drawing.Point(475, 209);
            this.dGV_resulttable.Name = "dGV_resulttable";
            this.dGV_resulttable.ReadOnly = true;
            this.dGV_resulttable.RowTemplate.Height = 27;
            this.dGV_resulttable.Size = new System.Drawing.Size(494, 242);
            this.dGV_resulttable.TabIndex = 8;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "D";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 75;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "HFW";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 75;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "LSU";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 75;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "V";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 75;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "AU";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 75;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "RAWH";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 75;
            // 
            // bt_getresult
            // 
            this.bt_getresult.Location = new System.Drawing.Point(20, 189);
            this.bt_getresult.Name = "bt_getresult";
            this.bt_getresult.Size = new System.Drawing.Size(93, 48);
            this.bt_getresult.TabIndex = 7;
            this.bt_getresult.Text = "Generate result";
            this.bt_getresult.UseVisualStyleBackColor = true;
            this.bt_getresult.Click += new System.EventHandler(this.bt_getresult_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(407, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "(directory can contain multiple fingerprint files)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.Location = new System.Drawing.Point(17, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(367, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Fingerprint directory for training set：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(98, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(286, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Fingerprint file for test set：";
            // 
            // tb_trainfrom
            // 
            this.tb_trainfrom.Location = new System.Drawing.Point(184, 85);
            this.tb_trainfrom.Name = "tb_trainfrom";
            this.tb_trainfrom.ReadOnly = true;
            this.tb_trainfrom.Size = new System.Drawing.Size(575, 25);
            this.tb_trainfrom.TabIndex = 3;
            // 
            // tb_mfgfrom
            // 
            this.tb_mfgfrom.Location = new System.Drawing.Point(184, 33);
            this.tb_mfgfrom.Name = "tb_mfgfrom";
            this.tb_mfgfrom.ReadOnly = true;
            this.tb_mfgfrom.Size = new System.Drawing.Size(575, 25);
            this.tb_mfgfrom.TabIndex = 2;
            // 
            // bt_trainfrom
            // 
            this.bt_trainfrom.Location = new System.Drawing.Point(794, 78);
            this.bt_trainfrom.Name = "bt_trainfrom";
            this.bt_trainfrom.Size = new System.Drawing.Size(118, 35);
            this.bt_trainfrom.TabIndex = 1;
            this.bt_trainfrom.Text = "Browse";
            this.bt_trainfrom.UseVisualStyleBackColor = true;
            this.bt_trainfrom.Click += new System.EventHandler(this.bt_trainfrom_Click);
            // 
            // bt_testfingerprinterfrom
            // 
            this.bt_testfingerprinterfrom.Location = new System.Drawing.Point(794, 26);
            this.bt_testfingerprinterfrom.Name = "bt_testfingerprinterfrom";
            this.bt_testfingerprinterfrom.Size = new System.Drawing.Size(118, 35);
            this.bt_testfingerprinterfrom.TabIndex = 0;
            this.bt_testfingerprinterfrom.Text = "Browse";
            this.bt_testfingerprinterfrom.UseVisualStyleBackColor = true;
            this.bt_testfingerprinterfrom.Click += new System.EventHandler(this.bt_testfingerprinterfrom_Click);
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.cb_hashead);
            this.tabCreate.Controls.Add(this.label3);
            this.tabCreate.Controls.Add(this.label2);
            this.tabCreate.Controls.Add(this.label1);
            this.tabCreate.Controls.Add(this.bt_tomfg);
            this.tabCreate.Controls.Add(this.bt_mailsfrom);
            this.tabCreate.Controls.Add(this.tb_mfgto);
            this.tabCreate.Controls.Add(this.tb_mailfrom);
            this.tabCreate.Controls.Add(this.bt_create);
            this.tabCreate.Location = new System.Drawing.Point(4, 25);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreate.Size = new System.Drawing.Size(996, 550);
            this.tabCreate.TabIndex = 0;
            this.tabCreate.Text = "Generate email fingerprint";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // cb_hashead
            // 
            this.cb_hashead.AutoSize = true;
            this.cb_hashead.Location = new System.Drawing.Point(584, 368);
            this.cb_hashead.Name = "cb_hashead";
            this.cb_hashead.Size = new System.Drawing.Size(261, 19);
            this.cb_hashead.TabIndex = 10;
            this.cb_hashead.Text = "Remove the header of the mail";
            this.cb_hashead.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(37, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(377, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Save the generated fingerprint directory:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(235, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mail set directory:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(131, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(726, 65);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fingerprint files are a feature of a particular mailing collection Please generat" +
    "e your fingerprints files and then continue to follow-up operation";
            // 
            // bt_tomfg
            // 
            this.bt_tomfg.Location = new System.Drawing.Point(750, 290);
            this.bt_tomfg.Name = "bt_tomfg";
            this.bt_tomfg.Size = new System.Drawing.Size(95, 35);
            this.bt_tomfg.TabIndex = 4;
            this.bt_tomfg.Text = "Browse";
            this.bt_tomfg.UseVisualStyleBackColor = true;
            this.bt_tomfg.Click += new System.EventHandler(this.bt_tomfg_Click);
            // 
            // bt_mailsfrom
            // 
            this.bt_mailsfrom.Location = new System.Drawing.Point(750, 188);
            this.bt_mailsfrom.Name = "bt_mailsfrom";
            this.bt_mailsfrom.Size = new System.Drawing.Size(95, 35);
            this.bt_mailsfrom.TabIndex = 3;
            this.bt_mailsfrom.Text = "Browse";
            this.bt_mailsfrom.UseVisualStyleBackColor = true;
            this.bt_mailsfrom.Click += new System.EventHandler(this.bt_mailsfrom_Click);
            // 
            // tb_mfgto
            // 
            this.tb_mfgto.Location = new System.Drawing.Point(273, 297);
            this.tb_mfgto.Name = "tb_mfgto";
            this.tb_mfgto.ReadOnly = true;
            this.tb_mfgto.Size = new System.Drawing.Size(471, 25);
            this.tb_mfgto.TabIndex = 2;
            // 
            // tb_mailfrom
            // 
            this.tb_mailfrom.Location = new System.Drawing.Point(273, 195);
            this.tb_mailfrom.Name = "tb_mailfrom";
            this.tb_mailfrom.ReadOnly = true;
            this.tb_mailfrom.Size = new System.Drawing.Size(471, 25);
            this.tb_mailfrom.TabIndex = 1;
            // 
            // bt_create
            // 
            this.bt_create.Location = new System.Drawing.Point(459, 363);
            this.bt_create.Name = "bt_create";
            this.bt_create.Size = new System.Drawing.Size(75, 26);
            this.bt_create.TabIndex = 0;
            this.bt_create.Text = "Generate";
            this.bt_create.UseVisualStyleBackColor = true;
            this.bt_create.Click += new System.EventHandler(this.bt_create_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCreate);
            this.tabControl1.Controls.Add(this.tabCompare);
            this.tabControl1.Location = new System.Drawing.Point(1, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1004, 579);
            this.tabControl1.TabIndex = 0;
            // 
            // FingerPrinterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 578);
            this.Controls.Add(this.tabControl1);
            this.Name = "FingerPrinterForm";
            this.Text = "MailFingerPrinter";
            this.tabCompare.ResumeLayout(false);
            this.tabCompare.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_resultimgbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_resulttable)).EndInit();
            this.tabCreate.ResumeLayout(false);
            this.tabCreate.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog WhereMailIn;
        private System.Windows.Forms.FolderBrowserDialog WhereToCreateMFG;
        private System.Windows.Forms.FolderBrowserDialog WhereTrainMFGIn;
        private System.Windows.Forms.OpenFileDialog WhichMFG;
        private System.Windows.Forms.TabPage tabCompare;
        private System.Windows.Forms.DataGridView dGV_resulttable;
        private System.Windows.Forms.Button bt_getresult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_trainfrom;
        private System.Windows.Forms.TextBox tb_mfgfrom;
        private System.Windows.Forms.Button bt_trainfrom;
        private System.Windows.Forms.Button bt_testfingerprinterfrom;
        private System.Windows.Forms.TabPage tabCreate;
        private System.Windows.Forms.CheckBox cb_hashead;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_tomfg;
        private System.Windows.Forms.Button bt_mailsfrom;
        private System.Windows.Forms.TextBox tb_mfgto;
        private System.Windows.Forms.TextBox tb_mailfrom;
        private System.Windows.Forms.Button bt_create;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button bt_painter;
        private System.Windows.Forms.ComboBox cb_results;
        private System.Windows.Forms.PictureBox pB_resultimgbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}

