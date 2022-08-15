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
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace QControl
{
    public partial class VerimlilikVsPLC : Form
    {
        public string filepath;
        FromDLL dll = new FromDLL();
        public VerimlilikVsPLC()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerimlilikVsPLC.ActiveForm.Close();
        }
        public void SelectQueryToDataGridView(DataGridView dg)
        {
            dll.QueryToDataGrid(dg, Properties.Settings.Default.cnnSQL,
                " EXEC VerimlilikVsPLC @dt='" + VerimPLC_date.Value.ToString("dd.MM.yyyy") + "' "
                );
            Application.EnableVisualStyles();
            dll.ReDesignDataGridViewCELLFormat(dg, Color.LightSteelBlue, Color.White, Color.DarkBlue, Color.LightBlue);
            //design options
            dg.ForeColor = Color.Black;
        }
        private void VerimlilikVsPLC_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(VerimPLC_date.Value.ToString("dd.MM.yyyy") + " verimlilik raporuna göre PLC Presler ile karışılaştırma istiyor musun ?", "Verimlilik - PLC Pres", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                SelectQueryToDataGridView(dg_verimplc);
                fillChart();
                lbl_time.Text = System.DateTime.Now.ToString("HH:mm:ss");
                MessageBox.Show("KARSILASTIRMA TAMAMLANDI.", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
        private void fillChart()
        {
            int chartlimit = chart1.Series.Count;
            for (int i=0;i< chartlimit; i++)
            {
                chart1.Series.RemoveAt(0);
            }
            int charttitle = chart1.Titles.Count;
            for (int i = 0; i < charttitle; i++)
            {
                chart1.Titles.RemoveAt(0);
            }

            chart1.DataSource = null;
            chart1.ChartAreas["ChartArea1"].AxisY.IsLabelAutoFit = true;
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            SqlConnection con = new SqlConnection(Properties.Settings.Default.cnnSQL);
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter(" select Press "
                                                        + " ,SUM(ISNULL(PlaniningProd,0)) as PLAN_Toplam "
                                                        + " ,SUM(ISNULL(PlcProd,0)) as PLC_Toplam "
                                                        + " from dbo.fxnVerimlilikVsPLC ('" + VerimPLC_date.Value.ToString("dd.MM.yyyy") + "')  "
                                                        + " group by Press "
                                                        + " order by dbo.fn_CreateAlphanumericSortValue(Press) "
                                                      , con
                                                      );
            adapt.Fill(ds);
            chart1.DataSource = ds;
            chart1.Series.Add("PLAN");
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["PLAN"].XValueMember = "Press";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["PLAN"].YValueMembers = "PLAN_Toplam";
            chart1.Series.Add("PLC");
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["PLC"].XValueMember = "Press";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["PLC"].YValueMembers = "PLC_Toplam";
            chart1.Titles.Add("KARSILASTIRMA Chart");
            con.Close();
        }

        private void chk_canli_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_canli.Checked==true)
            {
                VerimPLC_date.Enabled = false;
                button1.Enabled = false;
                timer1.Start();
            }
            else
            {
                VerimPLC_date.Enabled = true;
                button1.Enabled = true;
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chk_canli.Checked == true)
            {
                SelectQueryToDataGridView(dg_verimplc);
                fillChart();
                lbl_time.Text = System.DateTime.Now.ToString("HH:mm:ss");
            }
        }

        private void emailHazırlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Dt = DateTime.Now.ToString("yyyy-MM-dd");
            string promptValue = "Verimlilik vs PLC " + " @" + Dt;
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.FileName = promptValue;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                filepath = Path.GetDirectoryName(fdlg.FileName);
                if (fdlg.FileName != null)
                {
                    dll.ExportExcel(fdlg.FileName, dg_verimplc);
                    Application.DoEvents();
                    dll.SendEmail(filepath + "\\" + promptValue + ".xlsx", promptValue, "", "", "");
                }
            }
        }

        private void oUTLOOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Dt = DateTime.Now.ToString("yyyy-MM-dd");
            string promptValue = "Verimlilik vs PLC " + " @" + Dt;
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.FileName = promptValue;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                filepath = Path.GetDirectoryName(fdlg.FileName);
                if (fdlg.FileName != null)
                {
                    dll.ExportExcel(fdlg.FileName, dg_verimplc);
                    Application.DoEvents();
                }
            }
        }
    }
}
