using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class ComboBoxSettings:BindingSettingsBase
    {
        public ComboBoxSettings()
            :base("kendoComboBox")
        { }
        public bool? AutoBind { get; set; }
        /// <summary>
        /// the parent id
        /// </summary>
        public string CascadeFrom { get; set; }
        /// <summary>
        /// Defines the field to be used to filter the data source. 
        /// If not defined the parent's dataValueField option will be used. 
        /// </summary>
        public string CascadeFromField { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public bool? Enable { get; set; }
        /// <summary>
        /// startswith, endswith and contains,the default is none
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? HighlightFirst { get; set; }
        /// <summary>
        /// If set to false case-sensitive search will be 
        /// performed to find suggestions. The widget performs case-insensitive searching by default.
        /// </summary>
        public bool? IgnoreCase { get; set; }
        /// <summary>
        /// The index of the initially selected item. The index is 0 based,
        /// default is -1
        /// </summary>
        public int? Index { get; set; }
        /// <summary>
        /// The minimum number of characters the user must type before a 
        /// search is performed. Set to higher value than 1 if the search could match a lot of items.
        /// </summary>
        public int? MinLength { get; set; }
        public string Placeholder { get; set; }
        /// <summary>
        /// If set to true the widget will automatically use the first suggestion as its value.
        /// </summary>
        public bool? Suggest { get; set; }
        /// <summary>
        /// Specifies a static HTML content, which will be rendered as a header of the popup element.
        /// </summary>
        public string HeaderTemplate { get; set; }
        /// <summary>
        /// The template used to render the items. By default the 
        /// widget displays only the text of the data item (configured via dataTextField).
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// The text of the widget used when the autoBind is set to false.
        /// default is ""
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The value of the widget.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Specifies the value binding behavior for the widget when the initial model 
        /// value is null. If set to true, the View-Model field will be 
        /// updated with the selected item value field. If set to false, 
        /// the View-Model field will be updated with the selected item.
        /// </summary>
        public bool ValuePrimitive { get; set; }
    }
}
