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
    public partial class IkinciKaliteTT : Form
    {
        public string loc;
        public IkinciKaliteTT()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }
        public void geneltablo_all(ReportViewer rp, string sub,string tb)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            geneltablo_ikinci g = new geneltablo_ikinci();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            if (tb == "dbo.fxnGenelTabloDATTALL")
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10), "
                    + "         @dt2 as char(10)  "
                    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                + " select CASE WHEN [Kullanım Alani]<>KOD and KOD='Toplam' then null else [Kullanım Alani] end as Kullanim_Alani, KOD, Ebatlar "
                + " , [BIRIM KG] as BIRIM_KG, [DA ADET] as DA_ADET, [DA KG] as DA_KG, [TT ADET] as TT_ADET, [TT KG] as TT_KG  "
                + " from " + tb + " (@dt,@dt2) order by [Kullanım Alani], dbo.fn_CreateAlphanumericSortValue(KOD) "
                        , cn);
                da.Fill(g, g.Tables[0].TableName);
            }
            else if (tb == "dbo.fxnGenelTabloDATT")
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10), "
                    + "         @dt2 as char(10)  "
                    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                + " select CASE WHEN [Kullanım Alani]<>KOD and KOD='Toplam' then null else [Kullanım Alani] end as Kullanim_Alani, KOD, Ebatlar "
                + " , [BIRIM KG] as BIRIM_KG, [DA ADET] as DA_ADET, [DA KG] as DA_KG, [TT ADET] as TT_ADET, [TT KG] as TT_KG  "
                + " from " + tb + " (@dt,@dt2,'"+loc+"') order by [Kullanım Alani], dbo.fn_CreateAlphanumericSortValue(KOD) "
                        , cn);
                da.Fill(g, g.Tables[0].TableName);
            }
            ReportDataSource rds = new ReportDataSource("geneltablo_ikinci", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        public void DA_TT(ReportViewer rp,string sub,string tb)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            ikincikalite_dataset g = new ikincikalite_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            if (tb == "dbo.fxnIkinciKalite")
            {
                SqlDataAdapter da = new SqlDataAdapter(
                        " Declare @dt as char(10), "
                        + "         @dt2 as char(10)  "
                        + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                        + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                        + " select e.AreaOfUse as LASTIK_KULLANIM,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty from dbo.fxnIkinciKalite (@dt,@dt2) tp "
                        + " LEFT JOIN st_EbatAnlam as e "
                        + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
                        + " LEFT JOIN alp_EbatKG as ebat "
                        + " on tp.Code=ebat.Code "
                        + " order by dbo.fn_CreateAlphanumericSortValue(tp.NedenCode) "
                            , cn);
                da.Fill(g, g.Tables[0].TableName);
            }
            else if(tb == "dbo.fxnTT")
            {
                SqlDataAdapter da = new SqlDataAdapter(
                        " Declare @dt as char(10), "
                        + "         @dt2 as char(10)  "
                        + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                        + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                        + " select e.AreaOfUse as LASTIK_KULLANIM,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty from dbo.fxnTT (@dt,@dt2) tp "
                        + " LEFT JOIN st_EbatAnlam as e "
                        + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
                        + " LEFT JOIN alp_EbatKG as ebat "
                        + " on tp.Code=ebat.Code "
                        + " order by dbo.fn_CreateAlphanumericSortValue(tp.NedenCode) "
                            , cn);
                da.Fill(g, g.Tables[0].TableName);
            }
            ReportDataSource rds = new ReportDataSource("ikincikalite_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            geneltablo_all(reportViewer1,"Pişmiş Dış Lastik İkinci Kalite ve TT Raporu", "dbo.fxnGenelTabloDATTALL");
            loc = "OZKA 1";
            geneltablo_all(reportViewer2, "OZKA 1 - Pişmiş Dış Lastik İkinci Kalite ve TT Raporu", "dbo.fxnGenelTabloDATT");
            loc = "OZKA 2";
            geneltablo_all(reportViewer3, "OZKA 2 - Pişmiş Dış Lastik İkinci Kalite ve TT Raporu", "dbo.fxnGenelTabloDATT");
            DA_TT(reportViewer9, "IKINCI KALITE ANALIZ RAPORU", "dbo.fxnIkinciKalite");
            DA_TT(reportViewer4, "TT ANALIZ RAPORU", "dbo.fxnTT");
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IkinciKaliteTT.ActiveForm.Close();
        }

        private void IkinciKaliteTT_Load(object sender, EventArgs e)
        {

        }
    }
}
