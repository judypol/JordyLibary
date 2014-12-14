using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNameAttribute:Attribute
    {
        /// <summary>
        /// 是否在生成JS代码时忽视
        /// </summary>
        public bool IsIngoreForJavaScript { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
    }
    
}
