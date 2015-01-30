using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class Calendar : MvcExtenseBase
    {
        public Calendar(CalendarSettings settings)
            :base(settings)
        { }
        public Calendar(CalendarSettings settings,ViewContext context,IViewDataContainer viewDataContainer)
            :base(settings,context,viewDataContainer)
        { }
    }
}
