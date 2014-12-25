using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class ColorPickerSettings:SettingsBase
    {
        public ColorPickerSettings()
            :base("kendoColorPicker")
        { }
        public bool? Buttons { get; set; }
        public int? Columns { get; set; }
        public int? TileSize { get; set; }
        [PropertyName(IsIngoreForJavaScript=true)]
        public string Message { get; set; }
        public string Palette { get; set; }
        public string Value { get; set; }

        protected override string ToJSSettings()
        {
            string jsScript= base.ToJSSettings();
            if (!jsScript.EndsWith(","))
                jsScript += ",";
            jsScript += "message:{"+Message+"}";
            return jsScript;
        }
    }
}
