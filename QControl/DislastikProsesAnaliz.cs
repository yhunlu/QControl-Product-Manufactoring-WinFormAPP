using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Odbc;
using Microsoft.Reporting.WinForms;

namespace QControl
{
    public partial class DislastikProsesAnaliz : Form
    {
        public DislastikProsesAnaliz()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DislastikProsesAnaliz.ActiveForm.Close();
        }

        private void DislastikProsesAnaliz_Load(object sender, EventArgs e)
        {

        }
        public void geneltablo_all()
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", "Dış Lastik Genel Tablosu");

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { r1, r2,r3 });
            this.reportViewer1.LocalReport.Refresh();
            GenelTablo_Dataset g = new GenelTablo_Dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10), "
                    + "         @dt2 as char(10)  "
                    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                    + " select "
                    + " CASE WHEN [Kullanım Alani]<>KOD and KOD='Toplam' then null else [Kullanım Alani] end as Kullanım_Alani "
                    + " ,KOD "
                    + " ,Ebatlar "
                    + " ,[URETIM ADET] as Uretim_Adet "
                    + " ,[URETIM KG] as Uretim_Kg "
                    + " ,[URETIM TOTAL KG] as Uretim_Total_Kg "
                    + " ,[HURDA ADET] as Hurda_Adet "
                    + " ,[HURDA KG] as Hurda_Kg "
                    + " ,[HURDA KENDİ ÜRETİMİNE GÖRE %] as Hurda_Kendi_Uretimine_göre "
                    + " ,[HURDA TOTAL ÜRETİME GÖRE %] as Hurda_Total_Uretime_göre "
                    + " ,Hedef "
                    + " ,[OZUR ADET] as Ozur_Adet "
                    + " ,[OZUR KG] as Ozur_Kg "
                    + " ,[OZUR KENDİ ÜRETİMİNE GÖRE %] as Ozur_Kendi_Uretimine_göre "
                    + " ,[OZUR TOTAL ÜRETİMİNE GÖRE %] as Ozur_Total_Uretime_göre "
                    + "from dbo.fxnGenelTabloALL(@dt,@dt2) order by [Kullanım Alani],KOD "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("GenelTablo_Dataset", g.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        public void geneltablo_Ozka1()
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", "Dış Lastik Genel Tablosu OZKA 1");

            this.reportViewer2.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            this.reportViewer2.LocalReport.Refresh();
            GenelTablo_Dataset g = new GenelTablo_Dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10), "
                    + "         @dt2 as char(10)  "
                    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                    + " select "
                    + " CASE WHEN [Kullanım Alani]<>KOD and KOD='Toplam' then null else [Kullanım Alani] end as Kullanım_Alani "
                    + " ,KOD "
                    + " ,Ebatlar "
                    + " ,[URETIM ADET] as Uretim_Adet "
                    + " ,[URETIM KG] as Uretim_Kg "
                    + " ,[URETIM TOTAL KG] as Uretim_Total_Kg "
                    + " ,[HURDA ADET] as Hurda_Adet "
                    + " ,[HURDA KG] as Hurda_Kg "
                    + " ,[HURDA KENDİ ÜRETİMİNE GÖRE %] as Hurda_Kendi_Uretimine_göre "
                    + " ,[HURDA TOTAL ÜRETİME GÖRE %] as Hurda_Total_Uretime_göre "
                    + " ,Hedef "
                    + " ,[OZUR ADET] as Ozur_Adet "
                    + " ,[OZUR KG] as Ozur_Kg "
                    + " ,[OZUR KENDİ ÜRETİMİNE GÖRE %] as Ozur_Kendi_Uretimine_göre "
                    + " ,[OZUR TOTAL ÜRETİMİNE GÖRE %] as Ozur_Total_Uretime_göre "
                    + "from dbo.fxnGenelTablo(@dt,@dt2,'OZKA 1') order by [Kullanım Alani],KOD "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("GenelTablo_Dataset", g.Tables[0]);
            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.DataSources.Add(rds);
            this.reportViewer2.LocalReport.Refresh();
            this.reportViewer2.RefreshReport();
        }
        public void geneltablo_Ozka2()
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", "Dış Lastik Genel Tablosu OZKA 2");

            this.reportViewer3.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            this.reportViewer3.LocalReport.Refresh();
            GenelTablo_Dataset g = new GenelTablo_Dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10), "
                    + "         @dt2 as char(10)  "
                    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                    + " select "
                    + " CASE WHEN [Kullanım Alani]<>KOD and KOD='Toplam' then null else [Kullanım Alani] end as Kullanım_Alani "
                    + " ,KOD "
                    + " ,Ebatlar "
                    + " ,[URETIM ADET] as Uretim_Adet "
                    + " ,[URETIM KG] as Uretim_Kg "
                    + " ,[URETIM TOTAL KG] as Uretim_Total_Kg "
                    + " ,[HURDA ADET] as Hurda_Adet "
                    + " ,[HURDA KG] as Hurda_Kg "
                    + " ,[HURDA KENDİ ÜRETİMİNE GÖRE %] as Hurda_Kendi_Uretimine_göre "
                    + " ,[HURDA TOTAL ÜRETİME GÖRE %] as Hurda_Total_Uretime_göre "
                    + " ,Hedef "
                    + " ,[OZUR ADET] as Ozur_Adet "
                    + " ,[OZUR KG] as Ozur_Kg "
                    + " ,[OZUR KENDİ ÜRETİMİNE GÖRE %] as Ozur_Kendi_Uretimine_göre "
                    + " ,[OZUR TOTAL ÜRETİMİNE GÖRE %] as Ozur_Total_Uretime_göre "
                    + "from dbo.fxnGenelTablo(@dt,@dt2,'OZKA 2') order by [Kullanım Alani],KOD "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("GenelTablo_Dataset", g.Tables[0]);
            this.reportViewer3.LocalReport.DataSources.Clear();
            this.reportViewer3.LocalReport.DataSources.Add(rds);
            this.reportViewer3.LocalReport.Refresh();
            this.reportViewer3.RefreshReport();
        }
        public void Hurda()
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", "Hurda Analiz Raporu");

            this.reportViewer4.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            this.reportViewer4.LocalReport.Refresh();
            hurda_dataset g = new hurda_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                " select e.AreaOfUse as LASTIK_KULLANIM "
            + " ,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty "
            + " from dbo.fxnHurda ('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "') tp "
            + " LEFT JOIN st_EbatAnlam as e "
            + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Code)"
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("hurda_dataset", g.Tables[0]);
            this.reportViewer4.LocalReport.DataSources.Clear();
            this.reportViewer4.LocalReport.DataSources.Add(rds);
            this.reportViewer4.LocalReport.Refresh();
            this.reportViewer4.RefreshReport();
        }
        public void Hurda_ERR(string errtype,ReportViewer rp,string sub)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            hurda_dataset g = new hurda_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                " select e.AreaOfUse as LASTIK_KULLANIM "
            + " ,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty "
            + " from dbo.fxnHurdaCnd ('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "','"+ errtype + "') tp "
            + " LEFT JOIN st_EbatAnlam as e "
            + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Code)"
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("hurda_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        public void HurdaPressM_ERR(ReportViewer rp, string sub)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            hurda_dataset g = new hurda_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                " select e.AreaOfUse as LASTIK_KULLANIM "
            + " ,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty "
            + " from dbo.fxnHurdaPress ('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "') tp "
            + " LEFT JOIN st_EbatAnlam as e "
            + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Code) "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("hurda_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            // Press Hata Qty
            pressqty gp = new pressqty();
            SqlConnection cnp = new SqlConnection(cs);
            SqlDataAdapter dap= new SqlDataAdapter(
                " select tp.Code,ebat.Ebatlar,tp.Press,tp.Qty "
            + " from dbo.fxnHurdaFindPress('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "') tp "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Press) "
                        , cnp);
            dap.Fill(gp, gp.Tables[0].TableName);

            ReportDataSource rdsp = new ReportDataSource("pressqty", gp.Tables[0]);
            rp.LocalReport.DataSources.Add(rdsp);
            // Press Production
            PresProduct_dataset gp_ = new PresProduct_dataset();
            SqlConnection cnp_ = new SqlConnection(cs);
            SqlDataAdapter dap_ = new SqlDataAdapter(
                "  Declare @dt as char(10), "
                + "          @dt2 as char(10) "
                + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                + " Select m.Press as Press_p,CONVERT(Decimal(10,2),(per.Qty/m.Qty)*100,2) as Qty_p "
                + " From "
                + " ( "
                + "     select Press,SUM(cast(ProdQty as real)) as Qty from dbo.fxnPress (@dt,@dt2)  "
                + "     group by Press "
                + " ) m inner join "
                + " ( "
                + "     select tp.Press,SUM(tp.Qty) as Qty "
                + "     from dbo.fxnHurdaFindPress(@dt,@dt2) tp "
                + "     group by tp.Press "
                + " ) per "
                + " on m.Press=per.Press "
                + " order by dbo.fn_CreateAlphanumericSortValue(m.Press) "
                        , cnp_);
            dap_.Fill(gp_, gp_.Tables[0].TableName);

            ReportDataSource rdsp_ = new ReportDataSource("PresProduct_dataset", gp_.Tables[0]);
            rp.LocalReport.DataSources.Add(rdsp_);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        public void TamirPressM_ERR(ReportViewer rp, string sub)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            hurda_dataset g = new hurda_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                " select e.AreaOfUse as LASTIK_KULLANIM "
            + " ,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty "
            + " from dbo.fxnOzurPress ('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "') tp "
            + " LEFT JOIN st_EbatAnlam as e "
            + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Code) "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("hurda_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            // Press Hata Qty
            pressqty gp = new pressqty();
            SqlConnection cnp = new SqlConnection(cs);
            SqlDataAdapter dap = new SqlDataAdapter(
                " select tp.Code,ebat.Ebatlar,tp.Press,tp.Qty "
            + " from dbo.fxnOzurFindPress('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "') tp "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Press) "
                        , cnp);
            dap.Fill(gp, gp.Tables[0].TableName);

            ReportDataSource rdsp = new ReportDataSource("pressqty", gp.Tables[0]);
            rp.LocalReport.DataSources.Add(rdsp);
            // Press Production
            PresProduct_dataset gp_ = new PresProduct_dataset();
            SqlConnection cnp_ = new SqlConnection(cs);
            SqlDataAdapter dap_ = new SqlDataAdapter(
                "  Declare @dt as char(10), "
                + "          @dt2 as char(10) "
                + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                + " Select m.Press as Press_p,CONVERT(Decimal(10,2),(per.Qty/m.Qty)*100,2) as Qty_p "
                + " From "
                + " ( "
                + "     select Press,SUM(cast(ProdQty as real)) as Qty from dbo.fxnPress (@dt,@dt2)  "
                + "     group by Press "
                + " ) m inner join "
                + " ( "
                + "     select tp.Press,SUM(tp.Qty) as Qty "
                + "     from dbo.fxnOzurFindPress(@dt,@dt2) tp "
                + "     group by tp.Press "
                + " ) per "
                + " on m.Press=per.Press "
                + " order by dbo.fn_CreateAlphanumericSortValue(m.Press) "
                        , cnp_);
            dap_.Fill(gp_, gp_.Tables[0].TableName);

            ReportDataSource rdsp_ = new ReportDataSource("PresProduct_dataset", gp_.Tables[0]);
            rp.LocalReport.DataSources.Add(rdsp_);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        public void Tamir()
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", "Tamir Analiz Raporu");

            this.reportViewer10.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            this.reportViewer10.LocalReport.Refresh();
            hurda_dataset g = new hurda_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                " select e.AreaOfUse as LASTIK_KULLANIM "
            + " ,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty "
            + " from dbo.fxnTamir ('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "') tp "
            + " LEFT JOIN st_EbatAnlam as e "
            + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Code)"
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("hurda_dataset", g.Tables[0]);
            this.reportViewer10.LocalReport.DataSources.Clear();
            this.reportViewer10.LocalReport.DataSources.Add(rds);
            this.reportViewer10.LocalReport.Refresh();
            this.reportViewer10.RefreshReport();
        }
        public void Tamir_ERR(string errtype, ReportViewer rp, string sub)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            hurda_dataset g = new hurda_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                " select e.AreaOfUse as LASTIK_KULLANIM "
            + " ,ebat.Ebatlar,tp.Code,tp.Description,tp.NedenCode,tp.Qty "
            + " from dbo.fxnTamirCnd ('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "','" + errtype + "') tp "
            + " LEFT JOIN st_EbatAnlam as e "
            + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Code)"
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("hurda_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        public void Press (string sub, ReportViewer rp)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            press_dataset g = new press_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                " select e.AreaOfUse as LASTIK_KULLANIM "
            + " ,ebat.Ebatlar,tp.Code,tp.Press,tp.ProdQty "
            + " from dbo.fxnPress ('" + pickdate1.Value.ToString("dd.MM.yyyy") + "','" + pickdate2.Value.ToString("dd.MM.yyyy") + "') tp "
            + " LEFT JOIN st_EbatAnlam as e "
            + " on LEFT(RTRIM(LTRIM(tp.Code)),1)=LEFT(RTRIM(LTRIM(e.Code)),1) "
            + " LEFT JOIN alp_EbatKG as ebat "
            + " on tp.Code=ebat.Code "
            + " order by dbo.fn_CreateAlphanumericSortValue(tp.Press)"
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("press_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        public void PressAnaliz (ReportViewer rp, string sub)
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Raporisim_geneltablo", sub);

            rp.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3 });
            rp.LocalReport.Refresh();
            pressanaliz_dataset g = new pressanaliz_dataset();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
              "  Declare @dt as char(10), "
            + "          @dt2 as char(10) "
            + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
            + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
            + " Select x.Press,x.NedenCode,x.Description,SUM(x.Qty) as Qty "
            + " From "
            + " ( "
            + " Select Press,NedenCode,Description,Qty from dbo.fxnHPress (@dt,@dt2) "
            + " UNION "
            + " select  "
            + " case when LEFT(tp.Press,1)<>'R' and LEN(tp.Press)>2 then LEFT(tp.Press,1) "
            + "         when LEFT(tp.Press,1)='R' and LEN(tp.Press)>2 then LEFT(tp.Press,2) "
            + "     else LEFT(tp.Press,1) "
            + " end + ' TOPLAM' as Press "
            + "     ,NedenCode,Description, tp.Qty "
            + "     from dbo.fxnHPress (@dt,@dt2) as tp "
            + " ) x "
            + " group by x.Press,x.NedenCode,x.Description "
            + " order by dbo.fn_CreateAlphanumericSortValue(x.Press) "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("pressanaliz_dataset", g.Tables[0]);
            rp.LocalReport.DataSources.Clear();
            rp.LocalReport.DataSources.Add(rds);
            // Press Hata Qty
            pressanaliz_dataset gp = new pressanaliz_dataset();
            SqlConnection cnp = new SqlConnection(cs);
            SqlDataAdapter dap = new SqlDataAdapter(
              "  Declare @dt as char(10), "
            + "          @dt2 as char(10) "
            + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
            + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
            + " Select i.Press,CONVERT(Decimal(10,2),((i.Qty/j.TotalProd)*100),2) as TotalProd "
            + " From "
            + " ( "
            + "     Select x.Press,SUM(x.Qty) as Qty "
            + "     From "
            + "     ( "
            + "     Select Press,NedenCode,Description,Qty from dbo.fxnHPress (@dt,@dt2) "
            + "     UNION "
            + "     select  "
            + "     case when LEFT(tp.Press,1)<>'R' and LEN(tp.Press)>2 then LEFT(tp.Press,1) "
            + "             when LEFT(tp.Press,1)='R' and LEN(tp.Press)>2 then LEFT(tp.Press,2) "
            + "         else LEFT(tp.Press,1) "
            + "     end + ' TOPLAM' as Press "
            + "         ,NedenCode,Description, tp.Qty "
            + "         from dbo.fxnHPress (@dt,@dt2) as tp "
            + "     ) x "
            + "     group by x.Press "
            + " ) i "
            + " LEFT JOIN "
            + " ( "
            + "                 select "
            + "                     case when LEFT(Press,1)<>'R' and LEN(Press)>2 then LEFT(Press,1) "
            + "                          when LEFT(Press,1)='R' and LEN(Press)>2 then LEFT(Press,2) "
            + "                         else LEFT(Press,1) "
            + "                     end + ' TOPLAM' as Press "
            + "                     ,SUM(cast(ProdQty as real)) as TotalProd from dbo.fxnPress (@dt,@dt2)  "
            + "                 Group By "
            + "                     case when LEFT(Press,1)<>'R' and LEN(Press)>2 then LEFT(Press,1) "
            + "                          when LEFT(Press,1)='R' and LEN(Press)>2 then LEFT(Press,2) "
            + "                         else LEFT(Press,1) "
            + "                     end + ' TOPLAM' "
            + "                 UNION "
            + "                 select Press,SUM(cast(ProdQty as real)) as TotalProd from dbo.fxnPress (@dt,@dt2) Group By Press "
            + " ) j "
            + " ON i.Press=j.Press "
            + " order by dbo.fn_CreateAlphanumericSortValue(i.Press) "
                        , cnp);
            dap.Fill(gp, gp.Tables[0].TableName);

            ReportDataSource rdsp = new ReportDataSource("pressanalizTotalProd_dataset", gp.Tables[0]);
            rp.LocalReport.DataSources.Add(rdsp);
            //// Press Production
            //PresProduct_dataset gp_ = new PresProduct_dataset();
            //SqlConnection cnp_ = new SqlConnection(cs);
            //SqlDataAdapter dap_ = new SqlDataAdapter(
            //    "  Declare @dt as char(10), "
            //    + "          @dt2 as char(10) "
            //    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
            //    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
            //    + " Select m.Press as Press_p,CONVERT(Decimal(10,2),(per.Qty/m.Qty)*100,2) as Qty_p "
            //    + " From "
            //    + " ( "
            //    + "     select Press,SUM(cast(ProdQty as real)) as Qty from dbo.fxnPress (@dt,@dt2)  "
            //    + "     group by Press "
            //    + " ) m inner join "
            //    + " ( "
            //    + "     select tp.Press,SUM(tp.Qty) as Qty "
            //    + "     from dbo.fxnOzurFindPress(@dt,@dt2) tp "
            //    + "     group by tp.Press "
            //    + " ) per "
            //    + " on m.Press=per.Press "
            //    + " order by dbo.fn_CreateAlphanumericSortValue(m.Press) "
            //            , cnp_);
            //dap_.Fill(gp_, gp_.Tables[0].TableName);

            //ReportDataSource rdsp_ = new ReportDataSource("PresProduct_dataset", gp_.Tables[0]);
            //rp.LocalReport.DataSources.Add(rdsp_);

            rp.LocalReport.Refresh();
            rp.RefreshReport();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            geneltablo_all();
            geneltablo_Ozka1();
            geneltablo_Ozka2();

            Hurda();
            Hurda_ERR("Other",reportViewer5, "Hurda-Diger Hata Analiz Raporu");
            Hurda_ERR("Personal", reportViewer6, "Hurda-Bireysel Hata Analiz Raporu");
            Hurda_ERR("Process", reportViewer8, "Hurda-Proses Hata Analiz Raporu");
            HurdaPressM_ERR(reportViewer7, "Hurda-(Pres-Mekanik) Hata Analiz Raporu");

            Press("Press - Uretim Analiz Raporu", reportViewer9);

            Tamir();
            Tamir_ERR("Other", reportViewer11, "Tamir-Diger Hata Analiz Raporu");
            Tamir_ERR("Personal", reportViewer13, "Tamir-Bireysel Hata Analiz Raporu");
            Tamir_ERR("Process", reportViewer12, "Tamir-Proses Hata Analiz Raporu");
            TamirPressM_ERR(reportViewer14, "Tamir-(Pres-Mekanik) Hata Analiz Raporu");

            PressAnaliz(reportViewer15, "MEKANİKSEL - ELEKTİRİKSEL DIŞ LASTİK HURDA NEDENLERİNİN PRESLERE GÖRE SINIFLANDIRMASI GELİŞ ADETLERİ VE ORANLARI");
        }
    }
}
