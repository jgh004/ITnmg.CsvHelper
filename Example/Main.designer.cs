namespace Example
{
    partial class Main
    {
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_FileName = new System.Windows.Forms.TextBox();
            this.bt_Open = new System.Windows.Forms.Button();
            this.dgv_Data = new System.Windows.Forms.DataGridView();
            this.bt_Save = new System.Windows.Forms.Button();
            this.cob_separator = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cob_FirstIsHead = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cob_FieldEnclosed = new System.Windows.Forms.ComboBox();
            this.pb_Progress = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_Times = new System.Windows.Forms.TextBox();
            this.bt_GenerateTestData = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_FileName
            // 
            this.tb_FileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_FileName.Location = new System.Drawing.Point(12, 12);
            this.tb_FileName.Margin = new System.Windows.Forms.Padding(4);
            this.tb_FileName.Name = "tb_FileName";
            this.tb_FileName.Size = new System.Drawing.Size(881, 25);
            this.tb_FileName.TabIndex = 4;
            // 
            // bt_Open
            // 
            this.bt_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Open.Location = new System.Drawing.Point(897, 10);
            this.bt_Open.Margin = new System.Windows.Forms.Padding(4);
            this.bt_Open.Name = "bt_Open";
            this.bt_Open.Size = new System.Drawing.Size(56, 29);
            this.bt_Open.TabIndex = 5;
            this.bt_Open.Text = "Open";
            this.bt_Open.UseVisualStyleBackColor = true;
            this.bt_Open.Click += new System.EventHandler(this.bt_Open_Click);
            // 
            // dgv_Data
            // 
            this.dgv_Data.AllowUserToAddRows = false;
            this.dgv_Data.AllowUserToDeleteRows = false;
            this.dgv_Data.AllowUserToOrderColumns = true;
            this.dgv_Data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Data.Location = new System.Drawing.Point(3, 119);
            this.dgv_Data.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_Data.Name = "dgv_Data";
            this.dgv_Data.ReadOnly = true;
            this.dgv_Data.RowTemplate.Height = 23;
            this.dgv_Data.Size = new System.Drawing.Size(1278, 541);
            this.dgv_Data.TabIndex = 6;
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Save.Location = new System.Drawing.Point(960, 10);
            this.bt_Save.Margin = new System.Windows.Forms.Padding(4);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(57, 29);
            this.bt_Save.TabIndex = 8;
            this.bt_Save.Text = "Save";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // cob_separator
            // 
            this.cob_separator.FormattingEnabled = true;
            this.cob_separator.Items.AddRange(new object[] {
            ",",
            "|"});
            this.cob_separator.Location = new System.Drawing.Point(160, 48);
            this.cob_separator.Margin = new System.Windows.Forms.Padding(4);
            this.cob_separator.Name = "cob_separator";
            this.cob_separator.Size = new System.Drawing.Size(59, 23);
            this.cob_separator.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 29);
            this.label3.TabIndex = 11;
            this.label3.Text = "Field separator:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cob_FirstIsHead
            // 
            this.cob_FirstIsHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cob_FirstIsHead.FormattingEnabled = true;
            this.cob_FirstIsHead.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.cob_FirstIsHead.Location = new System.Drawing.Point(607, 48);
            this.cob_FirstIsHead.Margin = new System.Windows.Forms.Padding(4);
            this.cob_FirstIsHead.Name = "cob_FirstIsHead";
            this.cob_FirstIsHead.Size = new System.Drawing.Size(59, 23);
            this.cob_FirstIsHead.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(444, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 29);
            this.label4.TabIndex = 13;
            this.label4.Text = "First row is head:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(234, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 29);
            this.label2.TabIndex = 15;
            this.label2.Text = "Field qualifier:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cob_FieldEnclosed
            // 
            this.cob_FieldEnclosed.FormattingEnabled = true;
            this.cob_FieldEnclosed.Items.AddRange(new object[] {
            "\"",
            "#"});
            this.cob_FieldEnclosed.Location = new System.Drawing.Point(374, 48);
            this.cob_FieldEnclosed.Margin = new System.Windows.Forms.Padding(4);
            this.cob_FieldEnclosed.Name = "cob_FieldEnclosed";
            this.cob_FieldEnclosed.Size = new System.Drawing.Size(59, 23);
            this.cob_FieldEnclosed.TabIndex = 14;
            // 
            // pb_Progress
            // 
            this.pb_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Progress.Location = new System.Drawing.Point(12, 82);
            this.pb_Progress.Margin = new System.Windows.Forms.Padding(4);
            this.pb_Progress.Name = "pb_Progress";
            this.pb_Progress.Size = new System.Drawing.Size(1257, 29);
            this.pb_Progress.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(680, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Use times:";
            // 
            // tb_Times
            // 
            this.tb_Times.Location = new System.Drawing.Point(773, 48);
            this.tb_Times.Name = "tb_Times";
            this.tb_Times.ReadOnly = true;
            this.tb_Times.Size = new System.Drawing.Size(120, 25);
            this.tb_Times.TabIndex = 18;
            // 
            // bt_GenerateTestData
            // 
            this.bt_GenerateTestData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_GenerateTestData.Location = new System.Drawing.Point(1112, 10);
            this.bt_GenerateTestData.Name = "bt_GenerateTestData";
            this.bt_GenerateTestData.Size = new System.Drawing.Size(158, 29);
            this.bt_GenerateTestData.TabIndex = 19;
            this.bt_GenerateTestData.Text = "Generate Test Csv";
            this.bt_GenerateTestData.UseVisualStyleBackColor = true;
            this.bt_GenerateTestData.Click += new System.EventHandler(this.bt_GenerateTestData_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Cancel.Location = new System.Drawing.Point(1024, 10);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(65, 29);
            this.bt_Cancel.TabIndex = 20;
            this.bt_Cancel.Text = "Cancel";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 664);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_GenerateTestData);
            this.Controls.Add(this.tb_Times);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pb_Progress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cob_FieldEnclosed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cob_FirstIsHead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cob_separator);
            this.Controls.Add(this.bt_Save);
            this.Controls.Add(this.dgv_Data);
            this.Controls.Add(this.bt_Open);
            this.Controls.Add(this.tb_FileName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CsvTest";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_FileName;
        private System.Windows.Forms.Button bt_Open;
        private System.Windows.Forms.DataGridView dgv_Data;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.ComboBox cob_separator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cob_FirstIsHead;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cob_FieldEnclosed;
        private System.Windows.Forms.ProgressBar pb_Progress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_Times;
        private System.Windows.Forms.Button bt_GenerateTestData;
        private System.Windows.Forms.Button bt_Cancel;
    }
}