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
    public partial class RaporEngel : Form
    {
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private int cnt;
        private string sql = null;
        public bool fmain= false;
        FromDLL dll = new FromDLL();
        //DISLASTIK ds = new DISLASTIK();
        public RaporEngel()
        {
            InitializeComponent();
        }
        public void RAPORONAY_QueryToCheckedListBox(string cndi)
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = "Select NameOfSection from st_Reports where TypeOfReport='" + cndi + "' order by ID";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            clb_raporonay.Items.Clear();
            while (dataReader.Read())
            {
                clb_raporonay.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        public void RAPORENGEL_UpToDate(string cndi)
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = "Select NameOfSection,Active from cn_RaporEngel where Date='" + dateTimePicker_RaporOnayDate.Value.ToString("dd.MM.yyyy") + "' and TypeOfReport='" + cndi + "' order by ID";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            clb_raporonay.Items.Clear();
            cnt = -1;
            while (dataReader.Read())
            {
                cnt = cnt + 1;
                clb_raporonay.Items.Add(dataReader[0].ToString());
                if (dataReader[1].ToString() == "True") { clb_raporonay.SetItemChecked(cnt, true); } else { clb_raporonay.SetItemChecked(cnt, false); }
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        private void dateTimePicker_RaporOnayDate_ValueChanged_1(object sender, EventArgs e)
        {
                clb_raporonay.Items.Clear();
                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct TypeOfReport from cn_RaporEngel where Date='" + dateTimePicker_RaporOnayDate.Value.ToString("dd.MM.yyyy") + "'", cmb_reporttype, "TypeOfReport");
        }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb_raporonay.Items.Count; i++)
            {
                clb_raporonay.SetItemChecked(i, true);
            }
        }

        private void tümSeçiliOlanıKaldırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb_raporonay.Items.Count; i++)
            {
                clb_raporonay.SetItemChecked(i, false);
            }
        }

        private void cmb_reporttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_raporonay.Items.Clear();
            RAPORENGEL_UpToDate(cmb_reporttype.Text);
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct NameOfSection from st_Reports where TypeOfReport='" + cmb_reporttype.Text+"'", cmb_reportsection, "NameOfSection");
        }

        private void RaporEngel_Load(object sender, EventArgs e)
        {
             clb_raporonay.Items.Clear();
             dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct TypeOfReport from st_Reports", cmb_reporttype, "TypeOfReport");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show( cmb_reporttype.Text + " Raporunu engellemek istiyormusunuz ? " + cmb_reportsection.Text + " Kısım", "EKLE", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                clb_raporonay.Items.Add(cmb_reportsection.Text);
                clb_raporonay.SetItemChecked(this.clb_raporonay.Items.Count - 1, true);
                dll.UpdateDeleteInsertFunctions(
                    "IF NOT EXISTS(Select Date from cn_RaporEngel where Date='" + dateTimePicker_RaporOnayDate.Value.ToString("dd.MM.yyyy") + "' and TypeOfReport='" + cmb_reporttype.Text + "' and NameOfSection='" + cmb_reportsection.Text + "') "
                    + "BEGIN "
                    + "Insert into cn_RaporEngel "
                    + "(Date,typeOfReport,NameOfSection,Active) "
                    + "VALUES "
                    + "( "
                    + "'" + dateTimePicker_RaporOnayDate.Value.ToString("dd.MM.yyyy") + "','" + cmb_reporttype.Text + "','" + cmb_reportsection.Text + "','1' "
                    + ") "
                    + "END;"
                    , Properties.Settings.Default.cnn);

                MessageBox.Show("Rapor '" + cmb_reporttype.Text + " " + cmb_reportsection.Text + "' eklenmiştir.");
                RAPORENGEL_UpToDate(cmb_reporttype.Text);
                //ds.calculate_blockedreports();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void SilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(cmb_reporttype.Text + " Raporun engelini kaldırmak istiyormusunuz ? " + cmb_reportsection.Text + " Kısım", "SIL", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                    "delete c from cn_RaporEngel c where c.Date='" + dateTimePicker_RaporOnayDate.Value.ToString("dd.MM.yyyy") + "' and c.TypeOfReport='" + cmb_reporttype.Text + "' and c.NameOfSection='" + clb_raporonay.SelectedItem.ToString() + "'"
                    , Properties.Settings.Default.cnn);

                MessageBox.Show("Rapor '" + cmb_reporttype.Text + " " + cmb_reportsection.Text + "' engeli kaldırılmıştır.");
                RAPORENGEL_UpToDate(cmb_reporttype.Text);
                //ds.calculate_blockedreports();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void tümünüSILToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(cmb_reporttype.Text + " Tüm Rapor engellerini kaldırmak istiyormusunuz ? " + cmb_reporttype.Text + " HEPSINI SILMEK ISTIYOR MUSUN?", "SIL", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                    for (int i=0; i<clb_raporonay.Items.Count;i++)
                {
                    dll.UpdateDeleteInsertFunctions(
                    "delete c from cn_RaporEngel c where c.Date='" + dateTimePicker_RaporOnayDate.Value.ToString("dd.MM.yyyy") + "' and c.TypeOfReport='" + cmb_reporttype.Text + "' and c.NameOfSection='" + clb_raporonay.Items[i].ToString() + "'"
                    , Properties.Settings.Default.cnn);
                }
                RAPORENGEL_UpToDate(cmb_reporttype.Text);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
        //public void withoutduplicate()
        //{
        //    List<ListItem> itemsToAdd = new List<ListItem>();
        //    foreach (CheckedListBox itemToAdd in itemsToAdd)
        //    {
        //        if (selectedItems.Contains(itemToAdd)) continue;
        //        lstBoxToUserProjects.Items.Add(itemToAdd);
        //    }
        //}
    }
}