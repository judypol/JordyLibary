using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.ORM.Mappings
{
    public class TableAttribute : Attribute
    {
        public TableAttribute()
        {
            DISTINCT = false;
            DefaultConnection = DBContext.CurrentConnectionName;
        }
        public TableAttribute(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
        public bool DISTINCT
        {
            get;
            set;
        }
        public string DefaultConnection
        {
            get;
            set;
        }
    }
}
