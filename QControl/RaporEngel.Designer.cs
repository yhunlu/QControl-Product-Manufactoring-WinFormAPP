namespace QControl
{
    partial class RaporEngel
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
            this.gb_RaporOnay = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_reportsection = new System.Windows.Forms.ComboBox();
            this.cmb_reporttype = new System.Windows.Forms.ComboBox();
            this.clb_raporonay = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tümünüSeçToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tümSeçiliOlanıKaldırToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimePicker_RaporOnayDate = new System.Windows.Forms.DateTimePicker();
            this.tümünüSILToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gb_RaporOnay.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_RaporOnay
            // 
            this.gb_RaporOnay.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gb_RaporOnay.Controls.Add(this.button1);
            this.gb_RaporOnay.Controls.Add(this.label2);
            this.gb_RaporOnay.Controls.Add(this.label1);
            this.gb_RaporOnay.Controls.Add(this.cmb_reportsection);
            this.gb_RaporOnay.Controls.Add(this.cmb_reporttype);
            this.gb_RaporOnay.Controls.Add(this.clb_raporonay);
            this.gb_RaporOnay.Controls.Add(this.dateTimePicker_RaporOnayDate);
            this.gb_RaporOnay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_RaporOnay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gb_RaporOnay.ForeColor = System.Drawing.Color.DimGray;
            this.gb_RaporOnay.Location = new System.Drawing.Point(0, 0);
            this.gb_RaporOnay.Name = "gb_RaporOnay";
            this.gb_RaporOnay.Size = new System.Drawing.Size(280, 371);
            this.gb_RaporOnay.TabIndex = 15;
            this.gb_RaporOnay.TabStop = false;
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(3, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "EKLE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "İlgili Kısımlar :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Rapor İsmi :";
            // 
            // cmb_reportsection
            // 
            this.cmb_reportsection.FormattingEnabled = true;
            this.cmb_reportsection.Location = new System.Drawing.Point(3, 91);
            this.cmb_reportsection.Name = "cmb_reportsection";
            this.cmb_reportsection.Size = new System.Drawing.Size(274, 21);
            this.cmb_reportsection.TabIndex = 11;
            // 
            // cmb_reporttype
            // 
            this.cmb_reporttype.FormattingEnabled = true;
            this.cmb_reporttype.Location = new System.Drawing.Point(3, 51);
            this.cmb_reporttype.Name = "cmb_reporttype";
            this.cmb_reporttype.Size = new System.Drawing.Size(274, 21);
            this.cmb_reporttype.TabIndex = 10;
            this.cmb_reporttype.SelectedIndexChanged += new System.EventHandler(this.cmb_reporttype_SelectedIndexChanged);
            // 
            // clb_raporonay
            // 
            this.clb_raporonay.ContextMenuStrip = this.contextMenuStrip1;
            this.clb_raporonay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.clb_raporonay.FormattingEnabled = true;
            this.clb_raporonay.Location = new System.Drawing.Point(3, 139);
            this.clb_raporonay.Name = "clb_raporonay";
            this.clb_raporonay.Size = new System.Drawing.Size(274, 229);
            this.clb_raporonay.TabIndex = 9;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tümünüSeçToolStripMenuItem,
            this.tümSeçiliOlanıKaldırToolStripMenuItem,
            this.SilToolStripMenuItem,
            this.tümünüSILToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(194, 114);
            // 
            // tümünüSeçToolStripMenuItem
            // 
            this.tümünüSeçToolStripMenuItem.Name = "tümünüSeçToolStripMenuItem";
            this.tümünüSeçToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.tümünüSeçToolStripMenuItem.Text = "Tümünü Seç";
            this.tümünüSeçToolStripMenuItem.Click += new System.EventHandler(this.tümünüSeçToolStripMenuItem_Click);
            // 
            // tümSeçiliOlanıKaldırToolStripMenuItem
            // 
            this.tümSeçiliOlanıKaldırToolStripMenuItem.Name = "tümSeçiliOlanıKaldırToolStripMenuItem";
            this.tümSeçiliOlanıKaldırToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.tümSeçiliOlanıKaldırToolStripMenuItem.Text = "Tüm Seçili Olanı Kaldır";
            this.tümSeçiliOlanıKaldırToolStripMenuItem.Click += new System.EventHandler(this.tümSeçiliOlanıKaldırToolStripMenuItem_Click);
            // 
            // SilToolStripMenuItem
            // 
            this.SilToolStripMenuItem.Name = "SilToolStripMenuItem";
            this.SilToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.SilToolStripMenuItem.Text = "SIL";
            this.SilToolStripMenuItem.Click += new System.EventHandler(this.SilToolStripMenuItem_Click);
            // 
            // dateTimePicker_RaporOnayDate
            // 
            this.dateTimePicker_RaporOnayDate.Location = new System.Drawing.Point(3, 12);
            this.dateTimePicker_RaporOnayDate.Name = "dateTimePicker_RaporOnayDate";
            this.dateTimePicker_RaporOnayDate.Size = new System.Drawing.Size(274, 20);
            this.dateTimePicker_RaporOnayDate.TabIndex = 8;
            this.dateTimePicker_RaporOnayDate.ValueChanged += new System.EventHandler(this.dateTimePicker_RaporOnayDate_ValueChanged_1);
            // 
            // tümünüSILToolStripMenuItem
            // 
            this.tümünüSILToolStripMenuItem.Name = "tümünüSILToolStripMenuItem";
            this.tümünüSILToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.tümünüSILToolStripMenuItem.Text = "Tümünü SIL";
            this.tümünüSILToolStripMenuItem.Click += new System.EventHandler(this.tümünüSILToolStripMenuItem_Click);
            // 
            // RaporEngel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(280, 371);
            this.Controls.Add(this.gb_RaporOnay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RaporEngel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ENGELLI RAPORLAR";
            this.Load += new System.EventHandler(this.RaporEngel_Load);
            this.gb_RaporOnay.ResumeLayout(false);
            this.gb_RaporOnay.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_RaporOnay;
        private System.Windows.Forms.CheckedListBox clb_raporonay;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tümünüSeçToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tümSeçiliOlanıKaldırToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmb_reporttype;
        private System.Windows.Forms.ToolStripMenuItem SilToolStripMenuItem;
        public System.Windows.Forms.DateTimePicker dateTimePicker_RaporOnayDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_reportsection;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem tümünüSILToolStripMenuItem;
    }
}