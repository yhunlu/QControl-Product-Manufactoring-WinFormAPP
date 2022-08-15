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
    public partial class users : Form
    {
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private string sql = null;
        private int cnt;
        FromDLL dll = new FromDLL();
        public users()
        {
            InitializeComponent();
        }
        public void QueryToListBox()
        {
            cnn = new SqlConnection(Properties.Settings.Default.cnn);
            sql = "Select username from zUsers order by Username";

            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            listBox1.Items.Clear();
            cnt = 0;
            while (dataReader.Read())
            {
                cnt = cnt + 1;

                listBox1.Items.Add(dataReader[0].ToString());
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            label3.Text = cnt.ToString();
        }
        private void users_Load(object sender, EventArgs e)
        {
            QueryToListBox();
        }
        public void clean_datas()
        {
            //Clean TextBox
            foreach (Control x in this.groupBox1.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Text = String.Empty;
                }
            }
            foreach (Control x in this.groupBox3.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Text = String.Empty;
                }
            }
            //Clean CheckBox
            foreach (Control x in this.groupBox2.Controls)
            {
                if (x is CheckBox)
                {
                    ((CheckBox)x).Checked = false;
                }
            }
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            clean_datas();
            dll.QueryToTextBox("Select username from zUsers where username='" + listBox1.SelectedItem.ToString() + "'", QControl.Properties.Settings.Default.cnn, textBox1);
            dll.QueryToTextBox("Select fullname from zUsers where username='" + listBox1.SelectedItem.ToString() + "'", QControl.Properties.Settings.Default.cnn, textBox3);
            dll.QueryToTextBox("Select TEMP from zUserTEMP where [USER]='" + listBox1.SelectedItem.ToString() + "'", QControl.Properties.Settings.Default.cnn, txt_temp);

            dll.QueryToCheckBox("Select cast(Admin as real) as Admin from zUsers where username='" + listBox1.SelectedItem.ToString() + "'", QControl.Properties.Settings.Default.cnn, checkBox1);
            dll.QueryToCheckBox("Select cast(Specialist as real) as Specialist from zUsers where username='" + listBox1.SelectedItem.ToString() + "'", QControl.Properties.Settings.Default.cnn, checkBox4);
            dll.QueryToCheckBox("Select cast(Standard as real) as Standard from zUsers where username='" + listBox1.SelectedItem.ToString() + "'", QControl.Properties.Settings.Default.cnn, checkBox2);
            dll.QueryToCheckBox("Select cast(Active as real) as Active from zUsers where username='" + listBox1.SelectedItem.ToString() + "'", QControl.Properties.Settings.Default.cnn, checkBox3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Would You like to Add this User " + textBox1.Text + " ?", "New User", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                    "IF NOT EXISTS(Select username from zUsers where username='" + textBox1.Text + "') "
                    + "BEGIN "
                    + "Insert into zUsers "
                    + "(username,fullname,Admin,Specialist,Standard,Active) "
                    + "VALUES "
                    + "( "
                    + "'" + textBox1.Text + "','" + textBox3.Text + "','" + checkBox1.Checked + "','" + checkBox4.Checked + "','" + checkBox2.Checked + "','" + checkBox3.Checked + "' "
                    + ") "
                    + "END;"
                    , Properties.Settings.Default.cnn);

                dll.UpdateDeleteInsertFunctions(
                    "IF NOT EXISTS(Select [USER] from zUserTEMP where [USER]='" + textBox1.Text + "') "
                    + "BEGIN "
                    + "Insert into zUserTEMP "
                    + "([USER],TEMP) "
                    + "VALUES "
                    + "( "
                    + "'" + textBox1.Text + "','" + txt_temp.Text + "' "
                    + ") "
                    + "END;"
                    , Properties.Settings.Default.cnn);

                MessageBox.Show("User is '" + textBox1.Text + "' has been added in Database.");
                clean_datas();
                QueryToListBox();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Would You like to Modify this User " + textBox1.Text + " ?", "Update User", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions("Update zUsers set "
                    + "fullname = '" + textBox3.Text + "', "
                    + "Admin = '" + checkBox1.Checked + "', "
                    + "Specialist = '" + checkBox4.Checked + "', "
                    + "Standard = '" + checkBox2.Checked + "', "
                    + "Active = '" + checkBox3.Checked + "' "
                    + "From zUsers where username=RTRIM(LTRIM('" + textBox1.Text + "'))", Properties.Settings.Default.cnn);

                dll.UpdateDeleteInsertFunctions("Update zUserTEMP set "
                    + "TEMP = '" + txt_temp.Text + "' "
                    + "From zUserTEMP where [USER]=RTRIM(LTRIM('" + textBox1.Text + "'))", Properties.Settings.Default.cnn);
                MessageBox.Show("User is '" + textBox1.Text + "' has been modified in Database.");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Would You like to delete this User " + textBox1.Text + " ?", "Delete User", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions("delete z from zUsers z where RTRIM(LTRIM(username))='" + textBox1.Text + "'", Properties.Settings.Default.cnn);
                dll.UpdateDeleteInsertFunctions("delete z from zUserTEMP z where [USER]='" + textBox1.Text + "'", Properties.Settings.Default.cnn);
                MessageBox.Show("User is '" + textBox1.Text + "' has been removed from Database.");
                QueryToListBox();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}
