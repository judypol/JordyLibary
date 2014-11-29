using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JordyLibary.ORM
{
    /// <summary>
    /// SQL转换器
    /// </summary>
    public interface ISQLBuilder
    {
        string ReplaceSql(string sql);

        void SetProcParameter(IDataParameter dp, string name, object value, ParameterDirection direction);


        void SetParameter(IDataParameter dp, string name, object value, ParameterDirection direction);
    }
}
