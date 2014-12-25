using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class DateTimePicker:MvcExtenseBase
    {
        public DateTimePicker(DateTimePickerSettings settings)
            : base(settings)
        { }
        public DateTimePicker(DateTimePickerSettings settings,
            ViewContext context,IViewDataContainer viewDataContainer)
            :base(settings,context,viewDataContainer)
        { }
    }
}
