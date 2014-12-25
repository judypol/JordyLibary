using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class ColorPaletteSettings:SettingsBase
    {
        public ColorPaletteSettings()
            :base("kendoColorPalette")
        { }

        public string Palette { get; set; }
        public int? Columns { get; set; }
        public int? TileSize { get; set; }
        public string Value { get; set; }
        protected override string ToJSSettings()
        {
            return base.ToJSSettings();
        }
    }
}
