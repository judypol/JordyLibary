using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyCodeGenerator
{
    public class DbTypeMapSystemType
    {
        public static Dictionary<string,string> DbTypeAndSystemType
        {
            get { return GetAllTypeMapping(); }
        }
        private static Dictionary<string,string> GetAllTypeMapping()
        {
            Dictionary<string, string> mapDic = new Dictionary<string, string>();
            mapDic.Add("bigint","Int64?");
            mapDic.Add("binary","byte[]");
            mapDic.Add("bit", "bool?");
            mapDic.Add("bool","bool?");
            mapDic.Add("boolean", "boolean?");
            mapDic.Add("char", "string");
            mapDic.Add("date", "DateTime?");
            mapDic.Add("datetime","DateTime?");
            mapDic.Add("decimal","decimal?");
            mapDic.Add("double","double?");
            mapDic.Add("float","float?");
            mapDic.Add("guid", "Guid");
            mapDic.Add("image", "byte[]");
            mapDic.Add("int", "int?");
            mapDic.Add("integer","int?");
            mapDic.Add("long", "long?");
            mapDic.Add("money", "byte[]");
            mapDic.Add("nchar", "string");
            mapDic.Add("note", "string");
            mapDic.Add("ntext", "string");
            mapDic.Add("numeric", "int?");
            mapDic.Add("nvarchar", "string");
            mapDic.Add("samlldate", "DateTime?");
            mapDic.Add("string", "string");
            mapDic.Add("text", "string");
            mapDic.Add("timestamp", "byte[]");
            mapDic.Add("time", "DateTime?");
            mapDic.Add("uniqueidentifier", "Guid");
            mapDic.Add("tinyint", "int?");
            return mapDic;
        }
    }
}
