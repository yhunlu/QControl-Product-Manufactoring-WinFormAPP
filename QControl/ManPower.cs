using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QControl
{
    public partial class ManPower : Form
    {
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private string sql = null;
        private string findvalue;
        private string splitno;
        private bool decision;
        FromDLL dll = new FromDLL();
        Control c = new Control();
        WorkerPanel wp = null;
        public ManPower()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }
        public void CallWorkers()
        {
            lst_workers.Items.Clear();
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " select SICILNO + '-' + ADI + ' ' + SOYADI from cn_workers where Fabrika='"+cmb_shop.Text+"' order by Id ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                lst_workers.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        public void CallLog()
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " select Remarks from tbl_VardiyaLog order by Id ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                lst_log.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            int visibleItems = lst_log.ClientSize.Height / lst_log.ItemHeight;
            lst_log.TopIndex = Math.Max(lst_log.Items.Count - visibleItems + 1, 0);
            if (lst_log.SelectedIndex >= 0) { lst_log.SetSelected(lst_log.Items.Count - 1, true); }
        }
        private void ManPower_Load(object sender, EventArgs e)
        {
            decision = false;
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Location from st_Location order by dbo.fn_CreateAlphanumericSortValue(Location)", cmb_shop, "Location");
            CallWorkers();
            CallLog();
            Call1VardiyaUpToDate(lv_first);
            Call2VardiyaUpToDate(lv_second);
            Call3VardiyaUpToDate(lv_third);
            decision = true;
        }

        private void lst_workers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lst_workers.DoDragDrop(QControl.Properties.Resources.add1, DragDropEffects.All);

            }
        }
        private void lv_first_DragOver(object sender, DragEventArgs e)
        {
            if (e.KeyState == 1)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        public void Call1VardiyaUpToDate(ListView lst)
        {
            lst.Items.Clear();
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " select SICILNO from tbl_WorkersVardiya WHERE BirinciVardiya=1 AND Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                lst.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        public void Call2VardiyaUpToDate(ListView lst)
        {
            lst.Items.Clear();
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " select SICILNO from tbl_WorkersVardiya WHERE IkinciVardiya=1 AND Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                lst.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        public void Call3VardiyaUpToDate(ListView lst)
        {
            lst.Items.Clear();
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " select SICILNO from tbl_WorkersVardiya WHERE UcuncuVardiya=1 AND Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                lst.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control x in this.groupBox1.Controls)
            {
                if (x is ListView)
                {
                    ((ListView)x).Items.Clear();
                }
            }
            Call1VardiyaUpToDate(lv_first);
            Call2VardiyaUpToDate(lv_second);
            Call3VardiyaUpToDate(lv_third);
        }

        private void lv_second_DragOver(object sender, DragEventArgs e)
        {
            if (e.KeyState == 1)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lv_third_DragOver(object sender, DragEventArgs e)
        {
            if (e.KeyState == 1)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        public void rightclickToDelete()
        {
            if (lv_first.SelectedItems.Count > 0)
            {
                if (lv_first.SelectedItems[0] != null)
                {
                    dll.UpdateDeleteInsertFunctions(
                        " Delete t "
                        + " from tbl_WorkersVardiya t where t.Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) and t.SICILNO=LTRIM(RTRIM('" + lv_first.SelectedItems[0].Text + "')) "
                        , Properties.Settings.Default.cnn
                    );
                }
            }
            else if (lv_second.SelectedItems.Count > 0)
            {
                if (lv_second.SelectedItems[0] != null)
                {
                    dll.UpdateDeleteInsertFunctions(
                        " Delete t "
                        + " from tbl_WorkersVardiya t where t.Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) and t.SICILNO=LTRIM(RTRIM('" + lv_second.SelectedItems[0].Text + "')) "
                        , Properties.Settings.Default.cnn
                    );
                }
            }
            else if (lv_third.SelectedItems.Count>0)
            {
                if (lv_third.SelectedItems[0] != null)
                {
                    dll.UpdateDeleteInsertFunctions(
                        " Delete t "
                        + " from tbl_WorkersVardiya t where t.Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) and t.SICILNO=LTRIM(RTRIM('" + lv_third.SelectedItems[0].Text + "')) "
                        , Properties.Settings.Default.cnn
                    );
                }
            }
            Call1VardiyaUpToDate(lv_first);
            Call2VardiyaUpToDate(lv_second);
            Call3VardiyaUpToDate(lv_third);
        }
        public void targetfinder(ListBox lst)
        {
            int last = lst.SelectedItem.ToString().LastIndexOf("-");
            int first = 0;
            findvalue = lst.SelectedItem.ToString().Substring(first, last);
        }

        public void InsertOrUpdate_Vardiya(int v)
        {
            if(v==1)
            {
                dll.UpdateDeleteInsertFunctions(
                    " UPDATE tbl_WorkersVardiya set "
                    + " BirinciVardiya=1, "
                    + " IkinciVardiya=0, "
                    + " UcuncuVardiya=0 "
                    + " from tbl_WorkersVardiya t where t.Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) and t.SICILNO=LTRIM(RTRIM('" + findvalue + "')) "
                    + " IF @@ROWCOUNT=0 "
                    + " INSERT into tbl_WorkersVardiya ( "
                    + " SICILNO,Date,BirinciVardiya,IkinciVardiya,UcuncuVardiya "
                    + " ) "
                    + " select  "
                    + " LTRIM(RTRIM('" + findvalue + "')),LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')),1,0,0 "
                    , Properties.Settings.Default.cnn
                );
            }
            else if(v==2)
            {
                dll.UpdateDeleteInsertFunctions(
                    " UPDATE tbl_WorkersVardiya set "
                    + " BirinciVardiya=0, "
                    + " IkinciVardiya=1, "
                    + " UcuncuVardiya=0 "
                    + " from tbl_WorkersVardiya t where t.Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) and t.SICILNO=LTRIM(RTRIM('" + findvalue + "')) "
                    + " IF @@ROWCOUNT=0 "
                    + " INSERT into tbl_WorkersVardiya ( "
                    + " SICILNO,Date,BirinciVardiya,IkinciVardiya,UcuncuVardiya "
                    + " ) "
                    + " select  "
                    + " LTRIM(RTRIM('" + findvalue + "')),LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')),0,1,0 "
                    , Properties.Settings.Default.cnn
                );
            }
            else if(v==3)
            {
                dll.UpdateDeleteInsertFunctions(
                    " UPDATE tbl_WorkersVardiya set "
                    + " BirinciVardiya=0, "
                    + " IkinciVardiya=0, "
                    + " UcuncuVardiya=1 "
                    + " from tbl_WorkersVardiya t where t.Date=LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')) and t.SICILNO=LTRIM(RTRIM('" + findvalue + "')) "
                    + " IF @@ROWCOUNT=0 "
                    + " INSERT into tbl_WorkersVardiya ( "
                    + " SICILNO,Date,BirinciVardiya,IkinciVardiya,UcuncuVardiya "
                    + " ) "
                    + " select  "
                    + " LTRIM(RTRIM('" + findvalue + "')),LTRIM(RTRIM('" + dtp_date.Value.ToString("dd.MM.yyyy") + "')),0,0,1 "
                    , Properties.Settings.Default.cnn
                );
            }

        }
        public void Insert_VardiyaLogs()
        {
            dll.UpdateDeleteInsertFunctions(
                " Insert into tbl_VardiyaLog ( "
                + " SICILNO,OperationTime,Remarks "
                + " ) "
                + " select  "
                + " LTRIM(RTRIM('" + findvalue + "')),LTRIM(RTRIM('" + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "')),LTRIM(RTRIM('" + lst_log.SelectedItem.ToString() + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        private void lv_first_DragDrop(object sender, DragEventArgs e)
        {
            dll.ListboxToListViewDragDropANDlogListbox(lst_workers,lst_log,lv_first,lv_second,lv_third, " [" + dtp_date.Value.ToString("dd.MM.yyyy") + "] 00-08:00 vardiyasına atandı.",lv_first);
            targetfinder(lst_workers);
            lst_log.SelectedIndex = lst_log.Items.Count - 1;
            Insert_VardiyaLogs();
            if (lst_log.SelectedItem.ToString().Substring(0,1)!="!") { InsertOrUpdate_Vardiya(1); }
        }

        private void lv_second_DragDrop(object sender, DragEventArgs e)
        {
            dll.ListboxToListViewDragDropANDlogListbox(lst_workers, lst_log, lv_first, lv_second, lv_third, " [" + dtp_date.Value.ToString("dd.MM.yyyy") + "] 08-16:00 vardiyasına atandı.", lv_second);
            targetfinder(lst_workers);
            lst_log.SelectedIndex = lst_log.Items.Count - 1;
            Insert_VardiyaLogs();
            if (lst_log.SelectedItem.ToString().Substring(0, 1) != "!") { InsertOrUpdate_Vardiya(2); }
        }

        private void lv_third_DragDrop(object sender, DragEventArgs e)
        {
            dll.ListboxToListViewDragDropANDlogListbox(lst_workers, lst_log, lv_first, lv_second, lv_third, " [" + dtp_date.Value.ToString("dd.MM.yyyy") + "] 16-00:00 vardiyasına atandı.", lv_third);
            targetfinder(lst_workers);
            lst_log.SelectedIndex = lst_log.Items.Count - 1;
            Insert_VardiyaLogs();
            if (lst_log.SelectedItem.ToString().Substring(0, 1) != "!") { InsertOrUpdate_Vardiya(3); }
        }

        private void sILToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightclickToDelete();
        }
        public void workerpickup(ListView lv)
        {
            for (int i = 0; i < lst_workers.Items.Count; i++)
            {
                int last = lst_workers.Items[i].ToString().LastIndexOf("-");
                int first = 0;
                string workfinder = lst_workers.Items[i].ToString().Substring(first, last);

                if (workfinder == lv.SelectedItems[0].Text)
                {
                    lst_workers.SetSelected(i, true);
                }
            }
        }
        private void lv_first_Click(object sender, EventArgs e)
        {
            workerpickup(lv_first);
        }

        private void lst_workers_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            //if the item state is selected them change the back color 
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.Yellow);//Choose the color

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Draw the current item text
            e.Graphics.DrawString(lst_workers.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void lv_second_Click(object sender, EventArgs e)
        {
            workerpickup(lv_second);
        }

        private void lv_third_Click(object sender, EventArgs e)
        {
            workerpickup(lv_third);
        }

        private void lst_log_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (decision == true)
            {
                if (lst_log.SelectedIndex >= 0)
                {
                    if (lst_log.SelectedItem.ToString().Substring(0, 1) != "!")
                    {
                        if (e.Index < 0) return;
                        //if the item state is selected them change the back color 
                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            e = new DrawItemEventArgs(e.Graphics,
                                                      e.Font,
                                                      e.Bounds,
                                                      e.Index,
                                                      e.State ^ DrawItemState.Selected,
                                                      e.ForeColor,
                                                      Color.LightGreen);//Choose the color

                        // Draw the background of the ListBox control for each item.
                        e.DrawBackground();
                        // Draw the current item text
                        e.Graphics.DrawString(lst_log.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
                        // If the ListBox has focus, draw a focus rectangle around the selected item.
                        e.DrawFocusRectangle();
                    }
                    else
                    {
                        if (e.Index < 0) return;
                        //if the item state is selected them change the back color 
                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            e = new DrawItemEventArgs(e.Graphics,
                                                      e.Font,
                                                      e.Bounds,
                                                      e.Index,
                                                      e.State ^ DrawItemState.Selected,
                                                      e.ForeColor,
                                                      Color.OrangeRed);//Choose the color

                        // Draw the background of the ListBox control for each item.
                        e.DrawBackground();
                        // Draw the current item text
                        e.Graphics.DrawString(lst_log.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
                        // If the ListBox has focus, draw a focus rectangle around the selected item.
                        e.DrawFocusRectangle();
                    }
                }
            }
        }
        public void Fsplitno(ListBox lst)
        {
            int last = lst.SelectedItem.ToString().LastIndexOf("-");
            int first = 0;
            splitno = lst.SelectedItem.ToString().Substring(first, last);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (lst_workers.SelectedIndex >= 0)
            {
                if (wp == null || wp.Text == "")
                {
                    wp = new WorkerPanel();
                    Fsplitno(lst_workers);
                    wp.txt_sicilno.Text = splitno;
                    wp.WindowState = FormWindowState.Normal;
                    wp.Show();
                }
                else if (dll.CheckOpened(wp.Text))
                {
                    Fsplitno(lst_workers);
                    wp.txt_sicilno.Text = splitno;
                    wp.WindowState = FormWindowState.Normal;
                    wp.Show();
                    wp.Focus();
                }
                CallWorkers();
            }
        }

        private void cmb_shop_Click(object sender, EventArgs e)
        {
            cmb_shop.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_shop_DragDrop(object sender, DragEventArgs e)
        {
            cmb_shop.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_shop_Enter(object sender, EventArgs e)
        {
            cmb_shop.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_shop_SelectedIndexChanged(object sender, EventArgs e)
        {
            CallWorkers();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManPower.ActiveForm.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
            //string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            //if (e.RowIndex >= 0)
            //{
            //    if (columnName == "add")
            //    {
            //        dll.DataGridViewToInsertDBTable_RowIndex(Properties.Settings.Default.cnn, dataGridView1, "tbl_Vukat");
            //    }
            //    else if (columnName == "update")
            //    {
            //        MessageBox.Show("Güncelleme");
            //    }
            //    else if (columnName == "remove")
            //    {
            //        MessageBox.Show("SILENECEK");
            //    }
            //}
        }
    }
}