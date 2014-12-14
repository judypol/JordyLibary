using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jordy.MvcExtense.Extensions;
using Jordy.MvcExtense.Enums;
using Jordy.MvcExtense.KendoUI.Settings;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class AutoComplete:MvcExtenseBase
    {
        public AutoComplete(AutoCompleteSettings settings)
            :base(settings)
        {
            
        }
        
        public AutoComplete(AutoCompleteSettings settings,ViewContext context,IViewDataContainer viewDataContainer)
            :base(settings,context,viewDataContainer)
        {

        }
    }
}
