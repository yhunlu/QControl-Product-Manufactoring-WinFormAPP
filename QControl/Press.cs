using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QControl
{
    public partial class Press : Form
    {
        public string txtpress;
        public string txtlocation;
        public string txtcift;
        FromDLL dll = new FromDLL();
        public Press()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }
        public void clean_datas()
        {
            foreach (Control x in this.groupBox1.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Text = String.Empty;
                }
                if (x is ComboBox)
                {
                    ((ComboBox)x).Text = String.Empty;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(" " + txt_press.Text + " presi eklemek istiyormusun ?", "Yeni Pres", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                     "Insert into cn_PressIDs "
                    + "(Press,Location,CiftKalip) "
                    + "VALUES "
                    + "( "
                    + "'" + txt_press.Text + "','" + cmb_location.Text + "',case when '" + cmb_cift.Text+ "'='EVET' then 1 else 0 end "
                    + ") "
                    , Properties.Settings.Default.cnn);

                MessageBox.Show("Pres '" + txt_press.Text + "' eklendi.");
                clean_datas();
                loadtable();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
        public void loadtable()
        {
            dll.QueryToDataGrid(dg_qcpress, Properties.Settings.Default.cnn, "Select Press as Pres,Location as 'Çalışma Yeri',case when CiftKalip=1 then 'EVET' else 'HAYIR' end as 'Çift Kalıp ?' from cn_PressIDs order by dbo.fn_CreateAlphanumericSortValue(Press)");
        }
        private void Press_Load(object sender, EventArgs e)
        {
            txt_press.MaxLength = 5;
            loadtable();
            dll.FillComboboxFromQueryCLASS(Properties.Settings.Default.cnn, "Select Location from st_Location order by Location", cmb_location, "Location");
        }

        private void cIKISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Press.ActiveForm.Close();
        }

        private void cmb_location_Click(object sender, EventArgs e)
        {
            cmb_location.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_location_DropDown(object sender, EventArgs e)
        {
            cmb_location.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_location_Enter(object sender, EventArgs e)
        {
            cmb_location.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_cift_Click(object sender, EventArgs e)
        {
            cmb_cift.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_cift_DropDown(object sender, EventArgs e)
        {
            cmb_cift.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmb_cift_Enter(object sender, EventArgs e)
        {
            cmb_cift.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void DataGridPress_ToText(DataGridView dg)
        {
            if (dg.SelectedCells.Count > 0)
            {
                int selectedrowindex = dg.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dg.Rows[selectedrowindex];

                txtpress = Convert.ToString(selectedRow.Cells["Pres"].Value);
                txtlocation = Convert.ToString(selectedRow.Cells["Çalışma Yeri"].Value);
                txtcift = Convert.ToString(selectedRow.Cells["Çift Kalıp ?"].Value);
            }
        }
        private void dg_qcpress_DoubleClick(object sender, EventArgs e)
        {
            if (dg_qcpress.RowCount > 0)
            {
                    DataGridPress_ToText(dg_qcpress);
                    txt_press.Text = txtpress;
                    cmb_location.Text = txtlocation;
                    cmb_cift.Text = txtcift;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(" " + txt_press.Text + " presi güncellemek istiyormusun ?", "Pres Güncelleme", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                     "Update cn_PressIDs set "
                    + "Location='" + cmb_location.Text + "',CiftKalip=case when '" + cmb_cift.Text + "'='EVET' then 1 else 0 end "
                    + "From cn_PressIDs c "
                    + "where c.Press='" + txt_press.Text + "' "
                    , Properties.Settings.Default.cnn);

                MessageBox.Show("Pres '" + txt_press.Text + "' Güncellendi.");
                clean_datas();
                loadtable();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(" " + txt_press.Text + " presi silmek istiyormusun ?", "Pres Silmek", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dll.UpdateDeleteInsertFunctions(
                      "delete c "
                    + "From cn_PressIDs c "
                    + "where c.Press='" + txt_press.Text + "' "
                    , Properties.Settings.Default.cnn);

                MessageBox.Show("Pres '" + txt_press.Text + "' Silindi.");
                clean_datas();
                loadtable();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}
