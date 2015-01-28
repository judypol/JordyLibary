using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyCodeGenerator
{
    public class TableInfo
    {
        public TableInfo()
        {
            this.ColumnInfoCollection = new List<ColumnInfo>();
        }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表架构+“.”+表名，SQL数据库存在架构
        /// </summary>
        public string SchemasTableName { get; set; }
        public List<ColumnInfo> ColumnInfoCollection { get; set; }

    }

    public class ColumnInfo
    {
        /// <summary>
        /// 首字母大写
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 首字母小写
        /// </summary>
        public string LitteColumnName { get; set; }
        public string PrivateColumnName { get; set; }
        public string ColumnType { get; set; }
        public bool ID { get; set; }
    }
}
