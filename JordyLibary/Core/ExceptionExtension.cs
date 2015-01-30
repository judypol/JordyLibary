using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.Core
{
    public class ExceptionExtension
    {
        public static void IsNull(object obj,string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}
