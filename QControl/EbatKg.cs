using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QControl
{
    public partial class EbatKg : Form
    {
        public string txtcode;
        public string txtebat;
        public string txtrapor;
        public string txtgrup;
        public string txtgram;
        public string txtlastiktype;
        LastikTable lt = null;
        FromDLL dll = new FromDLL();
        public EbatKg()
        {
            InitializeComponent();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EbatKg.ActiveForm.Close();
        }

        private void cmb_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryToTableEbat(cmb_code, "cn_EbatKG",dg_qcebat);
            QueryToTableEbat(cmb_code, "alp_EbatKG",dg_alpataebat);
            QueryToTableEbat(cmb_code, "lnup_EbatKG", dg_lineupebat);
        }
        public void QueryToTableEbat(ComboBox cm, string tb, DataGridView dg)
        {
            if (cm.Text != string.Empty)
            {
                dll.QueryToDataGrid(dg, Properties.Settings.Default.cnn, "Select * from " + tb + " p where p.Code='" + cmb_code.Text + "' ");
            }
        }

        private void EbatKg_Activated(object sender, EventArgs e)
        {
            QueryToTableEbat(cmb_code, "cn_EbatKG", dg_qcebat);
            QueryToTableEbat(cmb_code, "alp_EbatKG", dg_alpataebat);
            QueryToTableEbat(cmb_code, "lnup_EbatKG", dg_lineupebat);
        }
        public void DataGridAlpata_ToTExtbox(DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {
                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                txtcode = Convert.ToString(selectedRow.Cells["Code"].Value);
                txtebat = Convert.ToString(selectedRow.Cells["Ebatlar"].Value);
                txtrapor = Convert.ToString(selectedRow.Cells["ReportType"].Value);
                txtgrup = Convert.ToString(selectedRow.Cells["GroupCode"].Value);
                txtlastiktype = Convert.ToString(selectedRow.Cells["LastikType"].Value);
                txtgram = Convert.ToString(selectedRow.Cells["TotalGR"].Value);
            }
        }
        public void DataGridLineup_ToTExtbox(DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {
                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                txtcode = Convert.ToString(selectedRow.Cells["Code"].Value);
                txtebat = Convert.ToString(selectedRow.Cells["Ebatlar"].Value);
                txtrapor = "";
                txtgrup = "";
                txtlastiktype = Convert.ToString(selectedRow.Cells["LastikType"].Value);
                txtgram = Convert.ToString(selectedRow.Cells["TotalGR"].Value);
            }
        }
        public void DataGridQCEbat_RemoveCode(DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {
                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                txtcode = Convert.ToString(selectedRow.Cells["Code"].Value);
            }
        }
        private void dg_alpataebat_DoubleClick(object sender, EventArgs e)
        {
            if (dg_alpataebat.RowCount>0)
            {
                if (lt == null || lt.Text == "")
                {
                    lt = new LastikTable();
                    DataGridAlpata_ToTExtbox(dg_alpataebat);
                    lt.txt_lastik.Text = txtcode;
                    lt.txt_ebat.Text = txtebat;
                    lt.txt_rapor.Text = txtrapor;
                    lt.txt_grup.Text = txtgrup;
                    lt.txt_yer.Text = txtlastiktype;
                    lt.txt_gr.Text = txtgram;
                    lt.WindowState = FormWindowState.Normal;
                    lt.Show();
                }
                else if (dll.CheckOpened(lt.Text))
                {
                    DataGridAlpata_ToTExtbox(dg_alpataebat);
                    lt.txt_lastik.Text = txtcode;
                    lt.txt_ebat.Text = txtebat;
                    lt.txt_rapor.Text = txtrapor;
                    lt.txt_grup.Text = txtgrup;
                    lt.txt_yer.Text = txtlastiktype;
                    lt.txt_gr.Text = txtgram;
                    lt.WindowState = FormWindowState.Normal;
                    lt.Show();
                    lt.Focus();
                }
            }
        }

        private void dg_lineupebat_DoubleClick(object sender, EventArgs e)
        {
            if (dg_lineupebat.RowCount > 0)
            {
                if (lt == null || lt.Text == "")
                {
                    lt = new LastikTable();
                    DataGridLineup_ToTExtbox(dg_lineupebat);
                    lt.txt_lastik.Text = txtcode;
                    lt.txt_ebat.Text = txtebat;
                    lt.txt_rapor.Text = txtrapor;
                    lt.txt_grup.Text = txtgrup;
                    lt.txt_yer.Text = txtlastiktype;
                    lt.txt_gr.Text = txtgram;
                    lt.WindowState = FormWindowState.Normal;
                    lt.Show();
                }
                else if (dll.CheckOpened(lt.Text))
                {
                    DataGridLineup_ToTExtbox(dg_lineupebat);
                    lt.txt_lastik.Text = txtcode;
                    lt.txt_ebat.Text = txtebat;
                    lt.txt_rapor.Text = txtrapor;
                    lt.txt_grup.Text = txtgrup;
                    lt.txt_yer.Text = txtlastiktype;
                    lt.txt_gr.Text = txtgram;
                    lt.WindowState = FormWindowState.Normal;
                    lt.Show();
                    lt.Focus();
                }
            }
        }

        private void eKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lt == null || lt.Text == "")
            {
                lt = new LastikTable();
                lt.txt_lastik.Text = cmb_code.Text;
                lt.txt_lastik.ReadOnly= false;
                lt.txt_ebat.ReadOnly = false;
                lt.txt_rapor.ReadOnly = false;
                lt.txt_grup.ReadOnly = false;
                lt.txt_yer.ReadOnly = false;
                lt.txt_gr.ReadOnly = false;
                lt.WindowState = FormWindowState.Normal;
                lt.Show();
            }
            else if (dll.CheckOpened(lt.Text))
            {
                lt.txt_lastik.Text = cmb_code.Text;
                lt.txt_lastik.ReadOnly = false;
                lt.txt_ebat.ReadOnly = false;
                lt.txt_rapor.ReadOnly = false;
                lt.txt_grup.ReadOnly = false;
                lt.txt_yer.ReadOnly = false;
                lt.txt_gr.ReadOnly = false;
                lt.WindowState = FormWindowState.Normal;
                lt.Show();
                lt.Focus();
            }
        }
        public void Delete_QcEbatKg(string cond)
        {
                dll.UpdateDeleteInsertFunctions("delete t "
                    + " from cn_EbatKG t where t.Code=LTRIM(RTRIM('" + cond + "')) "
                    , Properties.Settings.Default.cnn);
        }
        private void sILToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridQCEbat_RemoveCode(dg_qcebat);
            DialogResult dialogResult = MessageBox.Show(txtcode + "Lastik Silinecek !", "QC EBAT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Delete_QcEbatKg(txtcode);
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
    }
}