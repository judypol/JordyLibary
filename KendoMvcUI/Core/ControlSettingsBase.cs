using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using KendoMvcUI.Common;

namespace KendoMvcUI.Core
{
    /// <summary>
    /// 控件设置基类
    /// </summary>
    public class ControlSettingsBase
    {
        protected ControlSettingsBase(string kendoUIFunctionName)
        {
            this.KendoUIFunctionName = kendoUIFunctionName;
        }
        [PropertySetting(IsIngoreForJavaScript=true)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string StyleClass { get; set; }
        /// <summary>
        /// KendoUI 对应的方法名
        /// </summary>
        protected string KendoUIFunctionName { get; set; }
        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("<script>");
            //sb.AppendLine("$(function(){");
            //sb.AppendLine("$('#"+this.Id+"')."+this.KendoUIFunctionName+"({");
            //sb.AppendLine(this.ToJSSettings());
            //sb.AppendLine("});");
            //sb.AppendLine("});");
            //sb.AppendLine("</script>");
            return string.Empty;
        }
        /// <summary>
        /// 将设置类转换成js设置类格式，如：
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public virtual string ToJSSettings()
        {
            if (Id.IsNullOrWhiteSpace())
                throw new ArgumentNullException("id");

            Type type = this.GetType();
            string js = "";
            foreach (var property in type.GetProperties())        //获取所有的属性
            {
                string jsProperty = "";                           //js属性名
                string jsValue = "";                              //js属性值
                
                dynamic attrs = Attribute.GetCustomAttribute(property,typeof(PropertySettingAttribute));
                if (attrs != null)
                {
                    PropertySettingAttribute propertyName = (PropertySettingAttribute)attrs;
                    if (propertyName.IsIngoreForJavaScript)
                        continue;
                    jsProperty = propertyName.PropertyName.LowercaseFirst();
                }
                    
                if (jsProperty.IsNullOrWhiteSpace())
                    jsProperty = property.Name.LowercaseFirst();
                if (jsValue.IsNullOrWhiteSpace())
                    jsValue = property.GetValue(this, null) == null ? "" : property.GetValue(this, null).ToString();

                if (!jsValue.IsNullOrWhiteSpace())           //如果属性值不为空，则显示
                {
                    if(property.PropertyType==typeof(string)||property.PropertyType.BaseType==typeof(Enum))
                        js += jsProperty + ":'" + jsValue.LowercaseFirst() + "',";
                    else
                        js += jsProperty + ":" + jsValue.LowercaseFirst() + ",";
                }
                    
            }
            return js.TrimEnd(',');
        }
        protected string AddOptions(string script,string options)
        {
            if (!script.IsNullOrWhiteSpace())
                script += ",";
            return script += options;
        }
    }
}
