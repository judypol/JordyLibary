using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class EditorSettings:SettingsBase
    {
        public EditorSettings()
            :base("kendoEditor")
        { }
        /// <summary>
        /// Relaxes the same-origin policy when using the iframe-based editor. 
        /// This is done automatically for all cases except when the policy is relaxed by 
        /// document.domain = document.domain. 
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Indicates whether the Editor should submit encoded HTML tags. By default, the submitted value is encoded.
        /// </summary>
        public bool? Encoded { get; set; }
        private string _messages = null;
        public void SetMessage(Action<Messages> action)
        {
            Messages msgs = new Messages();
            action(msgs);
            _messages = msgs.ToString();
        }
        /// <summary>
        /// Allows custom stylesheets to be included within the editing area.
        /// </summary>
        [PropertyName(IsIngoreForJavaScript=true)]
        public List<string> Stylesheets { get; set; }
        protected override string ToJSSettings()
        {
            var jsScript=base.ToJSSettings();
            if(string.IsNullOrWhiteSpace(_messages))
                jsScript = AddOptions(jsScript, string.Format("messages:{{0}}", _messages));
            if(Stylesheets!=null)
            {
                string styles = "";
                foreach(var style in Stylesheets)
                {
                    styles += "'"+style + "',";
                }
                string jsStyle = "stylesheets:["+styles.TrimEnd(',')+"]";
                jsScript = AddOptions(jsScript, jsStyle);
            }

            return jsScript;
        }
        
    }
}
