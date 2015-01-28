using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JordyCodeGenerator
{
    public class SqlServerDatabaseStructure:DatabaseStructure
    {
        public override void GetAllTable(string connName)
        {
            try
            {
                this.TableInfos = new List<TableInfo>();
                string sql = "select t.name tablename,s.name schemasName from sys.sysobjects t join sys.schemas s on t.uid=s.schema_id "+
                                "where t.xtype='U' order by s.name,t.name";
                adoHelper = new ADOHelper(connName);
                DataTable allTable = adoHelper.ExecuteDataTable(sql);
                foreach (DataRow row in allTable.Rows)
                {
                    string tableName = row[0].ToString();
                    string schemasName = row[1].ToString();
                    TableInfo ti = new TableInfo();
                    ti.TableName = tableName;
                    ti.SchemasTableName = tableName;
                    if (!schemasName.Equals("dbo"))
                        ti.SchemasTableName = schemasName + "." + tableName;

                    GetTableStructure(ti);
                    if (ti.ColumnInfoCollection == null || ti.ColumnInfoCollection.Count == 0)
                        continue;

                    TableInfos.Add(ti);
                }
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }
        protected override void GetTableStructure(TableInfo ti)
        {
            try
            {
                Dictionary<string,string> dbTypeMapSysType= DbTypeMapSystemType.DbTypeAndSystemType;
                string sql = "select c.name columnName,t.name dataType,c.is_identity from sys.columns c "+
                    "left join sys.types t on "+
                    "c.user_type_id=t.user_type_id where c.[object_id]=object_id('{0}') order by c.column_id";
                sql=string.Format(sql,ti.SchemasTableName);

                DataTable colTable = adoHelper.ExecuteDataTable(sql);
                foreach(DataRow row in colTable.Rows)
                {
                    string temColName = row[0].ToString();
                    ColumnInfo ci = new ColumnInfo();
                    ci.ColumnName = temColName.Replace(temColName[0].ToString(),
                        temColName[0].ToString().ToUpper());
                    string dataType = row[1].ToString();
                    if (dataType.Contains("("))
                        dataType = dataType.Substring(0, dataType.IndexOf('('));

                    if (dbTypeMapSysType.ContainsKey(dataType))
                        ci.ColumnType = dbTypeMapSysType[dataType];
                    else
                        ci.ColumnType = "string";

                    ci.LitteColumnName = temColName.Replace(temColName[0].ToString(),
                        temColName[0].ToString().ToLower());
                    ci.PrivateColumnName = "m" + temColName;
                    ci.ID = Convert.ToBoolean(row[2]);

                    ti.ColumnInfoCollection.Add(ci);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
