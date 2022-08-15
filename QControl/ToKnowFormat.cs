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
    public partial class ToKnowFormat : Form
    {
        public string formtag;
        FromDLL dll = new FromDLL();
        public ToKnowFormat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToKnowFormat tk=new ToKnowFormat();
            //string promptV = dll.InputDialog("File Name:", "FILE Property");
            string Dt = DateTime.Now.ToString("dd.MM.yyyy");
            string promptValue = formtag + " @"  + " " + Dt;
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.FileName = promptValue;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                dll.ExportExcel(fdlg.FileName, dg_Format);
                Application.DoEvents();
            }
        }
    }
}
