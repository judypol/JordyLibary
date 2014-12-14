using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class Button:MvcExtenseBase
    {
        public Button(ButtonSettings settings)
            :base(settings)
        { }
        public Button(ButtonSettings settings,ViewContext context,IViewDataContainer viewDataContainer)
            : base(settings, context, viewDataContainer)
        {

        }
    }
}
