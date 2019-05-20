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
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_Data = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_FileName
            // 
            this.tb_FileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_FileName.Location = new System.Drawing.Point(246, 179);
            this.tb_FileName.Margin = new System.Windows.Forms.Padding(7);
            this.tb_FileName.Name = "tb_FileName";
            this.tb_FileName.Size = new System.Drawing.Size(1661, 38);
            this.tb_FileName.TabIndex = 4;
            // 
            // bt_Open
            // 
            this.bt_Open.Location = new System.Drawing.Point(23, 172);
            this.bt_Open.Margin = new System.Windows.Forms.Padding(7);
            this.bt_Open.Name = "bt_Open";
            this.bt_Open.Size = new System.Drawing.Size(202, 52);
            this.bt_Open.TabIndex = 5;
            this.bt_Open.Text = "2.Open";
            this.bt_Open.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Open.UseVisualStyleBackColor = true;
            this.bt_Open.Click += new System.EventHandler(this.bt_Open_Click);
            // 
            // bt_Save
            // 
            this.bt_Save.Location = new System.Drawing.Point(23, 100);
            this.bt_Save.Margin = new System.Windows.Forms.Padding(7);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(202, 52);
            this.bt_Save.TabIndex = 8;
            this.bt_Save.Text = "1.Save To";
            this.bt_Save.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // cob_separator
            // 
            this.cob_separator.FormattingEnabled = true;
            this.cob_separator.Items.AddRange(new object[] {
            ",",
            "|"});
            this.cob_separator.Location = new System.Drawing.Point(275, 33);
            this.cob_separator.Margin = new System.Windows.Forms.Padding(7);
            this.cob_separator.Name = "cob_separator";
            this.cob_separator.Size = new System.Drawing.Size(100, 35);
            this.cob_separator.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 52);
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
            this.cob_FirstIsHead.Location = new System.Drawing.Point(1057, 33);
            this.cob_FirstIsHead.Margin = new System.Windows.Forms.Padding(7);
            this.cob_FirstIsHead.Name = "cob_FirstIsHead";
            this.cob_FirstIsHead.Size = new System.Drawing.Size(100, 35);
            this.cob_FirstIsHead.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(772, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(280, 52);
            this.label4.TabIndex = 13;
            this.label4.Text = "First row is head:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(405, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 52);
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
            this.cob_FieldEnclosed.Location = new System.Drawing.Point(649, 33);
            this.cob_FieldEnclosed.Margin = new System.Windows.Forms.Padding(7);
            this.cob_FieldEnclosed.Name = "cob_FieldEnclosed";
            this.cob_FieldEnclosed.Size = new System.Drawing.Size(100, 35);
            this.cob_FieldEnclosed.TabIndex = 14;
            // 
            // pb_Progress
            // 
            this.pb_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Progress.Location = new System.Drawing.Point(23, 316);
            this.pb_Progress.Margin = new System.Windows.Forms.Padding(7);
            this.pb_Progress.Name = "pb_Progress";
            this.pb_Progress.Size = new System.Drawing.Size(1472, 52);
            this.pb_Progress.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1537, 329);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 27);
            this.label5.TabIndex = 17;
            this.label5.Text = "Use times:";
            // 
            // tb_Times
            // 
            this.tb_Times.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Times.Location = new System.Drawing.Point(1700, 323);
            this.tb_Times.Margin = new System.Windows.Forms.Padding(5);
            this.tb_Times.Name = "tb_Times";
            this.tb_Times.ReadOnly = true;
            this.tb_Times.Size = new System.Drawing.Size(207, 38);
            this.tb_Times.TabIndex = 18;
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(23, 244);
            this.bt_Cancel.Margin = new System.Windows.Forms.Padding(5);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(202, 52);
            this.bt_Cancel.TabIndex = 20;
            this.bt_Cancel.Text = "3.Cancel";
            this.bt_Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.bt_Cancel);
            this.panel1.Controls.Add(this.cob_separator);
            this.panel1.Controls.Add(this.tb_Times);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cob_FirstIsHead);
            this.panel1.Controls.Add(this.pb_Progress);
            this.panel1.Controls.Add(this.cob_FieldEnclosed);
            this.panel1.Controls.Add(this.bt_Save);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_FileName);
            this.panel1.Controls.Add(this.bt_Open);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1941, 396);
            this.panel1.TabIndex = 21;
            // 
            // dgv_Data
            // 
            this.dgv_Data.AllowUserToAddRows = false;
            this.dgv_Data.AllowUserToDeleteRows = false;
            this.dgv_Data.AllowUserToOrderColumns = true;
            this.dgv_Data.ColumnHeadersHeight = 40;
            this.dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Data.Location = new System.Drawing.Point(0, 396);
            this.dgv_Data.Margin = new System.Windows.Forms.Padding(7);
            this.dgv_Data.Name = "dgv_Data";
            this.dgv_Data.ReadOnly = true;
            this.dgv_Data.RowTemplate.Height = 40;
            this.dgv_Data.Size = new System.Drawing.Size(1941, 875);
            this.dgv_Data.TabIndex = 22;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(216F, 216F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1941, 1271);
            this.Controls.Add(this.dgv_Data);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CsvTest";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_FileName;
        private System.Windows.Forms.Button bt_Open;
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
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_Data;
    }
}