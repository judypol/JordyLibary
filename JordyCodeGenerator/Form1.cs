using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace JordyCodeGenerator
{
    public partial class CodeGeneratorFrm : Form
    {
        public CodeGeneratorFrm()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            string privderName = ((ItemValue)cbb_conStr.SelectedItem).ProviderName;
            List<TableInfo> tableInfos=null;
            if (privderName.ToLower().Contains("sqlite"))
                tableInfos=ExecuteSqliteTable();
            else if (privderName.ToLower().Contains("sqlclient"))
                tableInfos=ExecuteSqlTable();

            SaveCodeFile(tableInfos);
            MessageBox.Show("代码生成成功！");
        }

        private void CodeGeneratorFrm_Load(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection collection = ConfigurationManager.ConnectionStrings;
            //collection.RemoveAt(0);
            foreach(ConnectionStringSettings section in collection)
            {
                ItemValue iv = new ItemValue();
                iv.ConnectionName = section.Name;
                iv.ProviderName = section.ProviderName;
                cbb_conStr.Items.Add(iv);
            }
        }
        private List<TableInfo> ExecuteSqliteTable()
        {
            try
            {
                string connName = cbb_conStr.Text;
                SqliteDataStructure sds = new SqliteDataStructure();
                sds.GetAllTable(connName);
                //sds.GetStructure(allTables);
                return sds.TableInfos;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private List<TableInfo> ExecuteSqlTable()
        {
            try
            {
                string connName = cbb_conStr.Text;
                SqlServerDatabaseStructure sds = new SqlServerDatabaseStructure();
                sds.GetAllTable(connName);
                return sds.TableInfos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private void SaveCodeFile(List<TableInfo> tableInfos)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog()==DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                if (!path.EndsWith("\\"))
                    path += "\\";
                string templateFile = Properties.Resources.Template;
                foreach (TableInfo ti in tableInfos)
                {
                    RazorTemplate rt = new RazorTemplate(templateFile,true);
                    rt.Parse(ti);
                    string fileName = path + ti.TableName + ".cs";
                    rt.SaveFile(fileName);
                }
            }
        }

        public class ItemValue
        {
            public string ConnectionName { get; set; }
            public string ProviderName { get; set; }
        }
    }
}
