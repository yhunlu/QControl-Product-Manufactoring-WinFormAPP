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
    public partial class TyreControl : Form
    {
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private string sql = null;
        private int cnt;
        FromDLL dll = new FromDLL();
        public TyreControl()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private void TyreControl_Load(object sender, EventArgs e)
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Code from cn_EbatKG order by dbo.fn_CreateAlphanumericSortValue(Code)", cmb_eklelastik, "Code");
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Press from cn_PressIDs order by dbo.fn_CreateAlphanumericSortValue(Press)", cmb_eklepress, "Press");
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Code from cn_HurdaOzurCode order by dbo.fn_CreateAlphanumericSortValue(Code)", cmb_ekleozurneden, "Code");
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Code from cn_HurdaOzurCode order by dbo.fn_CreateAlphanumericSortValue(Code)", cmb_eklehurdaneden, "Code");
            cmb_eklelastik.Text = cmb_lastikcontrol.Text;
            cmb_eklepress.Text = cmb_presscontrol.Text;
            ozurneden();
            hurdaneden();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TyreControl.ActiveForm.Close();
        }

        private void dateTimePicker_bitirme_ValueChanged(object sender, EventArgs e)
        {
            cmb_tarihcontrol.Text = dateTimePicker_bitirme.Value.ToString("dd.MM.yyyy");

            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                " Select Code "
                + " From  "
                + " ( "
                + "     select distinct Code from tbl_DislastikProduction where Date='" + cmb_tarihcontrol.Text + "' "
                + "     union "
                + "     select distinct Code from tbl_DislastikOzur where Date='" + cmb_tarihcontrol.Text + "' "
                + "     union "
                + "     select distinct Code from tbl_DislastikHurda where Date='" + cmb_tarihcontrol.Text + "' "
                + " ) i "
                + " order by dbo.fn_CreateAlphanumericSortValue(Code) "
            , cmb_lastikcontrol, "Code");
        }

        private void cmb_lastikcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
            " Select Press "
            + " From  "
            + " ( "
            + "     select distinct Press from tbl_DislastikProduction where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' "
            + "     union "
            + "     select distinct Press from tbl_DislastikOzur where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' "
            + "     union "
            + "     select distinct Press from tbl_DislastikHurda where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' "
            + " ) i "
            + " order by dbo.fn_CreateAlphanumericSortValue(Press) "
            , cmb_presscontrol, "Press");
            cmb_eklelastik.Text = cmb_lastikcontrol.Text;
        }
        public void ozurneden()
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " Select NedenCode "
            + " From  "
            + " ( "
            + "     select distinct NedenCode from tbl_DislastikOzur where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' and Press='" + cmb_presscontrol.Text + "' "
            + " ) i "
            + " order by dbo.fn_CreateAlphanumericSortValue(NedenCode) ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            lst_ozurneden.Items.Clear();
            cnt = 0;
            while (dataReader.Read())
            {
                cnt = cnt + 1;

                lst_ozurneden.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            lbl_ozurnedenadet.Text = cnt.ToString();
        }
        public void hurdaneden()
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = " Select NedenCode "
            + " From  "
            + " ( "
            + "     select distinct NedenCode from tbl_DislastikHurda where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' and Press='" + cmb_presscontrol.Text + "' "
            + " ) i "
            + " order by dbo.fn_CreateAlphanumericSortValue(NedenCode) ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            lst_hurdaneden.Items.Clear();
            cnt = 0;
            while (dataReader.Read())
            {
                cnt = cnt + 1;

                lst_hurdaneden.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            lbl_hurdanedenadet.Text = cnt.ToString();
        }
        private void cmb_presscontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
            "Select case when CiftKalip=1 then 'EVET' else 'HAYIR' end as CiftKalip from tbl_DislastikProduction " +
            "where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' " +
            "and Press='" + cmb_presscontrol.Text + "' "
            , cmb_ciftkalip, "CiftKalip");

            dll.SqlToNumericUpDown(
            "Select cast(isnull(ProdQty,0) as real) as Prod from tbl_DislastikProduction " +
            "where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' " +
            "and Press='" + cmb_presscontrol.Text + "' "
            , Properties.Settings.Default.cnn, nud_uretim);
            ozurneden();
            hurdaneden();
            cmb_eklepress.Text = cmb_presscontrol.Text;
        }

        private void lst_ozurneden_Click(object sender, EventArgs e)
        {
            if (lst_ozurneden.SelectedItem != null)
            {
                dll.SqlToNumericUpDown(
                "Select cast(isnull(OzurQty,0) as real) as OzurQty from tbl_DislastikOzur " +
                "where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' " +
                "and Press='" + cmb_presscontrol.Text + "' " +
                "and NedenCode='" + lst_ozurneden.SelectedItem.ToString() + "' "
                , Properties.Settings.Default.cnn, nd_ozursayi);

                dll.SqlToNumericUpDown(
                "Select cast(isnull(DA,0) as real) as DAQty from tbl_DislastikOzur " +
                "where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' " +
                "and Press='" + cmb_presscontrol.Text + "' " +
                "and NedenCode='" + lst_ozurneden.SelectedItem.ToString() + "' "
                , Properties.Settings.Default.cnn, nd_dasayi);

                dll.SqlToNumericUpDown(
                "Select cast(isnull(TT,0) as real) as TTQty from tbl_DislastikOzur " +
                "where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' " +
                "and Press='" + cmb_presscontrol.Text + "' " +
                "and NedenCode='" + lst_ozurneden.SelectedItem.ToString() + "' "
                , Properties.Settings.Default.cnn, nd_ttsayi);

                cmb_ekleozurneden.Text = lst_ozurneden.SelectedItem.ToString();
            }
        }

        private void lst_hurdaneden_Click(object sender, EventArgs e)
        {
            if (lst_hurdaneden.SelectedItem != null)
            {
                dll.SqlToNumericUpDown(
                "Select cast(isnull(HurdaQty,0) as real) as HurdaQty from tbl_DislastikHurda " +
                "where Date='" + cmb_tarihcontrol.Text + "' and Code='" + cmb_lastikcontrol.Text + "' " +
                "and Press='" + cmb_presscontrol.Text + "' " +
                "and NedenCode='" + lst_hurdaneden.SelectedItem.ToString() + "' "
                , Properties.Settings.Default.cnn, nd_hurdasayi);

                cmb_eklehurdaneden.Text = lst_hurdaneden.SelectedItem.ToString();
            }
        }
        public void Delete_temp_Dislastik_BitirmeOzur_OneByOne()
        {
            dll.UpdateDeleteInsertFunctions(
                " delete t "
                + " from tbl_DislastikOzur t where t.Date=LTRIM(RTRIM('" + cmb_tarihcontrol.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_lastikcontrol.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_presscontrol.Text + "')) and t.NedenCode=LTRIM(RTRIM('" + lst_ozurneden.SelectedItem.ToString() + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Delete_temp_Dislastik_BitirmeHurda_OneByOne()
        {
            dll.UpdateDeleteInsertFunctions(
                " delete t "
                + " from tbl_DislastikHurda t where t.Date=LTRIM(RTRIM('" + cmb_tarihcontrol.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_lastikcontrol.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_presscontrol.Text + "')) and t.NedenCode=LTRIM(RTRIM('" + lst_hurdaneden.SelectedItem.ToString() + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Delete_temp_Dislastik_Pisim_OneByOne()
        {
            dll.UpdateDeleteInsertFunctions("delete t "
                + " from tbl_DislastikProduction t where t.Date=LTRIM(RTRIM('" + cmb_tarihcontrol.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_lastikcontrol.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_presscontrol.Text + "')) "
                , Properties.Settings.Default.cnn);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("TAMIR silinsin mi?", "TAMIR Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Delete_temp_Dislastik_BitirmeOzur_OneByOne();
                ozurneden();
                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("HURDA silinsin mi?", "HURDA Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Delete_temp_Dislastik_BitirmeHurda_OneByOne();
                hurdaneden();
                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("LASTIK silinsin mi?", "LASTIK Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Delete_temp_Dislastik_Pisim_OneByOne();
                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
        public void Update_temp_Dislastik_HurdaBitirme_OneByOne()
        {
            dll.UpdateDeleteInsertFunctions(
                " update tbl_DislastikHurda set "
                + " HurdaQty='" + nd_hurdasayi.Text + "' "
                + " from tbl_DislastikHurda t where t.Date=LTRIM(RTRIM('" + cmb_tarihcontrol.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_lastikcontrol.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_presscontrol.Text + "')) and t.NedenCode=LTRIM(RTRIM('" + lst_hurdaneden.SelectedItem.ToString() + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Update_temp_Dislastik_OzurBitirme_OneByOne()
        {
            dll.UpdateDeleteInsertFunctions(
                " update tbl_DislastikOzur set "
                + " OzurQty='" + nd_ozursayi.Text + "', "
                + " DA='" + nd_dasayi.Text + "', "
                + " TT='" + nd_ttsayi.Text + "' "
                + " from tbl_DislastikOzur t where t.Date=LTRIM(RTRIM('" + cmb_tarihcontrol.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_lastikcontrol.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_presscontrol.Text + "')) and t.NedenCode=LTRIM(RTRIM('" + lst_ozurneden.SelectedItem.ToString() + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Update_temp_Dislastik_Pisim_OneByOne()
        {
            dll.UpdateDeleteInsertFunctions(
                " update tbl_DislastikProduction set "
                + " ProdQty='" + nud_uretim.Text + "', "
                + " CiftKalip=Case when '" + cmb_ciftkalip.Text + "'='EVET' Then 1 else 0 end "
                + " from tbl_DislastikProduction t where t.Date=LTRIM(RTRIM('" + cmb_tarihcontrol.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_lastikcontrol.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_presscontrol.Text + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("LASTIK güncellensin mi?", "LASTIK Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Update_temp_Dislastik_Pisim_OneByOne();
                MessageBox.Show("Güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("TAMIR güncellensin mi?", "TAMIR Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Update_temp_Dislastik_OzurBitirme_OneByOne();
                MessageBox.Show("Güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("HURDA güncellensin mi?", "HURDA Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Update_temp_Dislastik_HurdaBitirme_OneByOne();
                MessageBox.Show("Güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void cmb_eklelastik_Click(object sender, EventArgs e)
        {
            cmb_eklelastik.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklelastik_DropDown(object sender, EventArgs e)
        {
            cmb_eklelastik.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklelastik_Enter(object sender, EventArgs e)
        {
            cmb_eklelastik.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklepress_Click(object sender, EventArgs e)
        {
            cmb_eklepress.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklepress_DropDown(object sender, EventArgs e)
        {
            cmb_eklepress.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklepress_Enter(object sender, EventArgs e)
        {
            cmb_eklepress.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_ekleozurneden_Click(object sender, EventArgs e)
        {
            cmb_ekleozurneden.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_ekleozurneden_DropDown(object sender, EventArgs e)
        {
            cmb_ekleozurneden.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_ekleozurneden_Enter(object sender, EventArgs e)
        {
            cmb_ekleozurneden.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklehurdaneden_Click(object sender, EventArgs e)
        {
            cmb_eklehurdaneden.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklehurdaneden_DropDown(object sender, EventArgs e)
        {
            cmb_eklehurdaneden.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_eklehurdaneden_Enter(object sender, EventArgs e)
        {
            cmb_eklehurdaneden.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void Insert_tbl_DislastikProduction_Pisim()
        {
            if (Int32.Parse(nud_uretim.Text) > 0)
            {
                dll.UpdateDeleteInsertFunctions(
                " insert tbl_DislastikProduction "
                + " Select  "
                + " '" + cmb_tarihcontrol.Text + "','" + cmb_eklelastik.Text + "','" + cmb_eklepress.Text + "','" + nud_uretim.Text + "',Case when '" + cmb_ciftkalip.Text + "'='EVET' Then 1 else 0 end "
                , Properties.Settings.Default.cnn
                );
            }
            else
            {
                MessageBox.Show("Değer Girmedin !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Insert_tbl_DislastikOzur()
        {
            if (Int32.Parse(nd_ozursayi.Text) > 0 || Int32.Parse(nd_dasayi.Text) > 0 || Int32.Parse(nd_ttsayi.Text) > 0)
            {
                dll.UpdateDeleteInsertFunctions(
                    " insert tbl_DislastikOzur "
                    + " select '" + cmb_tarihcontrol.Text + "','" + cmb_eklelastik.Text + "','" + cmb_eklepress.Text + "','" + cmb_ekleozurneden.Text + "','" + nd_ozursayi.Text + "','" + nd_dasayi.Text + "','" + nd_ttsayi.Text + "' "
                    , Properties.Settings.Default.cnn
                );
            }
            else
            {
                MessageBox.Show("Değer Girmedin !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Insert_tbl_DislastikHurda()
        {
            if (Int32.Parse(nd_hurdasayi.Text) > 0)
            {
                dll.UpdateDeleteInsertFunctions(
                    " insert tbl_DislastikHurda "
                    + " select '" + cmb_tarihcontrol.Text + "','" + cmb_eklelastik.Text + "','" + cmb_eklepress.Text + "','" + cmb_eklehurdaneden.Text + "','" + nd_hurdasayi.Text + "' "
                    , Properties.Settings.Default.cnn
                );
            }
            else
            {
                MessageBox.Show("Değer Girmedin !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("LASTIK eklensin mi?", "LASTIK EKLEME", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Insert_tbl_DislastikProduction_Pisim();
                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                    " Select Code "
                    + " From  "
                    + " ( "
                    + "     select distinct Code from tbl_DislastikProduction where Date='" + cmb_tarihcontrol.Text + "' "
                    + "     union "
                    + "     select distinct Code from tbl_DislastikOzur where Date='" + cmb_tarihcontrol.Text + "' "
                    + "     union "
                    + "     select distinct Code from tbl_DislastikHurda where Date='" + cmb_tarihcontrol.Text + "' "
                    + " ) i "
                    + " order by dbo.fn_CreateAlphanumericSortValue(Code) "
                , cmb_lastikcontrol, "Code");
                if (Int32.Parse(nud_uretim.Text) > 0) { MessageBox.Show("Eklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("TAMIR eklensin mi?", "TAMIR EKLEME", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Insert_tbl_DislastikOzur();
                ozurneden();
                MessageBox.Show("Eklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("HURDA eklensin mi?", "HURDA EKLEME", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Insert_tbl_DislastikHurda();
                hurdaneden();
                MessageBox.Show("Eklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.DoEvents();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
    }
}
