using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JordyLibary.ORM
{
    /// <summary>
    /// 实体状态描述接口
    /// </summary>
    public interface IEntityState
    {
        bool _Loaded
        {
            get;
            set;
        }
        Dictionary<string, FieldState> _FieldState
        {
            get;
        }
        void FieldChange(string field);
        void LoadData(IDataReader reader);
        string ConnectionName
        {
            get;
            set;
        }
    }
}
