using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BussinessDLL;

namespace QControl
{
    public partial class main : Form
    {
        private string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private string sql = null;
        private string uscon = "";
        private const int CP_NOCLOSE_BUTTON = 0x200;
        IkinciKaliteTT datt = null;
        Lastik_Uretim lu = null;
        Bitirme btr = null;
        DislastikProsesAnaliz dpa = null;
        PLCvsVerimlilik vsp = null;
        IKINCIKALITE ik = null;
        RaporEngel re = null;
        users us=null;
        DISLASTIK dl=null;
        DataIntegration dlv = null;
        TyreControl tc = null;
        EbatKg ek = null;
        Press ps = null;
        FromDLL dll = new FromDLL();
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public main()
        {
            InitializeComponent();
        }
        public void QControlPerAdmin()
        {
            try
            {
                cnn = new SqlConnection(Properties.Settings.Default.cnn);
                sql = " select username, fullname from zUsers tt "
                + " where tt.Admin=1 and tt.username='" + userName.Replace("OZKA\\", "").Replace("PC47448\\", "") + "'";

                cnn.Open();
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    uscon = dataReader[0].ToString();
                    if (uscon != "")
                    {
                        btn_users.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                        btnDataIntegration.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                        btn_ebatkg.ForeColor = System.Drawing.Color.FromArgb(254,214,2);
                        btn_pres.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                        btnlastikcontrol.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                        btn_engellirapor.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                        btn_ikincikalite.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                    }
                }
                if (uscon == "")
                {
                    btn_users.ForeColor = System.Drawing.Color.Red;
                    btn_ebatkg.ForeColor = System.Drawing.Color.Red;
                    btn_pres.ForeColor = System.Drawing.Color.Red;
                    btnlastikcontrol.ForeColor = System.Drawing.Color.Red;
                    btn_engellirapor.ForeColor = System.Drawing.Color.Red;
                }

                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch
            {

            }
        }
        public void QControlPerSpecialist()
        {
            try
            {
                cnn = new SqlConnection(Properties.Settings.Default.cnn);
                sql = " select username, fullname from zUsers tt "
                + " where tt.Specialist=1 and tt.username='" + userName.Replace("OZKA\\", "").Replace("PC47448\\", "") + "'";

                cnn.Open();
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    uscon = dataReader[0].ToString();
                    if (uscon != "")
                    {
                        btnDataIntegration.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                        btn_ikincikalite.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2);
                    }
                }
                if (uscon == "")
                {
                    btnDataIntegration.ForeColor = System.Drawing.Color.Red;
                    btn_ikincikalite.ForeColor = System.Drawing.Color.Red;
                }

                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch
            {

            }
        }

        private void main_Load(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            QControlPerSpecialist();
            QControlPerAdmin();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btn_users.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (us == null || us.Text == "")
                {
                    us = new users();
                    us.Show();
                }
                else if (dll.CheckOpened(us.Text))
                {
                    us.WindowState = FormWindowState.Normal;
                    us.Show();
                    us.Focus();
                }
            }
            else if (btn_users.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("Kalite Müdüründen izin alın !", "ERISIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDataIntegration_Click(object sender, EventArgs e)
        {
            if (btnDataIntegration.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (dl == null || dl.Text == "")
                {
                    dl = new DISLASTIK();
                    if (btn_ebatkg.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.kAYITISILToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    if (btn_pres.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.hATAPRESToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.vERIKONTROLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.sİLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.tabloyuTemizleToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    if (btn_engellirapor.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.button4.ForeColor = System.Drawing.Color.DarkRed; }
                    dl.Show();
                }
                else if (dll.CheckOpened(dl.Text))
                {
                    dl.WindowState = FormWindowState.Maximized;
                    dl.Show();
                    dl.Focus();
                }
            }
            else if (btnDataIntegration.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("Kalite Müdüründen izin alın !", "ERISIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ebatkg_Click(object sender, EventArgs e)
        {
            if (btn_ebatkg.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (ek == null || ek.Text == "")
                {
                    ek = new EbatKg();
                    dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                    " Select Distinct p.Code from cn_EbatKG p "
                    + " UNION "
                    + " Select Distinct p.Code from alp_EbatKG p "
                    + " UNION "
                    + " Select Distinct p.Code from lnup_EbatKG p "
                    + " order by p.Code "
                        , ek.cmb_code, "Code");
                    ek.eKLEToolStripMenuItem.Enabled = true;
                    ek.Show();
                }
                else if (dll.CheckOpened(ek.Text))
                {
                    ek.WindowState = FormWindowState.Normal;
                    dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                        "Select Distinct p.Code from cn_EbatKG p "
                        + " order by p.Code"
                        , ek.cmb_code, "Code");
                    ek.eKLEToolStripMenuItem.Enabled = true;
                    ek.Show();
                    ek.Focus();
                }
            }
            else if (btn_ebatkg.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("Kalite Müdüründen izin alın !", "ERISIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_pres_Click(object sender, EventArgs e)
        {
            if (btn_pres.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (ps == null || ps.Text == "")
                {
                    ps = new Press();
                    //dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                    //    "Select Distinct p.Code from cn_EbatKG p "
                    //    + " order by p.Code"
                    //    , ek.cmb_code, "Code");
                    //ek.eKLEToolStripMenuItem.Enabled = true;
                    ps.Show();
                }
                else if (dll.CheckOpened(ps.Text))
                {
                    ps.WindowState = FormWindowState.Normal;
                    ps.StartPosition = FormStartPosition.CenterScreen;
                    //dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn,
                    //    "Select Distinct p.Code from cn_EbatKG p "
                    //    + " order by p.Code"
                    //    , ek.cmb_code, "Code");
                    //ek.eKLEToolStripMenuItem.Enabled = true;
                    ps.Show();
                    ps.Focus();
                }
            }
            else if (btn_pres.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("Kalite Müdüründen izin alın !", "ERISIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (tc == null || tc.Text == "")
                {
                    tc = new TyreControl();
                    tc.Show();
                }
                else if (dll.CheckOpened(tc.Text))
                {
                    tc.WindowState = FormWindowState.Maximized;
                    tc.Show();
                    tc.Focus();
                }
            }
            else if (btnlastikcontrol.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("Kalite Müdüründen izin alın !", "ERISIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_engellirapor_Click(object sender, EventArgs e)
        {
            if (btn_engellirapor.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (re == null || re.Text == "")
                {
                    re = new RaporEngel();
                    re.Show();
                }
                else if (dll.CheckOpened(re.Text))
                {
                    re.WindowState = FormWindowState.Normal;
                    re.Show();
                    re.Focus();
                }
            }
            else if (btn_engellirapor.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("Kalite Müdüründen izin alın !", "ERISIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ikincikalite_Click(object sender, EventArgs e)
        {
            if (btn_ikincikalite.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2))
            {
                if (ik == null || ik.Text == "")
                {
                    ik = new IKINCIKALITE();
                    if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { ik.vERIKONTROLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { ik.btn_yukle.ForeColor = System.Drawing.Color.FromArgb(192, 255, 192); }
                    if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { ik.sİLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { ik.tabloyuTemizleToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    ik.Show();
                }
                else if (dll.CheckOpened(ik.Text))
                {
                    ik.WindowState = FormWindowState.Maximized;
                    ik.Show();
                    ik.Focus();
                }
            }
            else if (btn_ikincikalite.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("Kalite Müdüründen izin alın !", "ERISIM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_verimvspres_Click(object sender, EventArgs e)
        {
            if (vsp == null || vsp.Text == "")
            {
                vsp = new PLCvsVerimlilik();
                vsp.Show();
            }
            else if (dll.CheckOpened(vsp.Text))
            {
                vsp.WindowState = FormWindowState.Maximized;
                vsp.Show();
                vsp.Focus();
            }
        }

        private void btn_dislastikanaliz_Click(object sender, EventArgs e)
        {
            if (dpa == null || dpa.Text == "")
            {
                dpa = new DislastikProsesAnaliz();
                dpa.Show();
            }
            else if (dll.CheckOpened(dpa.Text))
            {
                dpa.WindowState = FormWindowState.Maximized;
                dpa.Show();
                dpa.Focus();
            }
        }

        private void btn_bitirme_Click(object sender, EventArgs e)
        {
            if (btr == null || btr.Text == "")
            {
                btr = new Bitirme();
                btr.Show();
            }
            else if (dll.CheckOpened(btr.Text))
            {
                btr.WindowState = FormWindowState.Maximized;
                btr.Show();
                btr.Focus();
            }
        }

        private void btn_lastikuretim_Click(object sender, EventArgs e)
        {
            if (lu == null || lu.Text == "")
            {
                lu = new Lastik_Uretim();
                lu.Show();
            }
            else if (dll.CheckOpened(lu.Text))
            {
                lu.WindowState = FormWindowState.Maximized;
                lu.Show();
                lu.Focus();
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (datt == null || datt.Text == "")
            {
                datt = new IkinciKaliteTT();
                datt.Show();
            }
            else if (dll.CheckOpened(datt.Text))
            {
                datt.WindowState = FormWindowState.Maximized;
                datt.Show();
                datt.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
                if (dlv == null || dlv.Text == "")
                {
                    dlv = new DataIntegration();
                    //if (btn_ebatkg.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.kAYITISILToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    //if (btn_pres.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.hATAPRESToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    //if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.vERIKONTROLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    //if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.sİLToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    //if (btnlastikcontrol.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.tabloyuTemizleToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(254, 214, 2); }
                    //if (btn_engellirapor.ForeColor == System.Drawing.Color.FromArgb(254, 214, 2)) { dl.button4.ForeColor = System.Drawing.Color.DarkRed; }
                    dlv.Show();
                }
                else if (dll.CheckOpened(dlv.Text))
                {
                    dlv.WindowState = FormWindowState.Maximized;
                    dlv.Show();
                    dlv.Focus();
                }
        }
    }
}
