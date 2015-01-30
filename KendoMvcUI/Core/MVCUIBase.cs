using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace KendoMvcUI.Core
{
    public class MVCUIBase
    {
        /// <summary>
        /// ViewContext
        /// </summary>
        protected virtual ViewContext ViewContext { get; private set; }
        /// <summary>
        /// ViewDataContainer
        /// </summary>
        protected virtual IViewDataContainer ViewDataContainer { get; private set; }
        /// <summary>
        /// 属性集合，如：class="",style=""
        /// </summary>
        protected virtual object[] Attributes{get;private set;}
        public ControlSettingsBase Settings{get;set;}
        public string RazorTemplate { get; set; }
        protected MVCUIBase(ControlSettingsBase settings, 
            ViewContext context, IViewDataContainer viewDataContainer)
            :this(settings)
        {
            ViewContext = context;
            ViewDataContainer = viewDataContainer;
        }
        public MVCUIBase(ControlSettingsBase settings)
        {
            this.Settings = settings;
        }
        /// <summary>
        /// 根据kendoJs 生成字符串
        /// </summary>
        /// <returns></returns>
        public virtual string RenderJavascript()
        {
            return Settings.ToJSSettings();
        }
        public override string ToString()
        {
            return RenderJavascript();
        }
    }
}
