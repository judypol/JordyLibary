using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jordy.MvcExtense.Common;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class CalendarSettings:SettingsBase
    {
        public CalendarSettings()
            :base("kendoCalendar")
        {
            this.Depth = CalendarDepth.Month;
        }
        public string Culture { get; set; }
        /// <summary>
        /// 此属性没有实现
        /// </summary>
        [PropertyName(IsIngoreForJavaScript=true)]
        public DateTime[] Dates { get; set; }
        [PropertyName(IsIngoreForJavaScript=true)]
        public CalendarDepth? Depth { get; set; }
        /// <summary>
        /// <div id="calendar"></div>
        ///<script id="footer-template" type="text/x-kendo-template">
        ///Today - #: kendo.toString(data, "d") #
        ///</script>
        ///<script>
        /// $("#calendar").kendoCalendar({
        /// ooter: kendo.template($("#footer-template").html())
        ///});
        ///</script>
        /// </summary>
        public string Footer { get; set; }
        /// <summary>
        /// 格式化日期，如yyyy/MM/dd
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Date(default: Date(2099, 11, 31))
        /// </summary>
        [PropertyName(IsIngoreForJavaScript=true)]
        public DateTime? Max { get; set; }
        /// <summary>
        /// Date(default: Date(1900, 0, 1))
        /// </summary>
        [PropertyName(IsIngoreForJavaScript=true)]
        public DateTime? Min { get; set; }
        [PropertyName(IsIngoreForJavaScript=true)]
        public DateTime? Value { get; set; }
        protected override string ToJSSettings()
        {
            string script=base.ToJSSettings()+",";
            if(Depth.HasValue)
            {
                string depth = string.Format("start:'{0}',depth:'{1}'", Depth.Value.ToString().LowercaseFirst(),
                    Depth.Value.ToString().LowercaseFirst());
                if (!script.EndsWith(","))
                    script += ",";
                script += depth;
            }
            if(Max.HasValue)
            {
                string maxDate =string.Format("max:new Date({0},{1},{2})",
                    Max.Value.Year,Max.Value.Month-1,Max.Value.Day);
                if (!script.EndsWith(","))
                    script += ",";
                script += maxDate;
            }
            if(Min.HasValue)
            {
                string minDate = string.Format("min:new Date({0},{1},{2})",
                    Min.Value.Year, Min.Value.Month - 1, Min.Value.Day);
                if (!script.EndsWith(","))
                    script += ",";
                script += minDate;
            }
            if(Value.HasValue)
            {
                string ValueDate = string.Format("value:new Date({0},{1},{2})",
                    Value.Value.Year, Value.Value.Month - 1, Value.Value.Day);
                if (!script.EndsWith(","))
                    script += ",";
                script += ValueDate;
            }

            return script;
        }
        
    }
    /// <summary>
    /// 设置日期的显示
    /// </summary>
    public enum CalendarDepth
    {
        Month,      //shows the days of the month
        Year,       //shows the months of the year
        Decade,     //shows the years of the decade
        Century     //shows the decades from the century
    }
}
