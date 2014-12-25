using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class DropdownList:MvcExtenseBase
    {
        public DropdownList(DropdownListSettings settings)
            :base(settings)
        { }
        public DropdownList(DropdownListSettings settings,ViewContext context,
            IViewDataContainer viewContainer)
            :base(settings,context,viewContainer)
        { }
    }
}
