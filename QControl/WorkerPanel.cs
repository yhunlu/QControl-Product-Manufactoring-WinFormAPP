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
    public partial class WorkerPanel : Form
    {
        ManPower mp = new ManPower();
        FromDLL dll = new FromDLL();
        public WorkerPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Personel Eklensin mi?", "EKLE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                 " INSERT into cn_workers ( "
                + " SICILNO,ADI,SOYADI,Fabrika,Pozisyon "
                + " ) "
                + " select  "
                + " LTRIM(RTRIM('" + txt_sicilno.Text + "')),LTRIM(RTRIM('" + txt_adi.Text + "')),LTRIM(RTRIM('" + txt_soyadi.Text + "')),LTRIM(RTRIM('" + txt_fabrika.Text + "')),LTRIM(RTRIM('" + txt_pozisyon.Text + "')) "
                , Properties.Settings.Default.cnn
                );
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Personel Güncellensin mi?", "GUNCELLE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                 " UPDATE cn_workers SET "
                + " ADI=LTRIM(RTRIM('" + txt_adi.Text + "')),SOYADI=LTRIM(RTRIM('" + txt_soyadi.Text + "')),Fabrika=LTRIM(RTRIM('" + txt_fabrika.Text + "')),Pozisyon=LTRIM(RTRIM('" + txt_pozisyon.Text + "')) "
                + " From cn_workers where SICILNO=LTRIM(RTRIM('" + txt_sicilno.Text + "')) "
                , Properties.Settings.Default.cnn
                );
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Personel Silinsin mi?", "SIL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                 " DELETE "
                + " From cn_workers where SICILNO=LTRIM(RTRIM('" + txt_sicilno.Text + "')) "
                , Properties.Settings.Default.cnn
                );
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
    }
}
