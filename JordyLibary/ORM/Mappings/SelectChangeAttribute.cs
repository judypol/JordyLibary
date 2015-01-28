using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.ORM.Mappings
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class SelectChangeAttribute : Attribute
    {
        public abstract void Change(Command cmd);
    }
}
