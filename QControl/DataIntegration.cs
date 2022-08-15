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
    public partial class DataIntegration : Form
    {
        public string TempUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Replace("OZKA\\", "");
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private string sql = null;
        public string temptable;
        private int cnt;
        FromDLL dll = new FromDLL();
        public DataIntegration()
        {
            InitializeComponent();
        }
        public void QueryToListView()
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = "Select t.TableName "
                + "From "
                + "( "
                + "SELECT SO.NAME as TableName  FROM sys.objects SO "
                + "WHERE SO.TYPE = 'U' and LEFT(SO.name,4)='temp' "
                + ") t "
                + "left join"
                + "( "
                + "select TEMP "
                + "from zUserTEMP "
                + ") n "
                + "on t.TableName=n.TEMP collate SQL_Latin1_General_CP1_CI_AS "
                + "where n.TEMP is null ";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            lstView_Tablolar.Items.Clear();
            cnt = 0;
            while (dataReader.Read())
            {
                cnt = cnt + 1;

                lstView_Tablolar.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            lb_tablosayisi.Text = cnt.ToString();
        }
        public void VerifyTemp()
        {
            if (label2.Text!="")
            {
                Image resim = Properties.Resources.success;
                pictureBox1.BackgroundImage = resim;
            }
            else
            {
                Image resim = Properties.Resources.delete;
                pictureBox1.BackgroundImage = resim;
            }
        }
        private void DataIntegration_Load(object sender, EventArgs e)
        {
            QueryToListView();
            UserDefineTempTb();
            VerifyTemp();
            CallTablesFromDB();
        }
        public void UserDefineTempTb()
        {
            dll.UserDefineToTempTable(Properties.Settings.Default.cnn,label2);
        }
        public void Insert_tempDislastik()
        {
            //" + lstView_Tablolar.SelectedItems[0].Text + "
            dll.UpdateDeleteInsertFunctions(
                " Insert into temp_dislastik( "
                + " Date,Code,Press,HurdaOzurCode,ProdQty,OzurQty,HurdaQty "
                + " ) "
                + " select  "
                + " LTRIM(RTRIM(Date)),LTRIM(RTRIM(Code)),LTRIM(RTRIM(Press)),LTRIM(RTRIM(HurdaOzurCode)),LTRIM(RTRIM(ProdQty)),LTRIM(RTRIM(OzurQty)),LTRIM(RTRIM(HurdaQty)) "
                + " from  " + label2.Text + " t "
                , Properties.Settings.Default.cnn
            );
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Gecici Veri Alanı temizlenicek !", "Durum Bilgisi", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions("delete from temp_dislastik ", Properties.Settings.Default.cnn);
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Select file";
                fdlg.InitialDirectory = @"";
                fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    dll.ImportTempTable(fdlg.FileName, label2, Properties.Settings.Default.cnn);
                    Insert_tempDislastik();
                    //RemoveRecords();
                    //InsertCheckTable();
                    //EXUCUTEQUERIES();
                    MessageBox.Show("Dosya yüklendi..", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.DoEvents();
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
        public void CallTablesFromDB()
        {
            for (int i = 0; i < lstView_Tablolar.Items.Count; i++)
            {
                TabPage tp = new TabPage(lstView_Tablolar.Items[i].Text);
                tabControl1.TabPages.Add(tp);

                DataGridView tb = new DataGridView();
                tb.Dock = DockStyle.Fill;
                tp.Controls.Add(tb);
                ExecuteSP(lstView_Tablolar.Items[i].Text);
                SelectQueryToDataGridView(lstView_Tablolar.Items[i].Text,tb);
            }
        }
        // ATTENTION : NEW SP, INCLUDE IN THERE... !!!
        public void ExecuteSP(string tb)
        {
            if(tb== "temp_DislastikBiasPisim")
            {
                dll.UpdateDeleteInsertFunctions(
                    " EXEC CHECKtemp_DislastikPisim @PartOfTable='temp_DislastikBiasPisim' "
                    , Properties.Settings.Default.cnn
                );
            }
            else if (tb == "temp_DislastikRadialPisim")
            {
                dll.UpdateDeleteInsertFunctions(
                    " EXEC CHECKtemp_DislastikPisim @PartOfTable='temp_DislastikRadialPisim' "
                    , Properties.Settings.Default.cnn
                );
            }
        }
        public void SelectQueryToDataGridView(string tb,DataGridView dg)
        {
            dll.QueryToDataGrid(dg, Properties.Settings.Default.cnn,
                "Select * from " + tb + ""
                );
            Application.EnableVisualStyles();
            dll.ReDesignDataGridViewCELLFormat(dg, Color.LightSteelBlue, Color.White, Color.DarkBlue, Color.LightBlue);
            //design options
            dg.ForeColor = Color.Black;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
            CallTablesFromDB();
        }
    }
}
