using Jordy.MvcExtense.KendoUI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class ColorPalette:MvcExtenseBase
    {
        public ColorPalette(ColorPaletteSettings settings)
            :base(settings)
        { }
        public ColorPalette(ColorPaletteSettings settings,ViewContext context,
            IViewDataContainer viewDataContainer)
            :base(settings,context,viewDataContainer)
        { }
    }
}
