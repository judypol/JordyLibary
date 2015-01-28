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
        /// <summary>
        /// Html在Razor中调用的总入口
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static UIEx UI(this HtmlHelper helper)
        {
            return new UIEx(helper.ViewContext, helper.ViewDataContainer);
        }
    }
    /// <summary>
    /// 生成UI对应的类
    /// </summary>
    public class UIEx
    {
        /// <summary>
        /// 根据类型生成对应的Javascript
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public T CreateUIExtension<T, T1>(Action<T1> action)
            where T : MvcExtenseBase
            where T1 : SettingsBase
        {
            T1 local = Activator.CreateInstance<T1>();
            action(local);

            T t = (T)Activator.CreateInstance(typeof(T), local);
            return t;
        }
        private T CreateUIExtension<T,T1>(Action<T1> action,
            ViewContext context,IViewDataContainer viewDataContainer)
            where T:MvcExtenseBase
            where T1:SettingsBase
        {
            try
            {
                T1 local = Activator.CreateInstance<T1>();
                action(local);

                T t = (T)Activator.CreateInstance(typeof(T), local, context, viewDataContainer);
                return t;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private ViewContext _context;
        private IViewDataContainer _viewDataContainer;
        public UIEx(ViewContext context,IViewDataContainer viewDataContainer)
        {
            _context = context;
            _viewDataContainer = viewDataContainer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MvcHtmlString AutoComplete(Action<AutoCompleteSettings> action)
        {
            return CreateUIExtension<AutoComplete, AutoCompleteSettings>(action).ToHtmlString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public MvcHtmlString Button(Action<ButtonSettings> action)
        {
            return CreateUIExtension<Button, ButtonSettings>(action).ToHtmlString();
        }
        public MvcHtmlString Calendar(Action<CalendarSettings> action)
        {
            return CreateUIExtension<Calendar, CalendarSettings>(action).ToHtmlString();
        }
        public MvcHtmlString ColorPalette(Action<ColorPaletteSettings> action)
        {
            return CreateUIExtension<ColorPalette, ColorPaletteSettings>(action).ToHtmlString();
        }
        public MvcHtmlString ColorPicker(Action<ColorPickerSettings> action)
        {
            return CreateUIExtension<ColorPicker, ColorPickerSettings>(action).ToHtmlString();
        }
        public MvcHtmlString ComboBox(Action<ComboBoxSettings> action)
        {
            return CreateUIExtension<ComboBox, ComboBoxSettings>(action).ToHtmlString();
        }
        public MvcHtmlString ContextMenu(Action<ContextMenuSettings> action)
        {
            return CreateUIExtension<ContextMenu, ContextMenuSettings>(action).ToHtmlString();
        }
        public MvcHtmlString DatePicker(Action<DatePickerSettings> action)
        {
            return CreateUIExtension<DatePicker, DatePickerSettings>(action).ToHtmlString();
        }
        //public MvcHtmlString DateTimePicker(Action<DateTimePickerSettings> action)
        //{
        //    return CreateUIExtension<DateTimePicker, DateTimePickerSettings>(action).ToHtmlString();
        //}
        //public MvcHtmlString DropdownList(Action<DropdownListSettings> action)
        //{
        //    return CreateUIExtension<DropdownList, DropdownListSettings>(action).ToHtmlString();
        //}
    }
}
