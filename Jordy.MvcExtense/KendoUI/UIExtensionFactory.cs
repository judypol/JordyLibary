using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Jordy.MvcExtense.KendoUI.Settings;
using Jordy.MvcExtense.KendoUI.UI;

namespace Jordy.MvcExtense.KendoUI
{
    /// <summary>
    /// HtmlHelper 帮助类
    /// </summary>
    public static class UIExtensionFactory
    {
        public static string AutoComplete(this HtmlHelper helper,Action<AutoCompleteSettings> action)
        {
            return UIEx.CreateUIExtension<AutoComplete, AutoCompleteSettings>(action).ToHtmlString();
        }
    }
    /// <summary>
    /// 生成UI对应的类
    /// </summary>
    public class UIEx
    {
        public static T CreateUIExtension<T, T1>(Action<T1> action)
            where T : MvcExtenseBase
            where T1 : SettingsBase
        {
            T1 local = Activator.CreateInstance<T1>();
            action(local);

            T t = (T)Activator.CreateInstance(typeof(T), local);
            return t;
        }
    }
}
