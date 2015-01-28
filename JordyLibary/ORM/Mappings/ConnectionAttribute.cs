using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.ORM.Mappings
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ConnectionAttribute : Attribute
    {
        public ConnectionAttribute(string connName)
        {
            ConnectionName = connName;
        }

        public string ConnectionName
        {
            get;
            set;
        }

        public static IConnectinContext GetContext(string connName)
        {
            return DBContext.GetConnection(connName);
        }

        public IConnectinContext GetContext()
        {
            return GetContext(ConnectionName);
        }
    }
}
