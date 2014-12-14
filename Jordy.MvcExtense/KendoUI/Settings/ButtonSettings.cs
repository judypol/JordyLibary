using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class ButtonSettings:SettingsBase
    {
        public ButtonSettings()
            :base("kendoButton")
        { }
        public string Icon { get; set; }
        public string ImageUrl { get; set; }
        public string SpriteCssClass { get; set; }

        public string Click { get; set; }
    }
}
