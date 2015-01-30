using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KendoMvcUI.Enums
{
    [Flags]
    public enum FilterString
    {
        StartsWith = 1,
        EndWith = 2,
        Contains = 4
    }
}
