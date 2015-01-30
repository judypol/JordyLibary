using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KendoMvcUI.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 将首字符转换成小写字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string LowercaseFirst(this string value)
        {
            return char.ToLower(value[0]) + value.Substring(1);
        }
    }
}
