using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class ColorPicker:MvcExtenseBase
    {
        public ColorPicker(ColorPickerSettings settings)
            :base(settings)
        { }
        public ColorPicker(ColorPickerSettings settings,ViewContext context,
            IViewDataContainer viewDataContainer)
            :base(settings,context,viewDataContainer)
        { }
    }
}
