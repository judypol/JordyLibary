using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Jordy.MvcExtense.Common;
using System.Web.Mvc;


namespace Jordy.MvcExtense.KendoUI
{
    public class MvcExtenseBase
    {
        protected virtual ViewContext ViewContext { get; private set; }
        protected virtual IViewDataContainer ViewDataContainer { get; private set; }
        public SettingsBase Settings{get;set;}
        public string RazorTemplate { get; set; }
        protected MvcExtenseBase(SettingsBase settings,ViewContext context,IViewDataContainer viewDataContainer)
            :this(settings)
        {
            ViewContext = context;
            ViewDataContainer = viewDataContainer;
        }
        public MvcExtenseBase(SettingsBase settings)
        {
            this.Settings = settings;
        }
        /// <summary>
        /// 根据kendoJs 生成字符串，main,
        /// </summary>
        /// <returns></returns>
        public virtual string RenderJavascript()
        {
            return Settings.ToString();
        }
        public override string ToString()
        {
            return RenderJavascript();
        }
        /// <summary>
        /// 生成MvcHtmlString
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString ToHtmlString()
        {
            return MvcHtmlString.Create(ToString());
        }
    }
}
