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
using Microsoft.Reporting.WinForms;

namespace QControl
{
    public partial class Bitirme : Form
    {
        public Bitirme()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitirme.ActiveForm.Close();
        }
        public void bitirme()
        {
            bitirme_Dataset g = new bitirme_Dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                    " select * from vbitirme order by SAAT asc "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("bitirme_Dataset", g.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        private void gUNCELLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bitirme();
        }

        private void Bitirme_Load(object sender, EventArgs e)
        {
            bitirme();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bitirme();
        }
    }
}
