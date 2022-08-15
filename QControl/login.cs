using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using BussinessDLL;
using System.Diagnostics;
using System.Reflection;

namespace QControl
{
    public partial class login : Form
    {
        public string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public Timer tmr;
        public int b = 0;
        public int chck = 0;
        public SqlConnection cnn;
        public SqlCommand command;
        public SqlDataReader dataReader;
        public string sql = null;
        public string uscon="";
        BussinessLayer pr = new BussinessLayer();
        public login()
        {
            InitializeComponent();
        }
        private void login_Load(object sender, EventArgs e)
        {
            chck = 1;
            //StartLoginProcess();
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            label1.Text = version;
        }
        private void StartLoginProcess()
        {
            //label2.Text = userName.Replace("OZKA\\", "").Replace("PC47448\\", "");
            //chck = 1;
            //label2.ForeColor = System.Drawing.Color.White;
            //label3.Text = "                              HOŞGELDİN " + label2.Text;
            //label3.ForeColor = System.Drawing.Color.White;



            //cnn = new SqlConnection(QControl.Properties.Settings.Default.cnn);
            //sql = " select username, fullname from zUsers tt "
            //+ " where tt.Active=1 and tt.username='" + label2.Text + "'";

            //cnn.Open();
            //command = new SqlCommand(sql, cnn);
            //dataReader = command.ExecuteReader();

            //while (dataReader.Read())
            //{
            //    uscon = dataReader[0].ToString();
            //    if (uscon != "")
            //    {

            //    }
            //}
            //if (uscon == "")
            //{
            //    label2.ForeColor = System.Drawing.Color.Red;
            //    label3.Text = "                                    ERİŞİM ENGELLENDİ !!!";
            //    label3.ForeColor = System.Drawing.Color.Red;
            //}

            //dataReader.Close();
            //command.Dispose();
            //cnn.Close();
        }
        private void login_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            Application.EnableVisualStyles();
            tmr = new Timer();
            //set time interval 3 sec
            tmr.Interval = 1000;
            //starts the timer
            tmr.Start();
            tmr.Tick += tmr_Tick;
            pBar2.Maximum = 100000;
            pBar2.Step = 1;
            for (int j = 0; j < 100000; j++)
            {
                Caluculate(j);
                pBar2.PerformStep();
            }
        }
        private void Caluculate(int i)
        {
            double pow = Math.Pow(i, i);
        }
        void tmr_Tick(object sender, EventArgs e)
        {
            //after 3 sec stop the timer
            tmr.Stop();
            //display mainform
            if (chck == 1)
            {
                main mf = new main();
                mf.Show();
                //hide this form
                this.Hide();
            }
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            //exit application when form is closed
           
            Application.Exit();
        }
    }
}
