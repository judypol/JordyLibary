using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Jordy.MvcExtense.Enums;
using Jordy.MvcExtense.Common;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class AutoCompleteSettings:BindingSettingsBase
    {
        public AutoCompleteSettings()
            : base("kendoAutoComplete")
        {   //设置属性的默认值
            this.Filter = FilterString.StartsWith;
            Placeholder = "请输入值...";
        }
        #region 属性
        [PropertyName(IsIngoreForJavaScript = true)]
        public string TextFieldName { get; set; }
        public bool? Enable { get; set; }
        public FilterString? Filter { get; set; }
        /// <summary>
        /// If set to true the first suggestion will be automatically highlighted.
        /// </summary>
        public bool? HighlightFirst { get; set; }
        /// <summary>
        /// If set to false case-sensitive search will be 
        /// performed to find suggestions. The widget performs case-insensitive searching by default
        /// </summary>
        public bool? IgnoreCase { get; set; }
        /// <summary>
        /// The minimum number of characters the user must type 
        /// before a search is performed. Set to higher value than 1 
        /// if the search could match a lot of items.
        /// </summary>
        public int? MinLength { get; set; }
        public string Placeholder { get; set; }
        public string Separator { get; set; }
        /// <summary>
        /// If set to true the widget will automatically use the first suggestion as its value.
        /// </summary>
        public bool? Suggest { get; set; }
        /// <summary>
        /// The template used to render the suggestions. By default the widget displays 
        /// only the text of the suggestion (configured via dataTextField).
        /// eg:'<span><img src="/img/#: id #.png" alt="#: name #" />#: name #</span>'
        /// </summary>
        public string Template { get; set; }
        #endregion
    }
}
