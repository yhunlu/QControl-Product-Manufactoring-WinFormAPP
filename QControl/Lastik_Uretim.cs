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
    public partial class Lastik_Uretim : Form
    {
        FromDLL dll = new FromDLL();
        public Lastik_Uretim()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private void Lastik_Uretim_Load(object sender, EventArgs e)
        {

        }
        public void Lastik_UretimQuery()
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Lastik_Code", cmb_ebat.Text);
            ReportParameter r4 = new ReportParameter("Report_name", "Ebatlara Göre Lastik Üretim");
            ReportParameter r5 = new ReportParameter("Fab", cmb_fab.Text);

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3, r4, r5 });
            this.reportViewer1.LocalReport.Refresh();

            TyreProduct g = new TyreProduct();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10), "
                    + "         @dt2 as char(10),  "
                    + "         @tyre as char(50)   "
                    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @tyre= '" + cmb_ebat.Text + "' "
                    + " select * from dbo.fxnLastikUretim(@dt,@dt2,@tyre,'" + cmb_fab.Text + "') order by case when ISDATE(Tarih)=1 then CONVERT(datetime,Tarih, 105) end desc "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("TyreProduct", g.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        public void Lastik_UretimQueryByPress()
        {
            ReportParameter r1 = new ReportParameter("StartDate", pickdate1.Value.ToString("dd.MM.yyyy"));
            ReportParameter r2 = new ReportParameter("FinishDate", pickdate2.Value.ToString("dd.MM.yyyy"));
            ReportParameter r3 = new ReportParameter("Lastik_Code", cmb_press.Text);
            ReportParameter r4 = new ReportParameter("Report_name", "Preslere Göre Lastik Üretim");
            ReportParameter r5 = new ReportParameter("Fab", cmb_fab.Text);

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { r1, r2, r3, r4, r5 });
            this.reportViewer1.LocalReport.Refresh();

            TyreProduct g = new TyreProduct();
            string cs = Properties.Settings.Default.cnnSQL;
            SqlConnection cn = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter(
                    " Declare @dt as char(10), "
                    + "         @dt2 as char(10),  "
                    + "         @tyre as char(50)   "
                    + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
                    + " set @tyre= '" + cmb_press.Text + "' "
                    + " select * from dbo.fxnLastikUretimByPress (@dt,@dt2,@tyre,'" + cmb_fab.Text + "') order by case when ISDATE(Tarih)=1 then CONVERT(datetime,Tarih, 105) end desc "
                        , cn);
            da.Fill(g, g.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("TyreProduct", g.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        public void actionafterdate()
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnnSQL,
                    " Declare @dt as char(10), "
            + "     @dt2 as char(10) "
            + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
            + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
            + " Select q.Code "
            + " From "
            + " ( "
            + "     select Ltrim(Rtrim(t.Code)) as Code "
            + "     from tbl_DislastikProduction t "
            + "     inner join cn_PressIDs p on t.Press=p.Press "
            + "     where p.Location='" + cmb_fab.Text + "' and cast(t.ProdQty as real)>0 and CONVERT(datetime,t.Date,104)>=CONVERT(datetime,@dt, 104) and CONVERT(datetime,t.Date,104)<=CONVERT(datetime,@dt2, 104) "
            + "     group by Ltrim(Rtrim(t.Code))"
            + " ) q "
            + " order by dbo.fn_CreateAlphanumericSortValue(q.Code) "
                , cmb_ebat, "Code");
        }
        public void actionForPressafterdate()
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnnSQL,
                    " Declare @dt as char(10), "
            + "     @dt2 as char(10) "
            + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
            + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
            + " Select q.Press "
            + " From "
            + " ( "
            + "     select Ltrim(Rtrim(t.Press)) as Press "
            + "     from tbl_DislastikProduction t "
            + "     inner join cn_PressIDs p on t.Press=p.Press "
            + "     where p.Location='" + cmb_fab.Text + "' and cast(t.ProdQty as real)>0 and CONVERT(datetime,t.Date,104)>=CONVERT(datetime,@dt, 104) and CONVERT(datetime,t.Date,104)<=CONVERT(datetime,@dt2, 104) "
            + "     group by Ltrim(Rtrim(t.Press))"
            + " ) q "
            + " order by dbo.fn_CreateAlphanumericSortValue(q.Press) "
                , cmb_press, "Press");
        }
        public void actionLocationafterdate()
        {
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnnSQL,
                    " Declare @dt as char(10), "
            + "     @dt2 as char(10) "
            + " set @dt= '" + pickdate1.Value.ToString("dd.MM.yyyy") + "' "
            + " set @dt2= '" + pickdate2.Value.ToString("dd.MM.yyyy") + "' "
            + " Select q.Fab "
            + " From  "
            + " (  "
            + "     select Ltrim(Rtrim(p.Location)) as Fab  "
            + "     from tbl_DislastikProduction t inner join cn_PressIDs p "
            + "     on t.Press=p.Press "
            + "     where cast(t.ProdQty as real)>0  "
            + "     and CONVERT(datetime,t.Date,104)>=CONVERT(datetime,@dt, 104) and CONVERT(datetime,t.Date,104)<=CONVERT(datetime,@dt2, 104)  "
            + "     group by Ltrim(Rtrim(p.Location)) "
            + " ) q  "
            + " order by dbo.fn_CreateAlphanumericSortValue(q.Fab)  "
                , cmb_fab, "Fab");
        }
        private void pickdate2_ValueChanged(object sender, EventArgs e)
        {
            actionLocationafterdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lastik_UretimQuery();
        }

        private void pickdate1_ValueChanged(object sender, EventArgs e)
        {
            actionLocationafterdate();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lastik_Uretim.ActiveForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lastik_UretimQueryByPress();
        }

        private void cmb_fab_SelectedIndexChanged(object sender, EventArgs e)
        {
            actionafterdate();
            actionForPressafterdate();
        }
    }
}
