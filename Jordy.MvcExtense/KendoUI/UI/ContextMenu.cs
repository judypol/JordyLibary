using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class ContextMenu:MvcExtenseBase
    {
        public ContextMenu(ContextMenuSettings settings)
            :base(settings)
        {

        }
        public ContextMenu(ContextMenuSettings settings,
            ViewContext context, IViewDataContainer viewDataContainer)
            : base(settings, context, viewDataContainer)
        { }
    }
}
