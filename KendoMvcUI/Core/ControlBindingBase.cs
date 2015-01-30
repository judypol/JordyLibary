using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KendoMvcUI.Common;

namespace KendoMvcUI.Core
{
    /// <summary>
    /// 需要绑定数据的控件，必须继承此类
    /// </summary>
    public class ControlBindingBase:ControlSettingsBase
    {
        public ControlBindingBase(string kendoUIFunctionName)
            : base(kendoUIFunctionName)
        {
        }
        private string _url = null;
        /// <summary>
        /// 通过URL绑定数据，URL与DataSource属性只能设置一个
        /// </summary>
        [PropertySetting(IsIngoreForJavaScript=true)]
        public string URL { 
            get {
                    return _url;
                }
            set {
                if (_dataSource!=null)
                    throw new Exception("你已经设置了DataSource属性，URL与DataSource属性只能设置一个");
                _url = value;
            }
        }
        private IEnumerable _dataSource=null;
        /// <summary>
        /// 通过DataSource绑定数据，URL与DataSource属性只能设置一个
        /// </summary>
        [PropertySetting(IsIngoreForJavaScript = true)]
        public IEnumerable DataSource { 
            get{
                    return _dataSource;
               }
            set {
            if (!_url.IsNullOrWhiteSpace())
                throw new Exception("你已经设置了URL属性，URL与DataSource属性只能设置一个");
            _dataSource = value;
            } 
        }
        /// <summary>
        /// 生成数据绑定字符串
        /// </summary>
        /// <returns></returns>
        protected virtual string ToBindingData()
        {
            string bindingDataString = "";
            if(!URL.IsNullOrWhiteSpace()&&DataSource==null)
            {
                bindingDataString = @"dataSource:new kendo.data.DataSource({transport: {read: {url: '" + _url + "',dataType:\"jsonp\"}}})";
            }
            else if(URL.IsNullOrWhiteSpace()&&DataSource!=null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(DataSource);
                bindingDataString = @"dataSource:"+json;
            }
            return bindingDataString;
        }
        protected override string ToJSSettings()
        {
            string jsString = base.ToJSSettings();
            string bindingDataString = ToBindingData();
            if (!bindingDataString.IsNullOrWhiteSpace())
                jsString += "," + bindingDataString;
            return jsString;
        }
    }
}
