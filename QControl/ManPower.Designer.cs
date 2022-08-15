namespace QControl
{
    partial class ManPower
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lst_workers = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lv_third = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sILToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lv_second = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.lv_first = new System.Windows.Forms.ListView();
            this.lst_log = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmb_shop = new System.Windows.Forms.ComboBox();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cIKISToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sICILNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACIKLAMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add = new System.Windows.Forms.DataGridViewButtonColumn();
            this.update = new System.Windows.Forms.DataGridViewButtonColumn();
            this.remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lst_workers
            // 
            this.lst_workers.AllowDrop = true;
            this.lst_workers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_workers.ContextMenuStrip = this.contextMenuStrip2;
            this.lst_workers.Dock = System.Windows.Forms.DockStyle.Left;
            this.lst_workers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_workers.FormattingEnabled = true;
            this.lst_workers.Location = new System.Drawing.Point(0, 0);
            this.lst_workers.Name = "lst_workers";
            this.lst_workers.Size = new System.Drawing.Size(211, 708);
            this.lst_workers.Sorted = true;
            this.lst_workers.TabIndex = 0;
            this.lst_workers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lst_workers_DrawItem);
            this.lst_workers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lst_workers_MouseDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(127, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItem1.Text = "Işçi Paneli";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lv_third);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lv_second);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lv_first);
            this.groupBox1.Location = new System.Drawing.Point(217, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(711, 72);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VARDIYA SEÇ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(451, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "16-00:00:";
            // 
            // lv_third
            // 
            this.lv_third.AllowDrop = true;
            this.lv_third.ContextMenuStrip = this.contextMenuStrip1;
            this.lv_third.LabelWrap = false;
            this.lv_third.Location = new System.Drawing.Point(488, 42);
            this.lv_third.MultiSelect = false;
            this.lv_third.Name = "lv_third";
            this.lv_third.Size = new System.Drawing.Size(199, 171);
            this.lv_third.TabIndex = 4;
            this.lv_third.Tag = "";
            this.lv_third.UseCompatibleStateImageBehavior = false;
            this.lv_third.View = System.Windows.Forms.View.Tile;
            this.lv_third.Click += new System.EventHandler(this.lv_third_Click);
            this.lv_third.DragDrop += new System.Windows.Forms.DragEventHandler(this.lv_third_DragDrop);
            this.lv_third.DragOver += new System.Windows.Forms.DragEventHandler(this.lv_third_DragOver);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sILToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(90, 26);
            // 
            // sILToolStripMenuItem
            // 
            this.sILToolStripMenuItem.Name = "sILToolStripMenuItem";
            this.sILToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            this.sILToolStripMenuItem.Text = "SIL";
            this.sILToolStripMenuItem.Click += new System.EventHandler(this.sILToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "08-16:00 :";
            // 
            // lv_second
            // 
            this.lv_second.AllowDrop = true;
            this.lv_second.ContextMenuStrip = this.contextMenuStrip1;
            this.lv_second.LabelWrap = false;
            this.lv_second.Location = new System.Drawing.Point(271, 42);
            this.lv_second.MultiSelect = false;
            this.lv_second.Name = "lv_second";
            this.lv_second.Size = new System.Drawing.Size(199, 171);
            this.lv_second.TabIndex = 2;
            this.lv_second.Tag = "";
            this.lv_second.UseCompatibleStateImageBehavior = false;
            this.lv_second.View = System.Windows.Forms.View.Tile;
            this.lv_second.Click += new System.EventHandler(this.lv_second_Click);
            this.lv_second.DragDrop += new System.Windows.Forms.DragEventHandler(this.lv_second_DragDrop);
            this.lv_second.DragOver += new System.Windows.Forms.DragEventHandler(this.lv_second_DragOver);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "00-08:00 :";
            // 
            // lv_first
            // 
            this.lv_first.AllowDrop = true;
            this.lv_first.ContextMenuStrip = this.contextMenuStrip1;
            this.lv_first.HideSelection = false;
            this.lv_first.LabelWrap = false;
            this.lv_first.Location = new System.Drawing.Point(47, 42);
            this.lv_first.MultiSelect = false;
            this.lv_first.Name = "lv_first";
            this.lv_first.Size = new System.Drawing.Size(199, 171);
            this.lv_first.TabIndex = 0;
            this.lv_first.Tag = "";
            this.lv_first.UseCompatibleStateImageBehavior = false;
            this.lv_first.View = System.Windows.Forms.View.Tile;
            this.lv_first.Click += new System.EventHandler(this.lv_first_Click);
            this.lv_first.DragDrop += new System.Windows.Forms.DragEventHandler(this.lv_first_DragDrop);
            this.lv_first.DragOver += new System.Windows.Forms.DragEventHandler(this.lv_first_DragOver);
            // 
            // lst_log
            // 
            this.lst_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_log.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_log.FormattingEnabled = true;
            this.lst_log.Location = new System.Drawing.Point(3, 16);
            this.lst_log.Name = "lst_log";
            this.lst_log.Size = new System.Drawing.Size(711, 351);
            this.lst_log.TabIndex = 3;
            this.lst_log.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lst_log_DrawItem);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lst_log);
            this.groupBox2.Location = new System.Drawing.Point(211, 338);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(717, 370);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LOGs";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmb_shop);
            this.groupBox3.Controls.Add(this.dtp_date);
            this.groupBox3.Location = new System.Drawing.Point(217, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(711, 57);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TARIH SEÇ";
            // 
            // cmb_shop
            // 
            this.cmb_shop.FormattingEnabled = true;
            this.cmb_shop.Location = new System.Drawing.Point(76, 19);
            this.cmb_shop.Name = "cmb_shop";
            this.cmb_shop.Size = new System.Drawing.Size(126, 21);
            this.cmb_shop.TabIndex = 1;
            this.cmb_shop.SelectedIndexChanged += new System.EventHandler(this.cmb_shop_SelectedIndexChanged);
            this.cmb_shop.Click += new System.EventHandler(this.cmb_shop_Click);
            this.cmb_shop.DragDrop += new System.Windows.Forms.DragEventHandler(this.cmb_shop_DragDrop);
            this.cmb_shop.Enter += new System.EventHandler(this.cmb_shop_Enter);
            // 
            // dtp_date
            // 
            this.dtp_date.Location = new System.Drawing.Point(261, 19);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(200, 20);
            this.dtp_date.TabIndex = 0;
            this.dtp_date.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(85)))));
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(211, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(803, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cIKISToolStripMenuItem});
            this.dosyaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dosyaToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(199)))));
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.dosyaToolStripMenuItem.Text = "DOSYA";
            // 
            // cIKISToolStripMenuItem
            // 
            this.cIKISToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(76)))), ((int)(((byte)(85)))));
            this.cIKISToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(199)))));
            this.cIKISToolStripMenuItem.Name = "cIKISToolStripMenuItem";
            this.cIKISToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.cIKISToolStripMenuItem.Text = "CIKIS";
            this.cIKISToolStripMenuItem.Click += new System.EventHandler(this.cIKISToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sICILNODataGridViewTextBoxColumn,
            this.Date,
            this.ACIKLAMA,
            this.add,
            this.update,
            this.remove});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(217, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(711, 150);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SICILNO";
            this.dataGridViewTextBoxColumn1.HeaderText = "SICILNO";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 74;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn2.HeaderText = "Date";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 55;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "VUKUAT";
            this.dataGridViewTextBoxColumn3.HeaderText = "VUKUAT";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 76;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ACIKLAMA";
            this.dataGridViewTextBoxColumn4.HeaderText = "ACIKLAMA";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 85;
            // 
            // sICILNODataGridViewTextBoxColumn
            // 
            this.sICILNODataGridViewTextBoxColumn.DataPropertyName = "SICILNO";
            this.sICILNODataGridViewTextBoxColumn.HeaderText = "SICILNO";
            this.sICILNODataGridViewTextBoxColumn.Name = "sICILNODataGridViewTextBoxColumn";
            this.sICILNODataGridViewTextBoxColumn.Width = 74;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.Width = 55;
            // 
            // ACIKLAMA
            // 
            this.ACIKLAMA.DataPropertyName = "ACIKLAMA";
            this.ACIKLAMA.HeaderText = "ACIKLAMA";
            this.ACIKLAMA.Name = "ACIKLAMA";
            this.ACIKLAMA.Width = 85;
            // 
            // add
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            this.add.DefaultCellStyle = dataGridViewCellStyle1;
            this.add.HeaderText = "EKLE";
            this.add.Name = "add";
            this.add.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.add.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.add.Width = 59;
            // 
            // update
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Olive;
            this.update.DefaultCellStyle = dataGridViewCellStyle2;
            this.update.HeaderText = "GUNCELLE";
            this.update.Name = "update";
            this.update.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.update.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.update.Width = 89;
            // 
            // remove
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Maroon;
            this.remove.DefaultCellStyle = dataGridViewCellStyle3;
            this.remove.HeaderText = "SIL";
            this.remove.Name = "remove";
            this.remove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.remove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.remove.Width = 48;
            // 
            // ManPower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1014, 708);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lst_workers);
            this.Name = "ManPower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İşçi Kontrol Modülü";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ManPower_Load);
            this.contextMenuStrip2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_workers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lst_log;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lv_first;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lv_third;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lv_second;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sILToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ComboBox cmb_shop;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cIKISToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn sICILNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACIKLAMA;
        private System.Windows.Forms.DataGridViewButtonColumn add;
        private System.Windows.Forms.DataGridViewButtonColumn update;
        private System.Windows.Forms.DataGridViewButtonColumn remove;
    }
}