using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jordy.MvcExtense.Common;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class DatePickerSettings:SettingsBase
    {
        public DatePickerSettings()
            :base("kendoDatePicker")
        {
            Depth = CalendarDepth.Month;
        }
        /// <summary>
        /// Specifies a template used to populate value of the aria-label attribute.
        /// default: "Current focused date is #=kendo.toString(data.current, 'D')#"
        /// </summary>
        [PropertyName(IsIngoreForJavaScript=true)]
        public string ARIATemplate { get; set; }
        /// <summary>
        /// default: "en-US"
        /// </summary>
        public string Culture { get; set; }
        [PropertyName(IsIngoreForJavaScript=true)]
        public CalendarDepth? Depth { get; set; }
        /// <summary>
        /// The template which renders the footer of the calendar. If false, the footer will not be rendered.
        /// </summary>
        public string Footer { get; set; }
        /// <summary>
        /// Specifies the format, which is used to format the value of the DatePicker displayed
        /// in the input. The format also will be used to parse the input
        /// default is 'MM/dd/yyyy"
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Specifies the maximum date, which the calendar can show.
        /// deault is Date(2099, 11, 31)
        /// </summary>
        public DateTime? Max { get; set; }
        /// <summary>
        /// default: Date(1900, 0, 1)
        /// </summary>
        public DateTime? Min { get; set; }

        public DateTime? Value { get; set; }
        protected override string ToJSSettings()
        {
            string jsScript= base.ToJSSettings();
            if (!jsScript.EndsWith(","))
                jsScript += ",";
            if (!ARIATemplate.IsNullOrWhiteSpace())
                jsScript += "ARIATemplate:'"+ARIATemplate+"'";
            if (Depth.HasValue)
                jsScript = AddOptions(jsScript, string.Format("start:'{0}',depth:'{1}'", 
                    Depth.Value.ToString().LowercaseFirst(),
                    Depth.Value.ToString().LowercaseFirst()));
            if (Max.HasValue)
                jsScript = AddOptions(jsScript, string.Format("max:new Date({0},{1},{2})",
                    Max.Value.Year, Max.Value.Month - 1, Max.Value.Day));
            if (Min.HasValue)
                jsScript = AddOptions(jsScript, string.Format("min:new Date({0},{1},{2})",
                    Min.Value.Year, Min.Value.Month - 1, Min.Value.Day));
            if (Value.HasValue)
                jsScript = AddOptions(jsScript, string.Format("value:new Date({0},{1},{2})",
                    Value.Value.Year,Value.Value.Month-1,Value.Value.Day));

            return jsScript;
        }
    }
}
