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
using System.Data.Odbc;
using Microsoft.Reporting.WinForms;

namespace QControl
{
    public partial class PLCvsVerimlilik : Form
    {
        public PLCvsVerimlilik()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PLCvsVerimlilik.ActiveForm.Close();
        }
        public void plcverimlilik()
        {
            ReportParameter r1 = new ReportParameter("StartDate", PlcVerim_date.Value.ToString("dd.MM.yyyy"));

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { r1});
            this.reportViewer1.LocalReport.Refresh();
            plcverimlilik_dataset g = new plcverimlilik_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                    " EXEC VerimlilikVsPLC @dt='" + PlcVerim_date.Value.ToString("dd.MM.yyyy") + "' "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("plcverimlilik_dataset", g.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        private void PLCvsVerimlilik_Load(object sender, EventArgs e)
        {

        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            plcverimlilik();
            plc_livedata(reportViewer9,"CANLI PRES VERİLERİ");
        }
        public void plc_livedata(ReportViewer rp, string sub)
        {
            ReportParameter r1 = new ReportParameter("StartDate", PlcVerim_date.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r3 });
            rp.LocalReport.Refresh();
            PlcLive_dataset g = new PlcLive_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
                SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10) "
                    + " set @dt= '" + PlcVerim_date.Value.ToString("dd.MM.yyyy") + "' "
                    + " SELECT [Start_Time] as StartTime "
                    + "       ,[Time] AS FinishTime "
                    + "       ,[Tyre_Code] "
                    + "       ,[Pres_Name] "
                    + "       ,[Steam_Time_AV] "
                    + "       ,[NO2_Time_AV] "
                    + "       ,[Tahliye_Time_AV] "
                    + "       ,[Cooling_Time_AV] "
                    + "       ,[Egsos_Time_AV] "
                    + "       ,[Total_Time_AV] "
                    + "       ,[NO2_Temperature_AV] "
                    + "       ,[Mold_Code] "
                    + "       ,[Bladder_Temp_Avg] "
                    + "       ,[Bladder_Steam_Pressure_AVG] "
                    + "       ,[Bladder_N2_Pressure_AVG] "
                    + "       ,[Steam_Time_SV] "
                    + "       ,[NO2_Time_SV] "
                    + "       ,[Tahliye_Time_SV] "
                    + "       ,[Cooling_Time_SV] "
                    + "       ,[Egsos_Time_SV] "
                    + "       ,[Steam_Temperature_SV]  "
                    + "       ,[NO2_Temperature_SV] "
                    + "       ,[Standby_Time] "
                    + "       ,[Operator_No] "
                    + "       ,[Bladder_Code] "
                    + "       ,[Vardiya_Kodu] "
                    + "       ,[Bladder_Counter] "
                    + "       ,[Tyre_Counter] "
                    + "       ,(Cast([Steam_Time_AV] as real) - Cast([Steam_Time_SV] as real)) as Steam_diff "
                    + "       ,(Cast([NO2_Time_AV] as real) - Cast([NO2_Time_SV] as real)) as NO2_diff  "
                    + "       ,(Cast([Tahliye_Time_AV] as real) - Cast([Tahliye_Time_SV] as real)) as Tahliye_diff "
                    + "       ,(Cast([Cooling_Time_AV] as real) - Cast([Cooling_Time_SV] as real)) as Cooling_diff "
                    + "       ,(Cast([Egsos_Time_AV] as real) - Cast([Egsos_Time_SV] as real)) as Egsos_diff "
                    + "       ,(Cast(replace([NO2_Temperature_AV],',','.') as float) - CASE WHEN charindex('E',[NO2_Temperature_SV])=0 then Cast(replace([NO2_Temperature_SV],',','.') as float) else Cast(replace([NO2_Temperature_AV],',','.') as float) end) as NO2_Temp_Diff  "
                    + "   FROM [QCDB].[dbo].[tbl_PLCMachines] "
                    + "   where CONVERT(datetime,[Time], 105)>=CONVERT(datetime,@dt, 105) "
                    + "   order by [Time] desc "
                        , cn);
                da.Fill(g, g.Tables[0].TableName);
            ReportDataSource rds = new ReportDataSource("PlcLive_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        private void chk_canliplc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_canliplc.Checked == true)
            {
                PlcVerim_date.Enabled = false;
                btn_run.Enabled = false;
                timer1.Start();
            }
            else
            {
                PlcVerim_date.Enabled = true;
                btn_run.Enabled = true;
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chk_canliplc.Checked == true)
            {
                plcverimlilik();
                plc_livedata(reportViewer9, "CANLI PRES VERİLERİ");
            }
        }
    }
}
