using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KendoMvcUI.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertySettingAttribute:Attribute
    {
        /// <summary>
        /// 是否在生成JS代码时忽略
        /// </summary>
        public bool IsIngoreForJavaScript { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
    }
    
}
