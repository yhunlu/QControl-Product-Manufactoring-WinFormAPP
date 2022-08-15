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
    public partial class IKINCIKALITE : Form
    {
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private string sql = null;
        public string cmbDate;
        public string cmbCode;
        public string cmbPress;
        public string cmbNedenCode;
        FromDLL dll = new FromDLL();
        TyreControl tc = null;
        public IKINCIKALITE()
        {
            InitializeComponent();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IKINCIKALITE.ActiveForm.Close();
        }
        public void SelectQueryToDataGridView(string tb, DataGridView dg)
        {
            dll.QueryToDataGrid(dg, Properties.Settings.Default.cnn,
                "Select * from " + tb + " order by HATA desc"
                );
            Application.EnableVisualStyles();
            dll.ReDesignDataGridViewCELLFormat(dg, Color.LightSteelBlue, Color.White, Color.DarkBlue, Color.LightBlue);
            //design options
            dg.ForeColor = Color.Black;
        }
        public void rightclick()
        {
            if (tc_DATT.SelectedTab == tabPage5)
            {
                SelectQueryToDataGridView("temp_IkinciKaliteDATT_OZKA1", dg_bitirmeO1);
            }
            else if (tc_DATT.SelectedTab == tabPage6)
            {
                SelectQueryToDataGridView("temp_IkinciKaliteDATT_OZKA2", dg_bitirmeO2);
            }
            //calculate_blockedreports();
        }
        private void IKINCIKALITE_Load(object sender, EventArgs e)
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Location from st_Location order by dbo.fn_CreateAlphanumericSortValue(Location)", cmb_loc, "Location");
            tabPage5.Text = @"temp_IkinciKaliteDATT_OZKA1";
            tabPage6.Text = @"temp_IkinciKaliteDATT_OZKA2";
            rightclick();
        }
        public void Insert_temp_Dislastik_IkinciKaliteTT_OneByOne(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " Insert into " + tb + "( "
                + " Date,Time,Press,Code,NedenCode,DA,TT,HATA "
                + " ) "
                + " select  "
                + " LTRIM(RTRIM('" + cmb_date.Text + "')),'" + System.DateTime.Now.ToString("HH:mm") + "',LTRIM(RTRIM('" + cmb_press.Text + "')),LTRIM(RTRIM('" + cmb_code.Text + "')),LTRIM(RTRIM('" + lst_ozurneden.SelectedItem.ToString() + "')),LTRIM(RTRIM('" + nd_dasayi.Text + "')),LTRIM(RTRIM('" + nd_ttsayi.Text + "')),'' "
                , Properties.Settings.Default.cnn
            );
        }
        public void EXEC_DATT(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " EXEC CHECKtemp_IkinciKaliteTTOzka @PartOfTable='" + tb + "' "
                , Properties.Settings.Default.cnn
            );
        }
        public void Update_DA_TT(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " update tbl_DislastikOzur set "
                + " DA=ISNULL(i.DA,0), "
                + " TT=ISNULL(i.TT,0) "
                + " from tbl_DislastikOzur o inner join " + tb + " i "
                + " on o.Date=i.Date and o.Code=i.Code and o.Press=i.Press and o.NedenCode=i.NedenCode "
                , Properties.Settings.Default.cnn
            );
            dll.UpdateDeleteInsertFunctions("Delete t from " + tb + " t where (t.HATA='' oR len(t.HATA)=0 oR isnull(t.HATA,'')='') ", Properties.Settings.Default.cnn);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (lst_ozurneden.SelectedItem != null)
            {
                if (Int32.Parse(nd_dasayi.Text) > 0 || Int32.Parse(nd_ttsayi.Text) > 0)
                {
                    if (cmb_loc.Text == "OZKA 1")
                    {
                        Insert_temp_Dislastik_IkinciKaliteTT_OneByOne("temp_IkinciKaliteDATT_OZKA1");
                        EXEC_DATT("temp_IkinciKaliteDATT_OZKA1");
                    }
                    else if (cmb_loc.Text == "OZKA 2")
                    {
                        Insert_temp_Dislastik_IkinciKaliteTT_OneByOne("temp_IkinciKaliteDATT_OZKA2");
                        EXEC_DATT("temp_IkinciKaliteDATT_OZKA2");
                    }
                    rightclick();
                    lst_ozurneden.SelectedItem = null;
                    foreach (Control x in this.groupBox1.Controls)
                    {
                        if (x is NumericUpDown)
                        {
                            ((NumericUpDown)x).Value = 0;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Değer Girmedin !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dateTimePicker_bitirme_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control x in this.groupBox3.Controls)
            {
                    if (x is ComboBox)
                    {
                        if (((ComboBox)x).Name != "cmb_loc")
                        {
                            ((ComboBox)x).DataSource = null;
                            ((ComboBox)x).Items.Clear();
                        }
                    }
            }
            cmb_date.Text = dateTimePicker_bitirme.Value.ToString("dd.MM.yyyy");

            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                " Select Code "
                + " From  "
                + " ( "
                + "     select distinct t.Code from tbl_DislastikOzur t inner join cn_PressIDs c on t.Press=c.Press  where c.Location='" + cmb_loc.Text + "' and t.Date='" + cmb_date.Text + "' "
                + " ) i "
                + " order by dbo.fn_CreateAlphanumericSortValue(Code) "
            , cmb_code, "Code");
        }

        private void cmb_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
            " Select Press "
            + " From  "
            + " ( "
            + "     select distinct t.Press from tbl_DislastikOzur t inner join cn_PressIDs c on t.Press=c.Press  where c.Location='" + cmb_loc.Text + "' and t.Date='" + cmb_date.Text + "' and t.Code='" + cmb_code.Text + "' "
            + " ) i "
            + " order by dbo.fn_CreateAlphanumericSortValue(Press) "
            , cmb_press, "Press");
            foreach (Control x in this.groupBox1.Controls)
            {
                if (x is NumericUpDown)
                {
                    ((NumericUpDown)x).Value = 0;
                }
            }
        }

        private void cmb_loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control x in this.groupBox3.Controls)
            {
                if (x is ComboBox)
                {
                    if (((ComboBox)x).Name != "cmb_loc")
                    {
                        ((ComboBox)x).DataSource = null;
                        ((ComboBox)x).Items.Clear();
                    }
                }
            }
            if (cmb_loc.Text=="OZKA 1")
            {
                tc_DATT.SelectedTab = tabPage5;
            }
            else if(cmb_loc.Text == "OZKA 2")
            {
                tc_DATT.SelectedTab = tabPage6;
            }
            rightclick();
            cmb_date.Text = dateTimePicker_bitirme.Value.ToString("dd.MM.yyyy");

            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                " Select Code "
                + " From  "
                + " ( "
                + "     select distinct t.Code from tbl_DislastikOzur t inner join cn_PressIDs c on t.Press=c.Press  where c.Location='" + cmb_loc.Text + "' and t.Date='" + cmb_date.Text + "' "
                + " ) i "
                + " order by dbo.fn_CreateAlphanumericSortValue(Code) "
            , cmb_code, "Code");
        }
        public void ozurneden()
        {
            lst_ozurneden.Items.Clear();
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " Select NedenCode "
            + " From  "
            + " ( "
            + "     select distinct NedenCode from tbl_DislastikOzur where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' and Press='" + cmb_press.Text + "' "
            + " ) i "
            + " order by dbo.fn_CreateAlphanumericSortValue(NedenCode) ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            lst_ozurneden.Items.Clear();
            while (dataReader.Read())
            {
                lst_ozurneden.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        private void cmb_press_SelectedIndexChanged(object sender, EventArgs e)
        {
            ozurneden();
        }

        private void lst_ozurneden_Click(object sender, EventArgs e)
        {
            if (lst_ozurneden.SelectedItem.ToString() != "")
            {
                dll.SqlToNumericUpDown(
                "Select cast(isnull(OzurQty,0) as real) as OzurQty from tbl_DislastikOzur " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' " +
                "and NedenCode='" + lst_ozurneden.SelectedItem.ToString() + "' "
                , Properties.Settings.Default.cnn, nd_ozursayi);

                dll.SqlToNumericUpDown(
                "Select cast(isnull(DA,0) as real) as DAQty from tbl_DislastikOzur " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' " +
                "and NedenCode='" + lst_ozurneden.SelectedItem.ToString() + "' "
                , Properties.Settings.Default.cnn, nd_dasayi);

                dll.SqlToNumericUpDown(
                "Select cast(isnull(TT,0) as real) as TTQty from tbl_DislastikOzur " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' " +
                "and NedenCode='" + lst_ozurneden.SelectedItem.ToString() + "' "
                , Properties.Settings.Default.cnn, nd_ttsayi);
            }
        }

        private void cmb_loc_Click(object sender, EventArgs e)
        {
            cmb_loc.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_loc_DragDrop(object sender, DragEventArgs e)
        {
            cmb_loc.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_loc_Enter(object sender, EventArgs e)
        {
            cmb_loc.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_date_Click(object sender, EventArgs e)
        {
            cmb_date.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_date_DropDown(object sender, EventArgs e)
        {
            cmb_date.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_date_Enter(object sender, EventArgs e)
        {
            cmb_date.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_code_Click(object sender, EventArgs e)
        {
            cmb_code.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_code_DropDown(object sender, EventArgs e)
        {
            cmb_code.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_code_Enter(object sender, EventArgs e)
        {
            cmb_code.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_press_Click(object sender, EventArgs e)
        {
            cmb_press.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_press_DropDown(object sender, EventArgs e)
        {
            cmb_press.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_press_Enter(object sender, EventArgs e)
        {
            cmb_press.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightclick();
        }
        public void Delete_temp_Dislastik_Bitirme_OneByOneCHOOSENDATAGRID(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " delete t "
                + " from " + tb + " t where t.Date=LTRIM(RTRIM('" + cmbDate + "')) and t.Code=LTRIM(RTRIM('" + cmbCode + "')) and t.Press=LTRIM(RTRIM('" + cmbPress + "')) and t.NedenCode=LTRIM(RTRIM('" + cmbNedenCode + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void DataGridBitirme_ToCombobox(DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {
                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                cmbDate = Convert.ToString(selectedRow.Cells["Date"].Value);
                cmbCode = Convert.ToString(selectedRow.Cells["Code"].Value);
                cmbPress = Convert.ToString(selectedRow.Cells["Press"].Value);
                cmbNedenCode = Convert.ToString(selectedRow.Cells["NedenCode"].Value);
            }
        }
        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sİLToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (tc_DATT.SelectedTab == tabPage5)
                {
                    if (dg_bitirmeO1.RowCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", "OZKA 1", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataGridBitirme_ToCombobox(dg_bitirmeO1);
                            Delete_temp_Dislastik_Bitirme_OneByOneCHOOSENDATAGRID("temp_IkinciKaliteDATT_OZKA1");

                            rightclick();
                            MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                    }
                }
                else if (tc_DATT.SelectedTab == tabPage6)
                {
                    if (dg_bitirmeO2.RowCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", "OZKA 2", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataGridBitirme_ToCombobox(dg_bitirmeO2);
                            Delete_temp_Dislastik_Bitirme_OneByOneCHOOSENDATAGRID("temp_IkinciKaliteDATT_OZKA2");

                            rightclick();
                            MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void rightclick_cleantable()
        {
            if (tc_DATT.SelectedTab == tabPage5)
            {
                dll.UpdateDeleteInsertFunctions("delete from temp_IkinciKaliteDATT_OZKA1", Properties.Settings.Default.cnn);
            }
            else if (tc_DATT.SelectedTab == tabPage6)
            {
                dll.UpdateDeleteInsertFunctions("delete from temp_IkinciKaliteDATT_OZKA2", Properties.Settings.Default.cnn);
            }
        }
        private void tabloyuTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabloyuTemizleToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                DialogResult dialogResult = MessageBox.Show("Tablodaki bütün veriler silinecek !", "VERI ALANI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    rightclick_cleantable();
                    rightclick();
                    MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void vERIKONTROLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vERIKONTROLToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (tc == null || tc.Text == "")
                {
                    tc = new TyreControl();
                    if (tc_DATT.SelectedTab == tabPage5)
                    {
                        if (dg_bitirmeO1.RowCount > 0)
                        {
                            DataGridBitirme_ToCombobox(dg_bitirmeO1);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            tc.cmb_ekleozurneden.Text = cmbNedenCode;
                            tc.ozurneden();
                            tc.hurdaneden();
                        }
                    }
                    if (tc_DATT.SelectedTab == tabPage6)
                    {
                        if (dg_bitirmeO2.RowCount > 0)
                        {
                            DataGridBitirme_ToCombobox(dg_bitirmeO2);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            tc.cmb_ekleozurneden.Text = cmbNedenCode;
                            tc.ozurneden();
                            tc.hurdaneden();
                        }
                    }
                    tc.WindowState = FormWindowState.Normal;
                    tc.Show();
                }
                else if (dll.CheckOpened(tc.Text))
                {
                    if (tc_DATT.SelectedTab == tabPage5)
                    {
                        if (dg_bitirmeO1.RowCount > 0)
                        {
                            DataGridBitirme_ToCombobox(dg_bitirmeO1);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            tc.cmb_ekleozurneden.Text = cmbNedenCode;
                            tc.ozurneden();
                            tc.hurdaneden();
                        }
                    }
                    if (tc_DATT.SelectedTab == tabPage6)
                    {
                        if (dg_bitirmeO2.RowCount > 0)
                        {
                            DataGridBitirme_ToCombobox(dg_bitirmeO2);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            tc.cmb_ekleozurneden.Text = cmbNedenCode;
                            tc.ozurneden();
                            tc.hurdaneden();
                        }
                    }
                    tc.WindowState = FormWindowState.Normal;
                    tc.Show();
                    tc.Focus();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void DATTSelectQueryToDataGridView(string tb, DataGridView dg)
        {
            dll.QueryToDataGrid(dg, Properties.Settings.Default.cnn,
                "Select * from " + tb + " order by HATA desc"
                );
            Application.EnableVisualStyles();
            dll.ReDesignDataGridViewCELLFormat(dg, Color.LightSteelBlue, Color.White, Color.DarkBlue, Color.LightBlue);
            //design options
            dg.ForeColor = Color.Black;
        }
        private void btn_yukle_Click(object sender, EventArgs e)
        {
            if (btn_yukle.ForeColor == Color.FromArgb(192, 255, 192))
            {
                if (tc_DATT.SelectedTab == tabPage5)
                {
                        DialogResult dialogResult = MessageBox.Show("IKINCI KALITE OZKA1 Gercek veri alanına eklenicek!", "IKINCI KALITE OZKA1 Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Update_DA_TT("temp_IkinciKaliteDATT_OZKA1");
                            
                            DATTSelectQueryToDataGridView("temp_IkinciKaliteDATT_OZKA1", dg_bitirmeO1);
                            MessageBox.Show("Veri yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                        }
                        else if (dialogResult == DialogResult.No)
                        {

                        }
                }
                if (tc_DATT.SelectedTab == tabPage6)
                {
                        DialogResult dialogResult = MessageBox.Show("IKINCI KALITE OZKA2 Gercek veri alanına eklenicek!", "IKINCI KALITE OZKA2 Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Update_DA_TT("temp_IkinciKaliteDATT_OZKA2");

                            DATTSelectQueryToDataGridView("temp_IkinciKaliteDATT_OZKA2", dg_bitirmeO2);
                            MessageBox.Show("Veri yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                        }
                        else if (dialogResult == DialogResult.No)
                        {

                        }
                }
            }
            else if (btn_yukle.ForeColor == System.Drawing.Color.DarkRed)
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tc_DATT_Selected(object sender, TabControlEventArgs e)
        {
            if (tc_DATT.SelectedTab == tabPage5)
            {
                cmb_loc.SelectedIndex = 0;
            }
            if (tc_DATT.SelectedTab == tabPage6)
            {
                cmb_loc.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmb_loc.Text == "OZKA 1")
            {
                EXEC_DATT("temp_IkinciKaliteDATT_OZKA1");
            }
            else if (cmb_loc.Text == "OZKA 2")
            {
                EXEC_DATT("temp_IkinciKaliteDATT_OZKA2");
            }
            rightclick();
        }
    }
}