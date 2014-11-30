using Jordy.MvcExtense.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Jordy.MvcExtense.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}
