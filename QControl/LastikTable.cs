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
    public partial class LastikTable : Form
    {
        EbatKg ek = new EbatKg();
        FromDLL dll = new FromDLL();
        public LastikTable()
        {
            InitializeComponent();
        }
        public void lastikTB_Insert(string tb)
        {

            dll.UpdateDeleteInsertFunctions(
                " Insert into " + tb + "( "
                + " Code, Ebatlar, ReportType, GroupCode, LastikType, TotalGR "
                + " ) "
                + " select  "
                + " LTRIM(RTRIM('" + txt_lastik.Text + "')),LTRIM(RTRIM('" + txt_ebat.Text + "')),LTRIM(RTRIM('" + txt_rapor.Text + "')),LTRIM(RTRIM('" + txt_grup.Text + "')),LTRIM(RTRIM('" + txt_yer.Text + "')),'" + txt_gr.Text.Replace(',', '.') + "' "
                , Properties.Settings.Default.cnn);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("LASTIK veri alanına eklenicek!", "LASTIK EKLE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                lastikTB_Insert("cn_EbatKG");
                dll.QueryToDataGrid(ek.dg_qcebat, Properties.Settings.Default.cnn, "Select * from cn_EbatKG where Code=LTRIM(RTRIM('" + ek.cmb_code.Text + "'))");
                LastikTable.ActiveForm.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }
    }
}
