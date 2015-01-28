using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JordyCodeGenerator
{
    public class SqliteDataStructure:DatabaseStructure
    {
        public override void GetAllTable(string connName)
        {
            try
            {
                this.TableInfos = new List<TableInfo>();
                string sql = "Select name from sqlite_master where type='table'";
                adoHelper = new ADOHelper(connName);
                DataTable allTable = adoHelper.ExecuteDataTable(sql);
                foreach (DataRow row in allTable.Rows)
                {
                    string tableName=row[0].ToString();
                    TableInfo ti = new TableInfo();
                    ti.TableName = tableName;
                    ti.SchemasTableName = tableName;
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
                
                string sql = "select sql from sqlite_master where name='{0}'";
                sql = string.Format(sql, ti.TableName);
                string sqliteSql = adoHelper.ExecuteScalar<string>(sql);
                int leftIndex = sqliteSql.IndexOf("(")+1;
                int rightIndex = sqliteSql.LastIndexOf(")");
                string columnsStr = sqliteSql.Substring(leftIndex , 
                    rightIndex - leftIndex);
                string[] columns = columnsStr.Split(',');

                foreach (string item in columns)
                {
                    if (item.Contains("FOREIGN KEY"))      //如果是外键则返回
                        continue;
                    string[] temStrs = item.Trim().Split(' ');
                    if (temStrs.Length < 2 )
                    {
                        continue;
                    }

                    string temColName = temStrs[0].TrimEnd(']').TrimStart('[');
                    ColumnInfo ci = new ColumnInfo();
                    ci.ColumnName = temColName.Replace(temColName[0].ToString(),
                        temColName[0].ToString().ToUpper());
                    string dataType=temStrs[1];
                    if (dataType.Contains("("))
                        dataType = dataType.Substring(0, dataType.IndexOf('('));
                    if (dbTypeMapSysType.ContainsKey(dataType))
                        ci.ColumnType = dbTypeMapSysType[dataType];
                    else
                        ci.ColumnType = "string";

                    ci.LitteColumnName = temColName.Replace(temColName[0].ToString(),
                        temColName[0].ToString().ToLower());
                    ci.PrivateColumnName = "m" + temColName;
                    ci.ID = temStrs.Contains("PRIMARY");

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
