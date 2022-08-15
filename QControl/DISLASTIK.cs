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
//using Microsoft.Office.Core;
using System.IO;

namespace QControl
{
    public partial class DISLASTIK : Form
    {
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private string sql = null;
        public string filepath;
        public string nodeselected;
        public string parent_nodeselected;
        public string prod_table = "tbl_DislastikProduction";
        public string neden_table = "cn_HurdaOzurCode";
        public int ykid=0;
        public string cmbDate;
        public string cmbCode;
        public string cmbPress;
        public string cmbNedenCode;
        public Int32 cmbOzurQty;
        public Int32 cmbHurdaQty;
        public Int32 cmbId;
        public int selectedrowindex;
        RaporEngel re = null;
        TyreControl tc = null;
        ToKnowFormat fr = null;
        EbatKg ebat = null;
        Press pres = null;
        FromDLL dll = new FromDLL();
        public DISLASTIK()
        {
            InitializeComponent();
        }
        public void UserDefineTempTb()
        {
            dll.UserDefineToTempTable(Properties.Settings.Default.cnn, label2);
        }
        public void VerifyTemp()
        {
            if (label2.Text != "")
            {
                Image resim = Properties.Resources.success;
                pictureBox1.BackgroundImage = resim;
                button1.ForeColor = Color.FromArgb(192,255,192);
            }
            else
            {
                Image resim = Properties.Resources.delete;
                pictureBox1.BackgroundImage = resim;
                button1.ForeColor = Color.DarkRed;
            }
            
        }
        public void Insert_temp_Dislastik_BiasPisim()
        {
            dll.UpdateDeleteInsertFunctions("Delete t from temp_DislastikBiasPisim t", Properties.Settings.Default.cnn);
            dll.UpdateDeleteInsertFunctions(
                " Insert into temp_DislastikBiasPisim( "
                + " Date,Press,Size,Code,Ebatlar,Birim_KG,STD_Adet,STD_KG,Fiili_0816,Fiili_1624,Fiili_2408,Fiili_GenelTP "
                + " ) "
                + " select  "
                + " CONVERT(varchar(10), CONVERT(datetime,LTRIM(RTRIM(Date)), 105), 104),LTRIM(RTRIM(Press)),LTRIM(RTRIM(Size)),LTRIM(RTRIM(Code)),LTRIM(RTRIM(Ebatlar)),LTRIM(RTRIM(Birim_KG)),LTRIM(RTRIM(STD_Adet)),LTRIM(RTRIM(STD_KG)),LTRIM(RTRIM(Fiili_0816)),LTRIM(RTRIM(Fiili_1624)),LTRIM(RTRIM(Fiili_2408)),LTRIM(RTRIM(Fiili_GenelTP)) "
                + " from  " + label2.Text + " t "
                , Properties.Settings.Default.cnn
            );
        }
        public void Insert_temp_Dislastik_RadialPisim()
        {
            dll.UpdateDeleteInsertFunctions("Delete t from temp_DislastikRadialPisim t", Properties.Settings.Default.cnn);
            dll.UpdateDeleteInsertFunctions(
                " Insert into temp_DislastikRadialPisim( "
                + " Date,Press,Size,Code,Ebatlar,Birim_KG,STD_Adet,STD_KG,Fiili_0816,Fiili_1624,Fiili_2408,Fiili_GenelTP "
                + " ) "
                + " select  "
                + " CONVERT(varchar(10), CONVERT(datetime,LTRIM(RTRIM(Date)), 105), 104),LTRIM(RTRIM(Press)),LTRIM(RTRIM(Size)),LTRIM(RTRIM(Code)),LTRIM(RTRIM(Ebatlar)),LTRIM(RTRIM(Birim_KG)),LTRIM(RTRIM(STD_Adet)),LTRIM(RTRIM(STD_KG)),LTRIM(RTRIM(Fiili_0816)),LTRIM(RTRIM(Fiili_1624)),LTRIM(RTRIM(Fiili_2408)),LTRIM(RTRIM(Fiili_GenelTP)) "
                + " from  " + label2.Text + " t "
                , Properties.Settings.Default.cnn
            );
        }
        public void Insert_temp_Dislastik_Pisim_OneByOne(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " Insert into " + tb + "( "
                + " Date,Press,Code,Fiili_0816,Fiili_1624,Fiili_2408 "
                + " ) "
                + " select  "
                + " LTRIM(RTRIM('" + cmb_date.Text + "')),LTRIM(RTRIM('" + cmb_press.Text + "')),LTRIM(RTRIM('" + cmb_code.Text + "')),LTRIM(RTRIM('" + nud_Prod0816.Text + "')),LTRIM(RTRIM('" + nud_Prod1624.Text + "')),LTRIM(RTRIM('" + nud_Prod2408.Text + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Insert_temp_Dislastik_Bitirme_OneByOne(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " Insert into " + tb + "( "
                + " Date,Time,Press,Code,NedenCode,OzurQty,HurdaQty,DA,TT "
                + " ) "
                + " select  "
                + " LTRIM(RTRIM('" + cmb_date.Text + "')),'" + System.DateTime.Now.ToString("HH:mm") + "',LTRIM(RTRIM('" + cmb_press.Text + "')),LTRIM(RTRIM('" + cmb_code.Text + "')),LTRIM(RTRIM('" + cmb_nedencode.Text + "')),LTRIM(RTRIM('" + nud_OzurQty.Text + "')),LTRIM(RTRIM('" + nud_HurdaQty.Text + "')),LTRIM(RTRIM('" + nud_DAQty.Text + "')),LTRIM(RTRIM('" + nud_TTQty.Text + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Update_temp_Dislastik_Bitirme_OneByOne(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " update " + tb + " set "
                + " OzurQty='" + nud_OzurQty.Text + "', "
                + " HurdaQty='" + nud_HurdaQty.Text + "', "
                + " DA='" + nud_DAQty.Text + "', "
                + " TT='" + nud_TTQty.Text + "' "
                + " from " + tb + " t where t.Date=LTRIM(RTRIM('" + cmb_date.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_code.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_press.Text + "')) and t.NedenCode=LTRIM(RTRIM('" + cmb_nedencode.Text + "')) and t.ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Update_temp_Dislastik_Pisim_OneByOne(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " update " + tb + " set "
                + " Fiili_0816='" + nud_Prod0816.Text + "', "
                + " Fiili_1624='" + nud_Prod1624.Text + "', "
                + " Fiili_2408='" + nud_Prod2408.Text + "' "
                + " from " + tb + " t where t.Date=LTRIM(RTRIM('" + cmb_date.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_code.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_press.Text + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Delete_temp_Dislastik_Pisim_OneByOne(string tb)
        {
            dll.UpdateDeleteInsertFunctions("delete t "
                + " from " + tb + " t where t.Date=LTRIM(RTRIM('" + cmb_date.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_code.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_press.Text + "')) "
                , Properties.Settings.Default.cnn);
        }
        public void Delete_temp_Dislastik_Bitirme_OneByOne(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " delete t "
                + " from " + tb + " t where t.Date=LTRIM(RTRIM('" + cmb_date.Text + "')) and t.Code=LTRIM(RTRIM('" + cmb_code.Text + "')) and t.Press=LTRIM(RTRIM('" + cmb_press.Text + "')) and t.NedenCode=LTRIM(RTRIM('" + cmb_nedencode.Text + "')) and t.ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn
            );
        }
        public void Delete_temp_Dislastik_Pisim_OneByOneCHOOSENDATAGRID(string tb)
        {
            dll.UpdateDeleteInsertFunctions("delete t "
                + " from " + tb + " t where t.Date=LTRIM(RTRIM('" + cmbDate + "')) and t.Code=LTRIM(RTRIM('" + cmbCode + "')) and t.Press=LTRIM(RTRIM('" + cmbPress + "')) "
                , Properties.Settings.Default.cnn);
        }
        public void Delete_temp_Dislastik_Bitirme_OneByOneCHOOSENDATAGRID(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " delete t "
                + " from " + tb + " t where t.Date=LTRIM(RTRIM('" + cmbDate + "')) and t.Code=LTRIM(RTRIM('" + cmbCode + "')) and t.Press=LTRIM(RTRIM('" + cmbPress + "')) and t.NedenCode=LTRIM(RTRIM('" + cmbNedenCode + "')) and t.ID=LTRIM(RTRIM('" + cmbId + "'))"
                , Properties.Settings.Default.cnn
            );
        }
        public void Insert_tbl_DislastikProduction_Pisim(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " insert " + prod_table + " "
                + " Select  "
                + " p.Date,p.Code,p.Press,p.Prod_Qty,p.CiftKalip "
                + " From "
                + " ( "
                + "     SELECT Date,Code "
                + "     ,sum(cast(isnull(Fiili_0816,0) as real)+cast(isnull(Fiili_1624,0) as real)+cast(isnull(Fiili_2408,0) as real)) as Prod_Qty "
                + "     ,Press as Press "
                + "     ,case when sum(1)>1 then 1 else 0 end as CiftKalip "
                + "       FROM " + tb + " "
                + "     WHERE (HATA='' oR len(HATA)=0 oR isnull(HATA,'')='') "
                + "     Group by Date,Code,Press "
                + " ) p "
                , Properties.Settings.Default.cnn
            );
            dll.UpdateDeleteInsertFunctions("Delete t from " + tb + " t where (t.HATA='' oR len(t.HATA)=0 oR isnull(t.HATA,'')='') ", Properties.Settings.Default.cnn);
        }
        public void Insert_tbl_DislastikOzur(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " insert tbl_DislastikOzur "
                + " select Date,Code,Press,NedenCode,OzurQty,DA,TT "
                + " From "
                + " ( "
                + " SELECT Date,Code,Press,NedenCode "
                + " ,sum(cast(isnull(OzurQty,0) as real)) as OzurQty "
                + " ,sum(cast(isnull(DA,0) as real)) as DA "
                + " ,sum(cast(isnull(TT,0) as real)) as TT "
                + " FROM " + tb + " "
                + " WHERE (HATA='' oR len(HATA)=0 oR isnull(HATA,'')='') "
                + " Group by Date,Code,NedenCode,Press "
                + " ) p "
                + " where p.OzurQty>0 "
                , Properties.Settings.Default.cnn
            );
        }
        public void Insert_tbl_DislastikHurda(string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " insert tbl_DislastikHurda "
                + " select Date,Code,Press,NedenCode,HurdaQty "
                + " From "
                + " ( "
                + " SELECT Date,Code,Press,NedenCode "
                + " ,sum(cast(isnull(HurdaQty,0) as real)) as HurdaQty "
                + " FROM " + tb + " "
                + " WHERE (HATA='' oR len(HATA)=0 oR isnull(HATA,'')='') "
                + " Group by Date,Code,NedenCode,Press "
                + " ) p "
                + " where p.HurdaQty>0 "
                , Properties.Settings.Default.cnn
            );
            dll.UpdateDeleteInsertFunctions("Delete t from " + tb + " t where (t.HATA='' oR len(t.HATA)=0 oR isnull(t.HATA,'')='') ", Properties.Settings.Default.cnn);
        }
        public void Insert_temp_Dislastik_BitirmeOzka1()
        {
            dll.UpdateDeleteInsertFunctions("Delete t from temp_DislastikBitirmeOZKA1 t", Properties.Settings.Default.cnn);
            dll.UpdateDeleteInsertFunctions(
                " Insert into temp_DislastikBitirmeOZKA1( "
                + " Date,Time,Code,Press,NedenCode,OzurQty,HurdaQty "
                + " ) "
                + " select  "
                + " CONVERT(varchar(10), CONVERT(datetime,LTRIM(RTRIM(Date)), 105), 104),LTRIM(RTRIM(Time)),LTRIM(RTRIM(Code)),LTRIM(RTRIM(Press)),LTRIM(RTRIM(NedenCode)),LTRIM(RTRIM(OzurQty)),LTRIM(RTRIM(HurdaQty)) "
                + " from  " + label2.Text + " t "
                , Properties.Settings.Default.cnn
            );
        }
        public void Insert_temp_Dislastik_BitirmeOzka2()
        {
            dll.UpdateDeleteInsertFunctions("Delete t from temp_DislastikBitirmeOZKA2 t", Properties.Settings.Default.cnn);
            dll.UpdateDeleteInsertFunctions(
                " Insert into temp_DislastikBitirmeOZKA2( "
                + " Date,Time,Code,Press,NedenCode,OzurQty,HurdaQty "
                + " ) "
                + " select  "
                + " CONVERT(varchar(10), CONVERT(datetime,LTRIM(RTRIM(Date)), 105), 104),LTRIM(RTRIM(Time)),LTRIM(RTRIM(Code)),LTRIM(RTRIM(Press)),LTRIM(RTRIM(NedenCode)),LTRIM(RTRIM(OzurQty)),LTRIM(RTRIM(HurdaQty)) "
                + " from  " + label2.Text + " t "
                , Properties.Settings.Default.cnn
            );
        }
        public void ActivateComponents_Delete()
        {
            foreach (Control x in this.groupBox6.Controls)
            {
                if (x is Label)
                {
                    ((Label)x).Enabled = true;
                }
                if (x is Button)
                {
                    ((Button)x).Enabled = true;
                }
            }
            treeView1.ExpandAll();
            treeView1.Enabled = true;
            dll.TREEVIEW_UnSelectedNodes(treeView1);
            button5.ForeColor = Color.FromArgb(192, 255, 192);
        }
        public void ActivateComponents()
        {
            foreach (Control x in this.groupBox6.Controls)
            {
                if (x is ComboBox)
                {
                    ((ComboBox)x).Enabled = true;
                    ((ComboBox)x).DataSource = null;
                    ((ComboBox)x).Items.Clear();
                }
                if (x is Label)
                {
                    ((Label)x).Enabled = true;
                }
                if (x is Button)
                {
                    ((Button)x).Enabled = true;
                }
            }
            treeView1.ExpandAll();
            treeView1.Enabled = true;
            dll.TREEVIEW_UnSelectedNodes(treeView1);
            button5.ForeColor = Color.FromArgb(192, 255, 192);
        }
        public void Deactivate_IslemCinsiComponents()
        {
            if (kAYITISILToolStripMenuItem.ForeColor != System.Drawing.Color.FromArgb(254, 214, 2))
            {
                foreach (Control x in this.groupBox6.Controls)
                {
                    if (x is ComboBox)
                    {
                        ((ComboBox)x).Enabled = false;
                    }
                    if (x is Label)
                    {
                        ((Label)x).Enabled = false;
                    }
                    if (x is Button)
                    {
                        ((Button)x).Enabled = false;
                    }
                }
                foreach (Control x in this.groupBox7.Controls)
                {
                    if (x is RadioButton)
                    {
                        ((RadioButton)x).Checked = false;
                    }
                }

                rb_ekle.Enabled = false;
                rb_guncelle.Enabled = false;
                gb_Hurda.Enabled = false;
                gb_Ozur.Enabled = false;
                gb_Uretim.Enabled = false;
                treeView1.CollapseAll();
                treeView1.Enabled = false;
                button3.ForeColor = Color.FromArgb(192, 255, 192);
            }
        }
        public void Activate_IslemCinsiComponents()
        {
            if (kAYITISILToolStripMenuItem.ForeColor != System.Drawing.Color.FromArgb(254, 214, 2))
            {
                rb_ekle.Enabled = true;
                rb_guncelle.Enabled = true;
                button3.ForeColor = Color.FromArgb(197, 197, 199);
            }
        }
        public void DeactivateComponents()
        {
            foreach (Control x in this.groupBox6.Controls)
            {
                if (x is ComboBox)
                {
                    ((ComboBox)x).Enabled = false;
                }
                if (x is Label)
                {
                    ((Label)x).Enabled = false;
                }
                if (x is Button)
                {
                    ((Button)x).Enabled = false;
                }
            }
            treeView1.ExpandAll();
            treeView1.Enabled = false;
        }
        private void DISLASTIK_Load(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            UserDefineTempTb();
            VerifyTemp();
            tabPage1.Text = @"temp_DislastikBiasPisim";
            tabPage2.Text = @"temp_DislastikRadialPisim";
            tabPage5.Text = @"temp_DislastikBitirmeOZKA1";
            tabPage6.Text = @"temp_DislastikBitirmeOZKA2";
            radioButton1.Checked = true;
            DeactivateComponents();
            if (kAYITISILToolStripMenuItem.ForeColor != System.Drawing.Color.FromArgb(254, 214, 2))
            {
                rb_sil.Enabled = false;
                dll.LoadXmlFileToTreeview(this.treeView1, Application.StartupPath + "\\NonAdmin_Nodes.xml");
            }
            else
            {
                button3.ForeColor = Color.FromArgb(192, 255, 192);
                dll.LoadXmlFileToTreeview(this.treeView1, Application.StartupPath + "\\Admin_Nodes.xml");
            }
            rightclick();
        }
        public void SelectQueryToDataGridView(string tb, DataGridView dg)
        {
            dll.QueryToDataGrid(dg, Properties.Settings.Default.cnn,
                "Select * from " + tb + " order by HATA desc"
                );
            Application.EnableVisualStyles();
            dll.ReDesignDataGridViewCELLFormat(dg, Color.LightSteelBlue, Color.White, Color.DarkBlue, Color.LightBlue);
            //design options
            dg.ForeColor = Color.Black;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.ForeColor == Color.FromArgb(192, 255, 192))
            {
                if (radioButton1.Checked == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Gecici Veri Alanı temizlenicek !", "BIAS Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OpenFileDialog fdlg = new OpenFileDialog();
                        fdlg.Title = "Select file";
                        fdlg.InitialDirectory = @"";
                        fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                        fdlg.FilterIndex = 1;
                        fdlg.RestoreDirectory = true;
                        if (fdlg.ShowDialog() == DialogResult.OK)
                        {

                            dll.ImportTempTable(fdlg.FileName, label2, Properties.Settings.Default.cnn);
                            Insert_temp_Dislastik_BiasPisim();

                            dll.UpdateDeleteInsertFunctions(
                                " EXEC CHECKtemp_DislastikPisim @PartOfTable='temp_DislastikBiasPisim' "
                                , Properties.Settings.Default.cnn
                            );
                            SelectQueryToDataGridView("temp_DislastikBiasPisim",dg_biaspisim);
                            tc_production.SelectedTab = tabPage1;
                            MessageBox.Show("Dosya yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                            ykid = 1;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ykid = 0;
                    }
                }
                if (radioButton2.Checked == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Gecici Veri Alanı temizlenicek !", "RADIAL Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OpenFileDialog fdlg = new OpenFileDialog();
                        fdlg.Title = "Select file";
                        fdlg.InitialDirectory = @"";
                        fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                        fdlg.FilterIndex = 1;
                        fdlg.RestoreDirectory = true;
                        if (fdlg.ShowDialog() == DialogResult.OK)
                        {
                            dll.ImportTempTable(fdlg.FileName, label2, Properties.Settings.Default.cnn);
                            Insert_temp_Dislastik_RadialPisim();

                            dll.UpdateDeleteInsertFunctions(
                                " EXEC CHECKtemp_DislastikPisim @PartOfTable='temp_DislastikRadialPisim' "
                                , Properties.Settings.Default.cnn
                            );
                            SelectQueryToDataGridView("temp_DislastikRadialPisim", dg_radialpisim);
                            tc_production.SelectedTab = tabPage2;
                            MessageBox.Show("Dosya yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                            ykid = 1;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ykid = 0;
                    }
                }
                if (radioButton3.Checked == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Gecici Veri Alanı temizlenicek !", "BITIRME OZKA1 Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OpenFileDialog fdlg = new OpenFileDialog();
                        fdlg.Title = "Select file";
                        fdlg.InitialDirectory = @"";
                        fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                        fdlg.FilterIndex = 1;
                        fdlg.RestoreDirectory = true;
                        if (fdlg.ShowDialog() == DialogResult.OK)
                        {
                            dll.ImportTempTable(fdlg.FileName, label2, Properties.Settings.Default.cnn);
                            Insert_temp_Dislastik_BitirmeOzka1();

                            dll.UpdateDeleteInsertFunctions(
                                " EXEC CHECKtemp_DislastikBitirmeOzka @PartOfTable='temp_DislastikBitirmeOZKA1' "
                                , Properties.Settings.Default.cnn
                            );
                            dll.UpdateDeleteInsertFunctions(
                                " EXEC [dbo].[CHECKtemp_DislastikOnlyHurda] @PartOfTable='temp_DislastikBitirmeOZKA1' "
                                , Properties.Settings.Default.cnn
                            );
                            dll.UpdateDeleteInsertFunctions(
                                " EXEC [dbo].[CHECKtemp_DislastikOnlyOzur] @PartOfTable='temp_DislastikBitirmeOZKA1' "
                                , Properties.Settings.Default.cnn
                            );
                            SelectQueryToDataGridView("temp_DislastikBitirmeOZKA1", dg_bitirmeO1);
                            tc_production.SelectedTab = tabPage5;
                            MessageBox.Show("Dosya yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                            ykid = 1;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ykid = 0;
                    }
                }
                if (radioButton4.Checked == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Gecici Veri Alanı temizlenicek !", "BITIRME OZKA2 Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OpenFileDialog fdlg = new OpenFileDialog();
                        fdlg.Title = "Select file";
                        fdlg.InitialDirectory = @"";
                        fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                        fdlg.FilterIndex = 1;
                        fdlg.RestoreDirectory = true;
                        if (fdlg.ShowDialog() == DialogResult.OK)
                        {
                            dll.ImportTempTable(fdlg.FileName, label2, Properties.Settings.Default.cnn);
                            Insert_temp_Dislastik_BitirmeOzka2();

                            dll.UpdateDeleteInsertFunctions(
                                " EXEC CHECKtemp_DislastikBitirmeOzka @PartOfTable='temp_DislastikBitirmeOZKA2' "
                                , Properties.Settings.Default.cnn
                            );
                            dll.UpdateDeleteInsertFunctions(
                                " EXEC [dbo].[CHECKtemp_DislastikOnlyHurda] @PartOfTable='temp_DislastikBitirmeOZKA2' "
                                , Properties.Settings.Default.cnn
                            );
                            dll.UpdateDeleteInsertFunctions(
                                " EXEC [dbo].[CHECKtemp_DislastikOnlyOzur] @PartOfTable='temp_DislastikBitirmeOZKA2' "
                                , Properties.Settings.Default.cnn
                            );
                            SelectQueryToDataGridView("temp_DislastikBitirmeOZKA2", dg_bitirmeO2);
                            tc_production.SelectedTab = tabPage6;
                            MessageBox.Show("Dosya yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                            ykid = 1;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ykid = 0;
                    }
                }
            }
            else if (button1.ForeColor == System.Drawing.Color.DarkRed)
            {
                MessageBox.Show("you dont have access !", "ACCESS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ykid == 1)
            {
                button3.ForeColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                button3.ForeColor = System.Drawing.Color.DarkRed;
            }
        }
        public void rightclick()
        {
            if (tc_production.SelectedTab == tabPage1)
            {
                lb_prodqty.Text = "0";
                SelectQueryToDataGridView("temp_DislastikBiasPisim", dg_biaspisim);
                calculate_Ozka1prodQty();
            }
            else if (tc_production.SelectedTab == tabPage2)
            {
                lb_prodqty.Text = "0";
                SelectQueryToDataGridView("temp_DislastikRadialPisim", dg_radialpisim);
                calculate_Ozka2prodQty();
            }
            else if (tc_production.SelectedTab == tabPage5)
            {
                lb_prodqty.Text = "0";
                SelectQueryToDataGridView("temp_DislastikBitirmeOZKA1", dg_bitirmeO1);
            }
            else if (tc_production.SelectedTab == tabPage6)
            {
                lb_prodqty.Text = "0";
                SelectQueryToDataGridView("temp_DislastikBitirmeOZKA2", dg_bitirmeO2);
            }
            calculate_blockedreports();
        }
        public void rightclick_cleantable()
        {
            if (tc_production.SelectedTab == tabPage1)
            {
                dll.UpdateDeleteInsertFunctions("delete from temp_DislastikBiasPisim", Properties.Settings.Default.cnn);
                lb_prodqty.Text = "0";
            }
            else if (tc_production.SelectedTab == tabPage2)
            {
                dll.UpdateDeleteInsertFunctions("delete from temp_DislastikRadialPisim", Properties.Settings.Default.cnn);
                lb_prodqty.Text = "0";
            }
            else if (tc_production.SelectedTab == tabPage5)
            {
                dll.UpdateDeleteInsertFunctions("delete from temp_DislastikBitirmeOZKA1", Properties.Settings.Default.cnn);
                lb_prodqty.Text = "0";
            }
            else if (tc_production.SelectedTab == tabPage6)
            {
                dll.UpdateDeleteInsertFunctions("delete from temp_DislastikBitirmeOZKA2", Properties.Settings.Default.cnn);
                lb_prodqty.Text = "0";
            }
        }
        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightclick();
        }
        public void ExecuteSPUpToRadioButton()
        {
            if (radioButton1.Checked == true)
            {
                dll.UpdateDeleteInsertFunctions(
                    " EXEC CHECKtemp_DislastikPisim @PartOfTable='temp_DislastikBiasPisim' "
                    , Properties.Settings.Default.cnn
                );
                SelectQueryToDataGridView("temp_DislastikBiasPisim", dg_biaspisim);
                tc_production.SelectedTab = tabPage1;
                MessageBox.Show("Kontrol Edildi..", tabPage1.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (radioButton2.Checked == true)
            {
                dll.UpdateDeleteInsertFunctions(
                    " EXEC CHECKtemp_DislastikPisim @PartOfTable='temp_DislastikRadialPisim' "
                    , Properties.Settings.Default.cnn
                );
                SelectQueryToDataGridView("temp_DislastikRadialPisim", dg_radialpisim);
                tc_production.SelectedTab = tabPage2;
                MessageBox.Show("Kontrol Edildi..", tabPage2.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (radioButton3.Checked == true)
            {
                dll.UpdateDeleteInsertFunctions(
                    " EXEC CHECKtemp_DislastikBitirmeOzka @PartOfTable='temp_DislastikBitirmeOZKA1' "
                    , Properties.Settings.Default.cnn
                );
                dll.UpdateDeleteInsertFunctions(
                    " EXEC [dbo].[CHECKtemp_DislastikOnlyHurda] @PartOfTable='temp_DislastikBitirmeOZKA1' "
                    , Properties.Settings.Default.cnn
                );
                dll.UpdateDeleteInsertFunctions(
                    " EXEC [dbo].[CHECKtemp_DislastikOnlyOzur] @PartOfTable='temp_DislastikBitirmeOZKA1' "
                    , Properties.Settings.Default.cnn
                );
                SelectQueryToDataGridView("temp_DislastikBitirmeOZKA1", dg_bitirmeO1);
                tc_production.SelectedTab = tabPage5;
                MessageBox.Show("Kontrol Edildi..", tabPage5.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (radioButton4.Checked == true)
            {
                dll.UpdateDeleteInsertFunctions(
                    " EXEC CHECKtemp_DislastikBitirmeOzka @PartOfTable='temp_DislastikBitirmeOZKA2' "
                    , Properties.Settings.Default.cnn
                );
                dll.UpdateDeleteInsertFunctions(
                    " EXEC [dbo].[CHECKtemp_DislastikOnlyHurda] @PartOfTable='temp_DislastikBitirmeOZKA2' "
                    , Properties.Settings.Default.cnn
                );
                dll.UpdateDeleteInsertFunctions(
                    " EXEC [dbo].[CHECKtemp_DislastikOnlyOzur] @PartOfTable='temp_DislastikBitirmeOZKA2' "
                    , Properties.Settings.Default.cnn
                );
                SelectQueryToDataGridView("temp_DislastikBitirmeOZKA2", dg_bitirmeO2);
                tc_production.SelectedTab = tabPage6;
                MessageBox.Show("Kontrol Edildi..", tabPage6.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ExecuteSPUpToRadioButton();
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISLASTIK.ActiveForm.Close();
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dll.TREEVIEW_ClearBackColor(treeView1);
            treeView1.SelectedNode.ForeColor = Color.Yellow;
            treeView1.SelectedNode.BackColor = Color.FromArgb(73,76,85);
            foreach (Control x in this.groupBox6.Controls)
            {
                if (x is ComboBox)
                {
                    ((ComboBox)x).DataSource = null;
                    ((ComboBox)x).Items.Clear();
                }
            }
            if (treeView1.SelectedNode.Nodes.Count == 0)
                {
                        nodeselected = treeView1.SelectedNode.Text.ToString();
                        parent_nodeselected = treeView1.SelectedNode.Parent.Text.ToString();
                        foreach (Control x in this.groupBox6.Controls)
                        {
                            if (x is ComboBox)
                            {
                                ((ComboBox)x).Enabled = true;
                                ((ComboBox)x).DataSource = null;
                                ((ComboBox)x).Items.Clear();
                            }
                            if (x is Label)
                            {
                                ((Label)x).Enabled = true;
                            }
                        }
                        if(dll.TreeViewFindRoot(treeView1.SelectedNode).Text.ToString() == "GECICI TABLOLAR")
                        {
                            if (parent_nodeselected == "URETIM")
                            {
                                gb_Uretim.Enabled = true;
                                gb_Hurda.Enabled = false;
                                gb_Ozur.Enabled = false;
                                if (nodeselected == "temp_DislastikBiasPisim")
                                {
                                    //tc_production.SelectedTab = tabPage1;
                                    radioButton1.Checked = true;
                                    SelectQueryToDataGridView("temp_DislastikBiasPisim", dg_biaspisim);
                                }
                                if (nodeselected == "temp_DislastikRadialPisim")
                                {
                                    //tc_production.SelectedTab = tabPage2;
                                    radioButton2.Checked = true;
                                    SelectQueryToDataGridView("temp_DislastikRadialPisim", dg_radialpisim);
                                }
                                cmb_nedencode.Enabled = false;
                                cmb_nedenacikla.Enabled = false;
                                lb_neden.Enabled = false;
                                lb_nedenacikla.Enabled = false;
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Date from " + nodeselected + " order by Date", cmb_date, "Date");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Code from " + nodeselected + " order by Code", cmb_code, "Code");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Press from " + nodeselected + " order by Press", cmb_press, "Press");
                            }
                            else if (parent_nodeselected == "OZUR HURDA")
                            {
                                    if (nodeselected == "temp_DislastikBitirmeOZKA1")
                                    {
                                        //tc_production.SelectedTab = tabPage5;
                                        radioButton3.Checked = true;
                                        SelectQueryToDataGridView("temp_DislastikBitirmeOZKA1", dg_bitirmeO1);
                                    }
                                    if (nodeselected == "temp_DislastikBitirmeOZKA2")
                                    {
                                        //tc_production.SelectedTab = tabPage6;
                                        radioButton4.Checked = true;
                                        SelectQueryToDataGridView("temp_DislastikBitirmeOZKA2", dg_bitirmeO2);
                                    }
                                    cmb_nedencode.Enabled = true;
                                    cmb_nedenacikla.Enabled = true;
                                    lb_neden.Enabled = true;
                                    lb_nedenacikla.Enabled = true;
                                    gb_Uretim.Enabled = false;
                                    gb_Hurda.Enabled = true;
                                    gb_Ozur.Enabled = true;
                                if (rb_ekle.Checked == false)
                                {
                                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                                            " select distinct p.Date  "
                                            + " from " + nodeselected + " p "
                                            , cmb_date, "Date");
                                }
                                else
                                {
                                    if (nodeselected == "temp_DislastikBitirmeOZKA1")
                                    {
                                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                                            " select distinct p.Date  "
                                            + " from " + prod_table + " p left join cn_PressIDs cp "
                                            + " on p.Press=cp.Press "
                                            + " where CONVERT(datetime,p.Date, 104)>=CONVERT(datetime,GETDATE()-5, 104) "
                                            + " And cp.Location='OZKA 1' "
                                            , cmb_date, "Date");
                                    }
                                    else if (nodeselected == "temp_DislastikBitirmeOZKA2")
                                    {
                                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                                            " select distinct p.Date  "
                                            + " from " + prod_table + " p left join cn_PressIDs cp "
                                            + " on p.Press=cp.Press "
                                            + " where CONVERT(datetime,p.Date, 104)>=CONVERT(datetime,GETDATE()-5, 104) "
                                            + " And cp.Location='OZKA 2' "
                                            , cmb_date, "Date");
                                    }
                                }
                            }
                        }
                        if (dll.TreeViewFindRoot(treeView1.SelectedNode).Text.ToString() == "GERCEK TABLOLAR")
                        {
                            if (parent_nodeselected == "URETIM")
                            {
                                cmb_nedencode.Enabled = false;
                                cmb_nedenacikla.Enabled = false;
                                lb_neden.Enabled = false;
                                lb_nedenacikla.Enabled = false;
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Date from " + nodeselected + " order by Date", cmb_date, "Date");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Code from " + nodeselected + " order by Code", cmb_code, "Code");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Press from " + nodeselected + " order by Press", cmb_press, "Press");
                            }
                            else if (parent_nodeselected == "OZUR HURDA")
                            {
                                cmb_nedencode.Enabled = true;
                                cmb_nedenacikla.Enabled = true;
                                lb_neden.Enabled = true;
                                lb_nedenacikla.Enabled = true;
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Date from " + nodeselected + " order by Date", cmb_date, "Date");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Code from " + nodeselected + " order by Code", cmb_code, "Code");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Press from " + nodeselected + " order by Press", cmb_press, "Press");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct NedenCode from " + nodeselected + " order by NedenCode", cmb_nedencode, "NedenCode");
                                dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct c.Description from " + nodeselected + " a inner join cn_HurdaOzurCode c on a.NedenCode=c.Code order by c.Description", cmb_nedenacikla, "Description");
                            }
                        }
                }
                else
                {
                    foreach (Control x in this.groupBox6.Controls)
                    {
                        if (x is ComboBox)
                        {
                            ((ComboBox)x).Enabled = false;
                        }
                        if (x is Label)
                        {
                            ((Label)x).Enabled = false;
                        }
                    }
                }
        }

        private void cmb_date_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_date.Text != string.Empty)
            {
                if (treeView1.SelectedNode.Parent.Text.ToString() == "URETIM")
                {
                    dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Code from " + treeView1.SelectedNode.Text.ToString() + " where Date='" + cmb_date.Text + "' order by Code", cmb_code, "Code");
                }
                if (treeView1.SelectedNode.Parent.Text.ToString() == "OZUR HURDA")
                {
                    if (rb_ekle.Checked == false)
                    {
                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                            "Select Distinct p.Code from " + nodeselected + " p "
                            + " where p.Date='" + cmb_date.Text + "' order by p.Code"
                            , cmb_code, "Code");
                    }
                    else
                    {
                        if (nodeselected == "temp_DislastikBitirmeOZKA1")
                        {
                            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                                "Select Distinct p.Code from " + prod_table + " p left join cn_PressIDs cp"
                                + " on p.Press=cp.Press "
                                + " where p.Date='" + cmb_date.Text + "' And cp.Location='OZKA 1' order by p.Code"
                                , cmb_code, "Code");
                        }
                        else if (nodeselected == "temp_DislastikBitirmeOZKA2")
                        {
                            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                                "Select Distinct p.Code from " + prod_table + " p left join cn_PressIDs cp"
                                + " on p.Press=cp.Press "
                                + " where p.Date='" + cmb_date.Text + "' And cp.Location='OZKA 2' order by p.Code"
                                , cmb_code, "Code");
                        }
                    }
                }
            }
        }

        private void cmb_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_date.Text != string.Empty && cmb_code.Text != string.Empty)
            {
                if (treeView1.SelectedNode.Parent.Text.ToString() == "URETIM")
                {
                    dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Press from " + treeView1.SelectedNode.Text.ToString() + " where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' order by Press", cmb_press, "Press");
                }
                if (treeView1.SelectedNode.Parent.Text.ToString() == "OZUR HURDA")
                {
                    if (rb_ekle.Checked == false)
                    {
                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                            "Select Distinct p.Press from " + nodeselected + " p "
                            + " where p.Date='" + cmb_date.Text + "' and p.Code='" + cmb_code.Text + "' order by p.Press"
                            , cmb_press, "Press");
                    }
                    else
                    {
                        if (nodeselected == "temp_DislastikBitirmeOZKA1")
                        {
                            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                                "Select Distinct p.Press from " + prod_table + " p left join cn_PressIDs cp"
                                + " on p.Press=cp.Press "
                                + " where p.Date='" + cmb_date.Text + "' and p.Code='" + cmb_code.Text + "' And cp.Location='OZKA 1' order by p.Press"
                                , cmb_press, "Press");
                        }
                        else if (nodeselected == "temp_DislastikBitirmeOZKA2")
                        {
                            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                                "Select Distinct p.Press from " + prod_table + " p left join cn_PressIDs cp"
                                + " on p.Press=cp.Press "
                                + " where p.Date='" + cmb_date.Text + "' and p.Code='" + cmb_code.Text + "' And cp.Location='OZKA 2' order by p.Press"
                                , cmb_press, "Press");
                        }
                    }
                }
            }
        }
        public void numericare_Clean()
        {
            foreach (Control x in this.gb_Uretim.Controls)
            {
                if (x is NumericUpDown)
                {
                    ((NumericUpDown)x).Text = "0";
                }
            }
            foreach (Control x in this.gb_Ozur.Controls)
            {
                if (x is NumericUpDown)
                {
                    ((NumericUpDown)x).Text = "0";
                }
            }
            foreach (Control x in this.gb_Hurda.Controls)
            {
                if (x is NumericUpDown)
                {
                    ((NumericUpDown)x).Text = "0";
                }
            }
        }
        private void cmb_press_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_date.Text != string.Empty && cmb_code.Text != string.Empty && cmb_press.Text != string.Empty)
            {
                if (treeView1.SelectedNode.Parent.Text.ToString() == "OZUR HURDA")
                {
                    if (rb_ekle.Checked == false)
                    {
                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct NedenCode from " + nodeselected + " where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' and Press='" + cmb_press.Text + "' order by NedenCode", cmb_nedencode, "NedenCode");
                    }
                    else
                    {
                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct '[' + RTRIM(LTRIM(Code)) + ']-' + RTRIM(LTRIM(Description)) as Description from cn_HurdaOzurCode order by '[' + RTRIM(LTRIM(Code)) + ']-' + RTRIM(LTRIM(Description))", cmb_nedenacikla, "Description");
                        //dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Code as NedenCode from " + neden_table + " order by NedenCode", cmb_nedencode, "NedenCode");
                    }
                }
                else
                {
                    if (rb_ekle.Checked == false)
                    {
                        dll.SqlToNumericUpDown(
                            "Select cast(isnull(Fiili_0816,0) as real) as Fiili_0816 from " + nodeselected + " " +
                            "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                            "and Press='" + cmb_press.Text + "' "
                            , Properties.Settings.Default.cnn, nud_Prod0816);
                        dll.SqlToNumericUpDown(
                            "Select cast(isnull(Fiili_1624,0) as real) as Fiili_1624 from " + nodeselected + " " +
                            "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                            "and Press='" + cmb_press.Text + "' "
                            , Properties.Settings.Default.cnn, nud_Prod1624);
                        dll.SqlToNumericUpDown(
                            "Select cast(isnull(Fiili_2408,0) as real) as Fiili_2408 from " + nodeselected + " " +
                            "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                            "and Press='" + cmb_press.Text + "' "
                            , Properties.Settings.Default.cnn, nud_Prod2408);
                    }
                    else
                    {
                        numericare_Clean();
                    }
                }
            }
        }
        //DROPDOWN
        private void cmb_date_DropDown(object sender, EventArgs e)
        {
            cmb_date.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_code_DropDown(object sender, EventArgs e)
        {
            cmb_code.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_press_DropDown(object sender, EventArgs e)
        {
            cmb_press.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_nedencode_DropDown(object sender, EventArgs e)
        {
            cmb_nedencode.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        //CLICK
        private void cmb_date_Click(object sender, EventArgs e)
        {
            cmb_date.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_code_Click(object sender, EventArgs e)
        {
            cmb_code.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void cmb_press_Click(object sender, EventArgs e)
        {
            cmb_press.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void cmb_nedencode_Click(object sender, EventArgs e)
        {
            cmb_nedencode.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        //ENTER
        private void cmb_date_Enter(object sender, EventArgs e)
        {
            cmb_date.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_code_Enter(object sender, EventArgs e)
        {
            cmb_code.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_press_Enter(object sender, EventArgs e)
        {
            cmb_press.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_nedencode_Enter(object sender, EventArgs e)
        {
            cmb_nedencode.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void DataGridPisim_ToGoToLASTIK(DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {
                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                cmbCode = Convert.ToString(selectedRow.Cells["Code"].Value);

                cmb_code.Text = cmbCode;
            }
        }
        public void DataGridPisim_ToCombobox (DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {
                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                cmbDate = Convert.ToString(selectedRow.Cells["Date"].Value);
                cmbCode = Convert.ToString(selectedRow.Cells["Code"].Value);
                cmbPress = Convert.ToString(selectedRow.Cells["Press"].Value);

                cmb_date.Text = cmbDate;
                cmb_code.Text = cmbCode;
                cmb_press.Text = cmbPress;
            }
        }
        public void DataGridOzur_ToCombobox(DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {

                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                    cmbDate = Convert.ToString(selectedRow.Cells["Date"].Value);
                    cmbCode = Convert.ToString(selectedRow.Cells["Code"].Value);
                    cmbPress = Convert.ToString(selectedRow.Cells["Press"].Value);
                    cmbNedenCode = Convert.ToString(selectedRow.Cells["NedenCode"].Value);
                    cmbOzurQty = Convert.ToInt32(selectedRow.Cells["OzurQty"].Value);
                    cmbHurdaQty = Convert.ToInt32(selectedRow.Cells["HurdaQty"].Value);
                    cmbId= Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    cmb_date.Text = cmbDate;
                    cmb_code.Text = cmbCode;
                    cmb_press.Text = cmbPress;
                    cmb_nedencode.Text = cmbNedenCode;
            }
        }
        
        //Radiobuttons
        private void rb_ekle_CheckedChanged(object sender, EventArgs e)
        {
            ActivateComponents();
            select_treeview();
        }
        private void rb_guncelle_CheckedChanged(object sender, EventArgs e)
        {
            ActivateComponents();
            select_treeview();
        }

        private void rb_sil_CheckedChanged(object sender, EventArgs e)
        {
            ActivateComponents_Delete();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.ForeColor == Color.FromArgb(192, 255, 192))
            {
                if (radioButton1.Checked == true)
                {
                    DialogResult dialogResult = MessageBox.Show("BIAS Gercek veri alanına eklenicek!", "BIAS Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                            Insert_tbl_DislastikProduction_Pisim("temp_DislastikBiasPisim");

                            SelectQueryToDataGridView("temp_DislastikBiasPisim", dg_biaspisim);
                            tc_production.SelectedTab = tabPage1;
                            MessageBox.Show("Veri yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                if (radioButton2.Checked == true)
                {
                    DialogResult dialogResult = MessageBox.Show("RADIAL Gercek veri alanına eklenicek!", "RADIAL Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                            Insert_tbl_DislastikProduction_Pisim("temp_DislastikRadialPisim");

                            SelectQueryToDataGridView("temp_DislastikRadialPisim", dg_radialpisim);
                            tc_production.SelectedTab = tabPage2;
                            MessageBox.Show("Veri yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.DoEvents();
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                //Bitirme process

                        if (radioButton3.Checked == true)
                        {
                            if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
                            {
                                DialogResult dialogResult = MessageBox.Show("BITIRME OZKA1 Gercek veri alanına eklenicek!", "BITIRME OZKA1 Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Insert_tbl_DislastikOzur("temp_DislastikBitirmeOZKA1");
                                    Insert_tbl_DislastikHurda("temp_DislastikBitirmeOZKA1");

                                    SelectQueryToDataGridView("temp_DislastikBitirmeOZKA1", dg_bitirmeO1);
                                    tc_production.SelectedTab = tabPage5;
                                    MessageBox.Show("Veri yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Application.DoEvents();
                                }
                                else if (dialogResult == DialogResult.No)
                                {

                                }
                            }
                            else
                            {
                                MessageBox.Show("Please contact to admin !", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        if (radioButton4.Checked == true)
                        {
                            if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
                            {
                                DialogResult dialogResult = MessageBox.Show("BITIRME OZKA2 Gercek veri alanına eklenicek!", "BITIRME OZKA2 Yukleme Alanı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Insert_tbl_DislastikOzur("temp_DislastikBitirmeOZKA2");
                                    Insert_tbl_DislastikHurda("temp_DislastikBitirmeOZKA2");

                                    SelectQueryToDataGridView("temp_DislastikBitirmeOZKA2", dg_bitirmeO2);
                                    tc_production.SelectedTab = tabPage6;
                                    MessageBox.Show("Veri yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Application.DoEvents();
                                }
                                else if (dialogResult == DialogResult.No)
                                {

                                }
                            }
                            else
                            {
                                MessageBox.Show("Please contact to admin !", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
            }
            else if (button3.ForeColor == System.Drawing.Color.DarkRed)
            {
                MessageBox.Show("you dont have access !", "ACCESS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void QueryToComboBox_SelectNedenAcikla()
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = "Select Distinct Description as NedenCode from cn_HurdaOzurCode where Code='" + cmb_nedencode.Text + "' order by Description";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                cmb_nedenacikla.SelectedText = dataReader[0].ToString();
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        private void cmb_nedencode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_date.Text != string.Empty && cmb_code.Text != string.Empty && cmb_press.Text != string.Empty)
            {
                //QueryToComboBox_SelectNedenAcikla();
                if (treeView1.SelectedNode.Parent.Text.ToString() == "OZUR HURDA")
                {
                    if (rb_ekle.Checked == false)
                    {
                        dll.SqlToNumericUpDown(
                            "Select cast(isnull(OzurQty,0) as real) as OzurQty from " + nodeselected + " " +
                            "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                            "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                            , Properties.Settings.Default.cnn,nud_OzurQty);
                        dll.SqlToNumericUpDown(
                            "Select cast(isnull(HurdaQty,0) as real) as HurdaQty from " + nodeselected + " " +
                            "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                            "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                            , Properties.Settings.Default.cnn, nud_HurdaQty);
                        dll.SqlToNumericUpDown(
                            "Select cast(isnull(DA,0) as real) as DA from " + nodeselected + " " +
                            "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                            "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                            , Properties.Settings.Default.cnn, nud_DAQty);
                        dll.SqlToNumericUpDown(
                            "Select cast(isnull(TT,0) as real) as TT from " + nodeselected + " " +
                            "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                            "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                            , Properties.Settings.Default.cnn, nud_TTQty);
                        dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Description from cn_HurdaOzurCode where Code='" + cmb_nedencode.Text + "' order by Description", cmb_nedenacikla, "Description");
                    }
                    else
                    {
                        numericare_Clean();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.ForeColor == Color.FromArgb(192, 255, 192))
            {
                if (rb_ekle.Checked == true)
                {
                    if (parent_nodeselected == "URETIM")
                    {
                        if (Int32.Parse(nud_Prod0816.Text) > 0 || Int32.Parse(nud_Prod1624.Text) > 0 || Int32.Parse(nud_Prod2408.Text) > 0)
                        {
                            if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBiasPisim")
                            {
                                DialogResult dialogResult = MessageBox.Show("Veri eklenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Insert_temp_Dislastik_Pisim_OneByOne(treeView1.SelectedNode.Text.ToString());
                                    SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_biaspisim);
                                    tc_production.SelectedTab = tabPage1;
                                    MessageBox.Show("Eklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ExecuteSPUpToRadioButton();
                                    Application.DoEvents();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                }
                            }
                            else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikRadialPisim")
                            {
                                DialogResult dialogResult = MessageBox.Show("Veri eklenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Insert_temp_Dislastik_Pisim_OneByOne(treeView1.SelectedNode.Text.ToString());
                                    SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_radialpisim);
                                    tc_production.SelectedTab = tabPage2;
                                    MessageBox.Show("Eklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ExecuteSPUpToRadioButton();
                                    Application.DoEvents();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Değer Girmedin !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if(parent_nodeselected == "OZUR HURDA")
                    {
                        if (Int32.Parse(nud_OzurQty.Text) > 0 || Int32.Parse(nud_HurdaQty.Text) > 0 || Int32.Parse(nud_DAQty.Text) > 0 || Int32.Parse(nud_TTQty.Text) > 0)
                        {
                            if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA1")
                            {
                                DialogResult dialogResult = MessageBox.Show("Veri eklenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Insert_temp_Dislastik_Bitirme_OneByOne(treeView1.SelectedNode.Text.ToString());
                                    SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO1);
                                    tc_production.SelectedTab = tabPage5;
                                    MessageBox.Show("Eklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ExecuteSPUpToRadioButton();
                                    Application.DoEvents();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                }
                            }
                            else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA2")
                            {
                                DialogResult dialogResult = MessageBox.Show("Veri eklenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Insert_temp_Dislastik_Bitirme_OneByOne(treeView1.SelectedNode.Text.ToString());
                                    SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO2);
                                    tc_production.SelectedTab = tabPage6;
                                    MessageBox.Show("Eklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ExecuteSPUpToRadioButton();
                                    Application.DoEvents();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Değer Girmedin !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (rb_guncelle.Checked == true)
                {
                    if (parent_nodeselected == "URETIM")
                    {
                        if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBiasPisim")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri güncellenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Update_temp_Dislastik_Pisim_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_biaspisim);
                                tc_production.SelectedTab = tabPage1;
                                MessageBox.Show("Güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ExecuteSPUpToRadioButton();
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                        else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikRadialPisim")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri güncellenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Update_temp_Dislastik_Pisim_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_radialpisim);
                                tc_production.SelectedTab = tabPage2;
                                MessageBox.Show("Güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ExecuteSPUpToRadioButton();
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                    else if (parent_nodeselected == "OZUR HURDA")
                    {
                        if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA1")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri güncellenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Update_temp_Dislastik_Bitirme_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO1);
                                tc_production.SelectedTab = tabPage5;
                                MessageBox.Show("Güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ExecuteSPUpToRadioButton();
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                        else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA2")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Güncellenicek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Update_temp_Dislastik_Bitirme_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO2);
                                tc_production.SelectedTab = tabPage6;
                                MessageBox.Show("Güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ExecuteSPUpToRadioButton();
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                }
                if (rb_sil.Checked == true)
                {
                    if (parent_nodeselected == "URETIM")
                    {
                        if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBiasPisim")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Delete_temp_Dislastik_Pisim_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_biaspisim);
                                tc_production.SelectedTab = tabPage1;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                        else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikRadialPisim")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Delete_temp_Dislastik_Pisim_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_radialpisim);
                                tc_production.SelectedTab = tabPage2;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                    else if (parent_nodeselected == "OZUR HURDA")
                    {
                        if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA1")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Delete_temp_Dislastik_Bitirme_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO1);
                                tc_production.SelectedTab = tabPage5;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                        else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA2")
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                Delete_temp_Dislastik_Bitirme_OneByOne(treeView1.SelectedNode.Text.ToString());

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO2);
                                tc_production.SelectedTab = tabPage6;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                }
            }
            else if (button3.ForeColor == System.Drawing.Color.DarkRed)
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bIASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fr == null || fr.Text == "")
            {
                fr = new ToKnowFormat();
                fr.Show();
            }
            else if (dll.CheckOpened(fr.Text))
            {
                fr.WindowState = FormWindowState.Normal;
                fr.Show();
                fr.Focus();
            }
            fr.Text = "BIAS FORMAT";
            fr.formtag = fr.Text;
            dll.QueryToDataGrid(fr.dg_Format,Properties.Settings.Default.cnn, "Select Top 0 Date,Press,Size,Code,Ebatlar,Birim_KG,STD_Adet,STD_KG,Fiili_0816,Fiili_1624,Fiili_2408,Fiili_GenelTP from temp_DislastikBiasPisim");
        }

        private void rADIALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fr == null || fr.Text == "")
            {
                fr = new ToKnowFormat();
                fr.Show();
            }
            else if (dll.CheckOpened(fr.Text))
            {
                fr.WindowState = FormWindowState.Normal;
                fr.Show();
                fr.Focus();
            }
            fr.Text = "RADIAL FORMAT";
            fr.formtag = fr.Text;
            dll.QueryToDataGrid(fr.dg_Format, Properties.Settings.Default.cnn, "Select Top 0 Date,Press,Size,Code,Ebatlar,Birim_KG,STD_Adet,STD_KG,Fiili_0816,Fiili_1624,Fiili_2408,Fiili_GenelTP from temp_DislastikRadialPisim");
        }

        private void bITIRMEOZKA1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fr == null || fr.Text == "")
            {
                fr = new ToKnowFormat();
                fr.Show();
            }
            else if (dll.CheckOpened(fr.Text))
            {
                fr.WindowState = FormWindowState.Normal;
                fr.Show();
                fr.Focus();
            }
            fr.Text = "BITIRME OZKA 1 FORMAT";
            fr.formtag = fr.Text;
            dll.QueryToDataGrid(fr.dg_Format, Properties.Settings.Default.cnn, "Select Top 0 Date,Time,Code,Press,NedenCode,OzurQty,HurdaQty,DA,TT from temp_DislastikBitirmeOZKA1");
        }

        private void bITIRMEOZKA2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fr == null || fr.Text == "")
            {
                fr = new ToKnowFormat();
                fr.Show();
            }
            else if (dll.CheckOpened(fr.Text))
            {
                fr.WindowState = FormWindowState.Normal;
                fr.Show();
                fr.Focus();
            }
            fr.Text = "BITIRME OZKA 2 FORMAT";
            fr.formtag = fr.Text;
            dll.QueryToDataGrid(fr.dg_Format, Properties.Settings.Default.cnn, "Select Top 0 Date,Time,Code,Press,NedenCode,OzurQty,HurdaQty,DA,TT from temp_DislastikBitirmeOZKA2");
        }

        private void kAYITISILToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (ebat == null || ebat.Text == "")
                {
                    ebat = new EbatKg();
                    if (tc_production.SelectedTab == tabPage1) { DataGridPisim_ToCombobox(dg_biaspisim); }
                    if (tc_production.SelectedTab == tabPage2) { DataGridPisim_ToCombobox(dg_radialpisim); }
                    if (tc_production.SelectedTab == tabPage5) { DataGridPisim_ToCombobox(dg_bitirmeO1); }
                    if (tc_production.SelectedTab == tabPage6) { DataGridPisim_ToCombobox(dg_bitirmeO2); }
                    ebat.cmb_code.Text = cmbCode;
                    if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { ebat.eKLEToolStripMenuItem.Enabled = true; }
                    ebat.WindowState = FormWindowState.Normal;
                    ebat.Show();
                }
                else if (dll.CheckOpened(ebat.Text))
                {
                    if (tc_production.SelectedTab == tabPage1) { DataGridPisim_ToCombobox(dg_biaspisim); }
                    if (tc_production.SelectedTab == tabPage2) { DataGridPisim_ToCombobox(dg_radialpisim); }
                    if (tc_production.SelectedTab == tabPage5) { DataGridPisim_ToCombobox(dg_bitirmeO1); }
                    if (tc_production.SelectedTab == tabPage6) { DataGridPisim_ToCombobox(dg_bitirmeO2); }
                    ebat.cmb_code.Text = cmbCode;
                    if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { ebat.eKLEToolStripMenuItem.Enabled = true; }
                    ebat.WindowState = FormWindowState.Normal;
                    ebat.Show();
                    ebat.Focus();
                }
        }

        private void tabloyuTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabloyuTemizleToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                    DialogResult dialogResult = MessageBox.Show("Tablodaki bütün veriler silinecek !", "VERI ALANI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        rightclick_cleantable();
                        rightclick();
                        MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tc_production.SelectedTab = tabPage1;
            Deactivate_IslemCinsiComponents();
            //if (treeView1.Nodes.Count>0)
            //{
            //    if (rb_ekle.Checked == true || rb_guncelle.Checked == true || rb_sil.Checked == true)
            //    {
            //        if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            //        {
            //            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[1];
            //        }
            //    }
            //}
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tc_production.SelectedTab = tabPage2;
            Deactivate_IslemCinsiComponents();

            //if (treeView1.Nodes.Count > 0)
            //{
            //    if (rb_ekle.Checked == true || rb_guncelle.Checked == true || rb_sil.Checked == true)
            //    {
            //        if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            //        {
            //            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[1];
            //        }
            //    }
            //}
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            tc_production.SelectedTab = tabPage5;
            Activate_IslemCinsiComponents();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            tc_production.SelectedTab = tabPage6;
            Activate_IslemCinsiComponents();
        }

        private void withAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptV = dll.InputDialog("File Name:", "FILE Property");
                string Dt = DateTime.Now.ToString("yyyy-MM-dd");
                string promptValue = promptV + " @" + Dt;
                SaveFileDialog fdlg = new SaveFileDialog();
                fdlg.FileName = promptValue;
                fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    filepath = Path.GetDirectoryName(fdlg.FileName);
                    if (fdlg.FileName != null)
                    {
                        if (tc_production.SelectedTab == tabPage1)
                        {
                            dll.ExportExcel(fdlg.FileName, dg_biaspisim);
                        }
                        else if (tc_production.SelectedTab == tabPage2)
                        {
                            dll.ExportExcel(fdlg.FileName, dg_radialpisim);
                        }
                        else if (tc_production.SelectedTab == tabPage5)
                        {
                            dll.ExportExcel(fdlg.FileName, dg_bitirmeO1);
                        }
                        else if (tc_production.SelectedTab == tabPage6)
                        {
                            dll.ExportExcel(fdlg.FileName, dg_bitirmeO2);
                        }
                    Application.DoEvents();
                    dll.SendEmail(filepath + "\\" + promptValue + ".xlsx", promptValue, "", "", "");
                    }
                }
        }

        private void cmb_nedenacikla_DropDown(object sender, EventArgs e)
        {
            cmb_nedenacikla.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_nedenacikla_Click(object sender, EventArgs e)
        {
            cmb_nedenacikla.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_nedenacikla_Enter(object sender, EventArgs e)
        {
            cmb_nedenacikla.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void QueryToComboBox_SelectNedenCode()
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = "Select Distinct Code as NedenCode from cn_HurdaOzurCode where Description='" + cmb_nedenacikla.Text + "' order by Code";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                cmb_nedencode.SelectedText = dataReader[0].ToString();
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
        private void cmb_nedenacikla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_date.Text != string.Empty && cmb_code.Text != string.Empty && cmb_press.Text != string.Empty)
            {
                int first = cmb_nedenacikla.Text.LastIndexOf("]-") +2;
                if (cmb_nedenacikla.Text.Length > first)
                {
                    int last = cmb_nedenacikla.Text.Length - first;
                    string findvalue = cmb_nedenacikla.Text.Substring(first, last);
                    dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Distinct Code as NedenCode from cn_HurdaOzurCode where Description='" + findvalue + "' order by Code", cmb_nedencode, "NedenCode");
                }
            }
        }
        public void bitirmedateupdate (string tb)
        {
            dll.UpdateDeleteInsertFunctions(
                " update " + tb + " set "
                + " Date='" + dateTimePicker_bitirme.Value.ToString("dd.MM.yyyy") + "' "
                + " from " + tb + " t "
                , Properties.Settings.Default.cnn
            );
            dll.UpdateDeleteInsertFunctions(
                " EXEC CHECKtemp_DislastikBitirmeOzka @PartOfTable='" +tb + "' "
                , Properties.Settings.Default.cnn
            );
            rightclick();
        }
        private void btn_bitirmedate_Click(object sender, EventArgs e)
        {
            if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (tc_production.SelectedTab == tabPage5 || tc_production.SelectedTab == tabPage6)
                {
                    DialogResult dialogResult = MessageBox.Show(treeView1.SelectedNode.Text.ToString() + " Tablosunda ki Tarihler " + dateTimePicker_bitirme.Value.ToString("dd.MM.yyyy") + " göre güncellensin istiyormusun ?", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        bitirmedateupdate(treeView1.SelectedNode.Text.ToString());
                        ExecuteSPUpToRadioButton();
                        MessageBox.Show(dateTimePicker_bitirme.Value.ToString("dd.MM.yyyy") + " göre Güncellendi.", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.DoEvents();
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Sadece Bitirme Verileri içindir !", "BILGI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void hATAPRESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hATAPRESToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (pres == null || pres.Text == "")
                {
                    pres = new Press();
                    if (tc_production.SelectedTab == tabPage1) { DataGridPisim_ToCombobox(dg_biaspisim); }
                    if (tc_production.SelectedTab == tabPage2) { DataGridPisim_ToCombobox(dg_radialpisim); }
                    if (tc_production.SelectedTab == tabPage5) { DataGridPisim_ToCombobox(dg_bitirmeO1); }
                    if (tc_production.SelectedTab == tabPage6) { DataGridPisim_ToCombobox(dg_bitirmeO2); }
                    pres.txt_press.Text = cmbPress;
                    pres.WindowState = FormWindowState.Normal;
                    pres.Show();
                }
                else if (dll.CheckOpened(pres.Text))
                {
                    if (tc_production.SelectedTab == tabPage1) { DataGridPisim_ToCombobox(dg_biaspisim); }
                    if (tc_production.SelectedTab == tabPage2) { DataGridPisim_ToCombobox(dg_radialpisim); }
                    if (tc_production.SelectedTab == tabPage5) { DataGridPisim_ToCombobox(dg_bitirmeO1); }
                    if (tc_production.SelectedTab == tabPage6) { DataGridPisim_ToCombobox(dg_bitirmeO2); }
                    pres.txt_press.Text = cmbPress;
                    pres.WindowState = FormWindowState.Normal;
                    pres.Show();
                    pres.Focus();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void withContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptV = dll.InputDialog("File Name:", "FILE Property");
            string Dt = DateTime.Now.ToString("yyyy-MM-dd");
            string promptValue = promptV + " @" + Dt;
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.FileName = promptValue;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                filepath = Path.GetDirectoryName(fdlg.FileName);
                if (fdlg.FileName != null)
                {
                    if (tc_production.SelectedTab == tabPage1)
                    {
                        dll.ExportExcel(fdlg.FileName, dg_biaspisim);
                    }
                    else if (tc_production.SelectedTab == tabPage2)
                    {
                        dll.ExportExcel(fdlg.FileName, dg_radialpisim);
                    }
                    else if (tc_production.SelectedTab == tabPage5)
                    {
                        dll.ExportExcel(fdlg.FileName, dg_bitirmeO1);
                    }
                    else if (tc_production.SelectedTab == tabPage6)
                    {
                        dll.ExportExcel(fdlg.FileName, dg_bitirmeO2);
                    }
                    Application.DoEvents();
                }
            }
        }
        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sİLToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (parent_nodeselected == "URETIM")
                {
                    if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBiasPisim")
                    {
                        if (dg_biaspisim.RowCount > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                DataGridPisim_ToCombobox(dg_biaspisim);
                                Delete_temp_Dislastik_Pisim_OneByOneCHOOSENDATAGRID(treeView1.SelectedNode.Text.ToString());
                                calculate_Ozka1prodQty();

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_biaspisim);
                                tc_production.SelectedTab = tabPage1;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                    else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikRadialPisim")
                    {
                        if (dg_radialpisim.RowCount > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                DataGridPisim_ToCombobox(dg_radialpisim);
                                Delete_temp_Dislastik_Pisim_OneByOneCHOOSENDATAGRID(treeView1.SelectedNode.Text.ToString());
                                calculate_Ozka2prodQty();

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_radialpisim);
                                tc_production.SelectedTab = tabPage2;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                }
                else if (parent_nodeselected == "OZUR HURDA")
                {
                    if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA1")
                    {
                        if (dg_bitirmeO1.RowCount > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                DataGridOzur_ToCombobox(dg_bitirmeO1);
                                Delete_temp_Dislastik_Bitirme_OneByOneCHOOSENDATAGRID(treeView1.SelectedNode.Text.ToString());
                                lb_prodqty.Text = "0";

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO1);
                                tc_production.SelectedTab = tabPage5;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                    else if (treeView1.SelectedNode.Text.ToString() == "temp_DislastikBitirmeOZKA2")
                    {
                        if (dg_bitirmeO2.RowCount > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Veri Silinecek!", treeView1.SelectedNode.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                DataGridOzur_ToCombobox(dg_bitirmeO2);
                                Delete_temp_Dislastik_Bitirme_OneByOneCHOOSENDATAGRID(treeView1.SelectedNode.Text.ToString());
                                lb_prodqty.Text = "0";

                                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO2);
                                tc_production.SelectedTab = tabPage6;
                                MessageBox.Show("Silindi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.DoEvents();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void select_treeview()
        {
            rightclick();
                if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
                {
                    if (tc_production.SelectedTab == tabPage1)
                    {
                        treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[0];
                    }
                    if (tc_production.SelectedTab == tabPage2)
                    {
                        treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[1];
                    }
                    if (tc_production.SelectedTab == tabPage5)
                    {
                        treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[0];
                    }
                    if (tc_production.SelectedTab == tabPage6)
                    {
                        treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[1];
                    }
                }
                else
                {
                    if (tc_production.SelectedTab == tabPage5)
                    {
                        treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[0];
                    }
                    if (tc_production.SelectedTab == tabPage6)
                    {
                        treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[1];
                    }
                }
        }
        private void tc_production_Selected(object sender, TabControlEventArgs e)
        {
            select_treeview();
        }
        private void dg_biaspisim_DoubleClick(object sender, EventArgs e)
        {

        }
        private void dg_radialpisim_DoubleClick(object sender, EventArgs e)
        {

        }
        private void dg_bitirmeO1_DoubleClick(object sender, EventArgs e)
        {
            dll.SqlToNumericUpDown(
                "Select cast(isnull(OzurQty,0) as real) as OzurQty from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_OzurQty);
            dll.SqlToNumericUpDown(
                "Select cast(isnull(HurdaQty,0) as real) as HurdaQty from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_HurdaQty);
            dll.SqlToNumericUpDown(
                "Select cast(isnull(DA,0) as real) as DA from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_DAQty);
            dll.SqlToNumericUpDown(
                "Select cast(isnull(TT,0) as real) as TT from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_TTQty);
        }

        private void dg_bitirmeO2_DoubleClick(object sender, EventArgs e)
        {
            dll.SqlToNumericUpDown(
                "Select cast(isnull(OzurQty,0) as real) as OzurQty from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_OzurQty);
            dll.SqlToNumericUpDown(
                "Select cast(isnull(HurdaQty,0) as real) as HurdaQty from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_HurdaQty);
            dll.SqlToNumericUpDown(
                "Select cast(isnull(DA,0) as real) as DA from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_DAQty);
            dll.SqlToNumericUpDown(
                "Select cast(isnull(TT,0) as real) as TT from " + nodeselected + " " +
                "where Date='" + cmb_date.Text + "' and Code='" + cmb_code.Text + "' " +
                "and Press='" + cmb_press.Text + "' and NedenCode='" + cmb_nedencode.Text + "' and ID=LTRIM(RTRIM('" + cmbId + "')) "
                , Properties.Settings.Default.cnn, nud_TTQty);
        }

        private void dg_biaspisim_Click(object sender, EventArgs e)
        {
            if (dg_biaspisim.RowCount > 0)
            {
                if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
                {
                    rb_guncelle.Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[0];
                    DataGridPisim_ToCombobox(dg_biaspisim);
                }
            }
        }

        private void dg_radialpisim_Click(object sender, EventArgs e)
        {
            if (dg_radialpisim.RowCount > 0)
            {
                if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
                {
                    rb_guncelle.Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[1];
                    DataGridPisim_ToCombobox(dg_radialpisim);
                }
            }
        }

        private void dg_bitirmeO1_Click(object sender, EventArgs e)
        {
            if (dg_bitirmeO1.RowCount > 0)
            {
                if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
                {
                    rb_guncelle.Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[0];
                    DataGridOzur_ToCombobox(dg_bitirmeO1);
                }
                else
                {
                    rb_guncelle.Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[0];
                    DataGridOzur_ToCombobox(dg_bitirmeO1);
                }
            }
        }

        private void dg_bitirmeO2_Click(object sender, EventArgs e)
        {
            if (dg_bitirmeO2.RowCount > 0)
            {
                if (kAYITISILToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
                {
                    rb_guncelle.Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[1];
                    DataGridOzur_ToCombobox(dg_bitirmeO2);
                }
                else
                {
                    rb_guncelle.Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0].Nodes[1];
                    DataGridOzur_ToCombobox(dg_bitirmeO2);
                }
            }
        }
        public void calculate_Ozka1prodQty()
        {
            dll.QueryToLabel(
               " SELECT SUM((cast(ISNULL([Fiili_0816],0) as real) + cast(ISNULL([Fiili_1624],0) as real) + cast(ISNULL([Fiili_2408],0) as real))) as total "
                + " FROM [QCDB].[dbo].[temp_DislastikBiasPisim] "
            , Properties.Settings.Default.cnn
            , lb_prodqty
           );
        }
        public void calculate_Ozka2prodQty()
        {
            dll.QueryToLabel(
               " SELECT SUM((cast(ISNULL([Fiili_0816],0) as real) + cast(ISNULL([Fiili_1624],0) as real) + cast(ISNULL([Fiili_2408],0) as real))) as total "
                + " FROM [QCDB].[dbo].[temp_DislastikRadialPisim] "
            , Properties.Settings.Default.cnn
            , lb_prodqty
           );
        }

        private void vERIKONTROLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vERIKONTROLToolStripMenuItem.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (tc == null || tc.Text == "")
                {
                    tc = new TyreControl();
                    if (tc_production.SelectedTab == tabPage1)
                    {
                        if (dg_biaspisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_biaspisim);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                        }
                    }
                    if (tc_production.SelectedTab == tabPage2)
                    {
                        if (dg_radialpisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_radialpisim);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                        }
                    }
                    if (tc_production.SelectedTab == tabPage5)
                    {
                        if (dg_bitirmeO1.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO1);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            if (cmbOzurQty > 0) { tc.lst_ozurneden.Items.Add(cmbNedenCode); }
                            if (cmbHurdaQty > 0) { tc.lst_hurdaneden.Items.Add(cmbNedenCode); }
                        }
                    }
                    if (tc_production.SelectedTab == tabPage6)
                    {
                        if (dg_bitirmeO2.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO2);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            if (cmbOzurQty > 0) { tc.lst_ozurneden.Items.Add(cmbNedenCode); }
                            if (cmbHurdaQty > 0) { tc.lst_hurdaneden.Items.Add(cmbNedenCode); }
                        }
                    }
                    tc.WindowState = FormWindowState.Normal;
                    tc.Show();
                }
                else if (dll.CheckOpened(tc.Text))
                {
                    if (tc_production.SelectedTab == tabPage1)
                    {
                        if (dg_biaspisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_biaspisim);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                        }
                    }
                    if (tc_production.SelectedTab == tabPage2)
                    {
                        if (dg_radialpisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_radialpisim);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                        }
                    }
                    if (tc_production.SelectedTab == tabPage5)
                    {
                        if (dg_bitirmeO1.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO1);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            if (cmbOzurQty > 0) { tc.lst_ozurneden.Items.Add(cmbNedenCode); }
                            if (cmbHurdaQty > 0) { tc.lst_hurdaneden.Items.Add(cmbNedenCode); }
                        }
                    }
                    if (tc_production.SelectedTab == tabPage6)
                    {
                        if (dg_bitirmeO2.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO2);
                            tc.dateTimePicker_bitirme.Value = Convert.ToDateTime(cmbDate);
                            tc.cmb_tarihcontrol.Text = cmbDate;
                            tc.cmb_lastikcontrol.Text = cmbCode;
                            tc.cmb_presscontrol.Text = cmbPress;
                            if (cmbOzurQty > 0) { tc.lst_ozurneden.Items.Add(cmbNedenCode); }
                            if (cmbHurdaQty > 0) { tc.lst_hurdaneden.Items.Add(cmbNedenCode); }
                        }
                    }
                    tc.WindowState = FormWindowState.Normal;
                    tc.Show();
                    tc.Focus();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Kalite Müdüründen Yetki Alın !", "YETKI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void calculate_blockedreports()
        {
            if (tc_production.SelectedTab == tabPage1)
            {
                if (dg_biaspisim.RowCount > 0)
                {
                    DataGridPisim_ToCombobox(dg_biaspisim);
                    dll.QueryToLabel(
                       " SELECT sum(1) as sayi FROM [QCDB].[dbo].[cn_RaporEngel] WHERE DATE='" + cmbDate + "' and Active=1 "
                    , Properties.Settings.Default.cnn
                    , lb_engelrapor
                   );
                }
            }
            if (tc_production.SelectedTab == tabPage2)
            {
                if (dg_radialpisim.RowCount > 0)
                {
                    DataGridPisim_ToCombobox(dg_radialpisim);
                    dll.QueryToLabel(
                       " SELECT sum(1) as sayi FROM [QCDB].[dbo].[cn_RaporEngel] WHERE DATE='" + cmbDate + "' and Active=1 "
                    , Properties.Settings.Default.cnn
                    , lb_engelrapor
                   );
                }
            }
            if (tc_production.SelectedTab == tabPage5)
            {
                if (dg_bitirmeO1.RowCount > 0)
                {
                    DataGridOzur_ToCombobox(dg_bitirmeO1);
                    dll.QueryToLabel(
                       " SELECT sum(1) as sayi FROM [QCDB].[dbo].[cn_RaporEngel] WHERE DATE='" + cmbDate + "' and Active=1 "
                    , Properties.Settings.Default.cnn
                    , lb_engelrapor
                   );
                }
            }
            if (tc_production.SelectedTab == tabPage6)
            {
                if (dg_bitirmeO2.RowCount > 0)
                {
                    DataGridOzur_ToCombobox(dg_bitirmeO2);
                    dll.QueryToLabel(
                       " SELECT sum(1) as sayi FROM [QCDB].[dbo].[cn_RaporEngel] WHERE DATE='"+cmbDate+"' and Active=1 "
                    , Properties.Settings.Default.cnn
                    , lb_engelrapor
                   );
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.ForeColor == System.Drawing.Color.DarkRed)
            {
                if (re == null || re.Text == "")
                {
                    re = new RaporEngel();
                    if (tc_production.SelectedTab == tabPage1)
                    {
                        if (dg_biaspisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_biaspisim);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    if (tc_production.SelectedTab == tabPage2)
                    {
                        if (dg_radialpisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_radialpisim);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    if (tc_production.SelectedTab == tabPage5)
                    {
                        if (dg_bitirmeO1.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO1);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    if (tc_production.SelectedTab == tabPage6)
                    {
                        if (dg_bitirmeO2.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO2);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    re.WindowState = FormWindowState.Normal;
                    re.Show();
                }
                else if (dll.CheckOpened(re.Text))
                {
                    if (tc_production.SelectedTab == tabPage1)
                    {
                        if (dg_biaspisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_biaspisim);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    if (tc_production.SelectedTab == tabPage2)
                    {
                        if (dg_radialpisim.RowCount > 0)
                        {
                            DataGridPisim_ToCombobox(dg_radialpisim);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    if (tc_production.SelectedTab == tabPage5)
                    {
                        if (dg_bitirmeO1.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO1);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    if (tc_production.SelectedTab == tabPage6)
                    {
                        if (dg_bitirmeO2.RowCount > 0)
                        {
                            DataGridOzur_ToCombobox(dg_bitirmeO2);
                            re.dateTimePicker_RaporOnayDate.Value = Convert.ToDateTime(cmbDate);
                        }
                    }
                    re.WindowState = FormWindowState.Normal;
                    re.Show();
                    re.Focus();
                }
            }

        }
        public void SelectedUpdateDataGridView(DataGridView dg,string tb)
        {
            foreach (DataGridViewRow row in dg.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                dll.UpdateDeleteInsertFunctions(
                    " update " + tb + " set "
                    + " Date='" + dateTimePicker_bitirme.Value.ToString("dd.MM.yyyy") + "' "
                    + " from " + tb + " t where t.ID=LTRIM(RTRIM('" + value1 + "')) "
                    , Properties.Settings.Default.cnn
                );
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if(tc_production.SelectedTab == tabPage5)
            {
                SelectedUpdateDataGridView(dg_bitirmeO1, treeView1.SelectedNode.Text.ToString());
                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO1);
                MessageBox.Show("Tarihler güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tc_production.SelectedTab == tabPage6)
            {
                SelectedUpdateDataGridView(dg_bitirmeO2, treeView1.SelectedNode.Text.ToString());
                SelectQueryToDataGridView(treeView1.SelectedNode.Text.ToString(), dg_bitirmeO2);
                MessageBox.Show("Tarihler güncellendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}