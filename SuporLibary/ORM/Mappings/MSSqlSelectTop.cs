using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.ORM.Mappings
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class MSSqlSelectTop : SelectChangeAttribute
    {
        public MSSqlSelectTop(int max)
        {
            Max = max;
        }

        public int Max
        {
            get;
            set;
        }

        public override void Change(Command cmd)
        {
            string sql = string.Format("select top {1} * from ({0}) ", cmd.Text.ToString(), Max);
#if NET_4
            cmd.Text.Clear();
#else
            cmd.Text.Remove(0, cmd.Text.Length);
#endif

            cmd.Text.Append(sql);

        }
    }
}
