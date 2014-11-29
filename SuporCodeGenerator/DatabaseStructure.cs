using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace JordyCodeGenerator
{
    public abstract class DatabaseStructure
    {
        public virtual List<TableInfo> TableInfos {get;set;}
        public virtual ADOHelper adoHelper { get; internal set; }
        public virtual void GetAllTable(string connName)
        {
            
        }
        protected virtual void GetTableStructure(TableInfo ti)
        { 
        }
    }
}
