using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class DatePicker:MvcExtenseBase
    {
        public DatePicker(DatePickerSettings settings)
            :base(settings)
        {}
        public DatePicker(DatePickerSettings settings,
            ViewContext context,IViewDataContainer viewDataContainer)
            :base(settings,context,viewDataContainer)
        { }
    }
}
