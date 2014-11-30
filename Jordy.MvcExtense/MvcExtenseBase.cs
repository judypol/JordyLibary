using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Jordy.MvcExtense
{
    public class MvcExtenseBase:IHtmlString
    {
        public string RenderJavascript()
        {
            
            return "";
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
