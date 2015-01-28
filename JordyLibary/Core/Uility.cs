using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.Core
{
    public class Uility
    {
        /// <summary>
        /// 将obj类型转换为指定的类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ChangeType<T>(object obj)
        {
            T t = default(T);
            if (obj == null || obj == DBNull.Value)
                return t;
            Type type = typeof(T);
            try
            {
                t = (T)ChangeType(obj, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return t;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ChangeType(object value,Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)&&(type==typeof(string))) return value.ToString();
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }
        /// <summary>
        /// 扩展方法，将字符串转换为Int类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //public static int ToInt(this string str)
        //{
        //    int res=int.MinValue;
        //    int.TryParse(str, out res);
        //    return res;
        //}
        ///// <summary>
        ///// 扩展方法，将字符串转换为double类型
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static double ToDouble(this string str)
        //{
        //    double res=double.MinValue;
        //    double.TryParse(str, out res);
        //    return res;
        //}
        ///// <summary>
        ///// 扩展方法，将字符串转换为float类型
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static float ToFloat(this string str)
        //{
        //    float res = float.MinValue;
        //    float.TryParse(str, out res);
        //    return res;
        //}
        ///// <summary>
        ///// 扩展方法，将字符串转换为Int类型
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static decimal ToDecimal(this string str)
        //{
        //    decimal res = decimal.MinValue;
        //    decimal.TryParse(str,out res);
        //    return res;
        //}
        ///// <summary>
        ///// 扩展方法，将字符串转换为Int类型
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static DateTime ToDateTime(this string str)
        //{
        //    DateTime res = DateTime.MinValue;
        //    DateTime.TryParse(str, out res); 
        //    return res;
        //}
    }
}
