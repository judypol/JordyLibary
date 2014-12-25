using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class ComboBox:MvcExtenseBase
    {
        public ComboBox(ComboBoxSettings settings)
            : base(settings)
        { }
        public ComboBox(ComboBoxSettings settings,
            ViewContext context,IViewDataContainer viewDataContainer)
            :base(settings,context,viewDataContainer)
        { }
    }
}
