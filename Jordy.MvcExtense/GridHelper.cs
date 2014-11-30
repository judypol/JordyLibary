using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jordy.MvcExtense
{
    public static class GridHelper
    {
        public static JqGrid.Grid Grid(this HtmlHelper helper, string id)
        {
            return new JqGrid.Grid(id);
        }
    }
}
