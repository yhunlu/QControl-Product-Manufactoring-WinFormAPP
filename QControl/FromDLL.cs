using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Windows.Forms;
using BussinessDLL;
using System.Drawing;
using Microsoft.Reporting.WinForms;

namespace QControl
{
    class FromDLL
    {
        BussinessLayer pr = new BussinessLayer();
        TreeViewSerializer serializer = new TreeViewSerializer();
        private string lxmlElement;
        private string lxmlAttribute;
        private string lxmlString;

        #region XML Class Properties

        public string xmlElement
        {
            get
            {
                return lxmlElement;
            }
            set
            {
                lxmlElement = value;
            }
        }

        public string xmlAttribute
        {
            get
            {
                return lxmlAttribute;
            }
            set
            {
                lxmlAttribute = value;
            }
        }


        public string xmlString
        {
            get
            {
                return lxmlString;
            }
            set
            {
                lxmlString = value;
            }
        }


        #endregion

        public void LoadXmlFileToTreeview(TreeView treeView, string fileName)
        {
            serializer.DeserializeTreeView(treeView, fileName);
        }
        public void SqlToNumericUpDown(string sqlscript, string connectionString, NumericUpDown tx)
        {
            pr.QueryToNumericUpDown(sqlscript, connectionString, tx);
        }
        public void TREEVIEW_ClearBackColor(TreeView tv)
        {
            pr.TREEVIEW_ClearBackColor(tv);
        }
        public void TREEVIEW_UnSelectedNodes(TreeView tv)
        {
            pr.TREEVIEW_UnSelected(tv);
        }
        #region Query To DataGridView
        public void QueryToDataGrid(DataGridView dg, string connection, string query)
        {
            DataSet ds = new DataSet();
            ds = GetSelectQueryWin(connection, query);

            dg.DataSource = ds.Tables[0];
        }
        public void FillComboboxFromQueryCLASS(string connectionString, string Query, ComboBox combo, string ColumName)
        {
            pr.FillComboboxFromQuery(connectionString, Query, combo, ColumName);
        }
        public TreeNode TreeViewFindRoot(TreeNode tn)
        {
            return pr.FindRootNode(tn);
        }
        private DataSet GetSelectQueryWin(string connectionString, string AllQuery)
        {
            return pr.GetSelectQueryDB(connectionString, AllQuery);
        }

        #endregion

        public void ReDesignDataGridViewCELLFormat(DataGridView dg
                                               , Color Rows_DefaultCellStyle
                                               , Color AlternatingRows_DefaultCellStyle
                                               , Color DefaultCellStyle_SelectionBack
                                               , Color DefaultCellStyle_SelectionFore
                                              )
        {
            pr.ReDesignDataGridViewFormat(dg, Rows_DefaultCellStyle, AlternatingRows_DefaultCellStyle, DefaultCellStyle_SelectionBack, DefaultCellStyle_SelectionFore);
        }
        public void ActivateForm(Form fm)
        {
            pr.ActivateForm2(fm);
        }
        public bool CheckOpened(string name)
        {
            return pr.CheckOpened(name);
        }
        public void UserDefineToTempTable(string connectionString, Label lb)
        {
            pr.UserDefineTempTable(connectionString, lb);
        }
        public void ExportExcel(string file, DataGridView dg)
        {
            pr.ExportExcel(file, dg);
        }
        public void ImportTempTable(string filename, Label lb, string connectionString)
        {
            pr.ImportTempTable(filename, lb, connectionString);
        }
        public void ImportTempTableNoLabel(string filename, string lb, string connectionString)
        {
            pr.ImportTempTableNoLabel(filename, lb, connectionString);
        }
        public void SendEmail(string attachmentfile, string issue, string context, string Towho, string ToCC)
        {
            pr.SendEmail(attachmentfile, issue, context, Towho, ToCC);
        }
        public void UpdateDeleteInsertFunctions(string sqlscript, string connectionString)
        {
            pr.ExecuteNonQueryMethod(sqlscript, connectionString);
        }
        public void QueryToCheckBox(string sqlscript, string connectionString, CheckBox tx)
        {
            pr.QueryToCheckBox(sqlscript, connectionString, tx);
        }
        public void QueryToComboTEXT(string sqlscript, string connectionString, ComboBox tx)
        {
            pr.QueryToComboBoxTEXT(sqlscript, connectionString, tx);
        }
        public void QueryToTextBox(string sqlscript, string connectionString, TextBox tx)
        {
            pr.QueryToTextBox(sqlscript, connectionString, tx);
        }
        public void QueryToManyTextBox(string sqlscript, string connectionString, TextBox tx, Int16 ColumnIndex)
        {
            pr.QueryToManyTextBox(sqlscript, connectionString, tx, ColumnIndex);
        }
        public void QueryToLabel(string sqlscript, string connectionString, Label lb)
        {
            pr.QueryToLabel(sqlscript, connectionString, lb);
        }
        public String InputDialog(string text, string caption)
        {
            return pr.ShowInputDialog(text, caption);
        }
        public void DataGridViewToInsertDatabaseTable(string ConnString, DataGridView dg, string TableName)
        {
            pr.DataGridToInsertDatabaseTable(ConnString, dg, TableName);
        }
        public void ListboxToListViewDragDropANDlogListbox(ListBox lst, ListBox list_log, ListView lv, ListView lv1, ListView lv2, string comm, ListView lvtarget)
        {
            pr.ListboxToListViewDragDropANDlogListbox(lst, list_log, lv,lv1,lv2, comm, lvtarget);
        }
        public void DataGridViewToInsertDBTable_RowIndex(string ConnString, DataGridView dg, string TableName)
        {
            pr.DataGridToInsertDBTable_RowIndex(ConnString, dg, TableName);
        }
    }
}
