using Jordy.MvcExtense.KendoUI.Settings;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Jordy.MvcExtense.KendoUI
{
    public class MvcExtenseBase:IHtmlString
    {
        public SettingsBase Settings{get;set;}
        public MvcExtenseBase()
        { }
        public MvcExtenseBase(SettingsBase settings)
        {
            this.Settings = settings;
        }
        public string RenderJavascript()
        {
            return Razor.Parse<SettingsBase>("", Settings);
        }
        public override string ToString()
        {
            return RenderJavascript();
        }
        public string ToHtmlString()
        {
            return ToString();
        }
    }
}
