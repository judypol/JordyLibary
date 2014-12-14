using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jordy.MvcExtense.Common;

namespace Jordy.MvcExtense.KendoUI
{
    public static class SettingsEx
    {
        /// <summary>
        /// 将设置类转换成js设置类格式，如：animation:false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJSSettings(Settings.SettingsBase t)
        {
            Type type = t.GetType();
            string js = ";";
            foreach(var property in type.GetProperties())       //获取所有的属性
            {
                string jsProperty="";                           //js属性名
                string jsValue="";                              //js属性值
                dynamic attrs = Attribute.GetCustomAttribute(property, typeof(PropertyNameAttribute));
                if (attrs != null && (!attrs.IsIngoreForJavaScript))
                    jsProperty = attrs.PropertyName.LowercaseFirst();

                if(jsProperty.IsNullOrWhiteSpace())
                    jsProperty=property.Name.LowercaseFirst();
                if(jsValue.IsNullOrWhiteSpace())
                    jsValue=property.GetValue(t, null)==null?"":property.GetValue(t,null).ToString();

                if(!jsValue.IsNullOrWhiteSpace())           //如果属性值不为空，则显示
                    js += jsProperty + ":" + jsValue.LowercaseFirst()+",";
            }
            return js;
        }
    }
}
