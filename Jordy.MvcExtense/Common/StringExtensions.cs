using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
