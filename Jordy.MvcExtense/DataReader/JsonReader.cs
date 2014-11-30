using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.DataReader
{
    public class JsonReader
    {
        public JsonReader()
        {
            RepeatItems = false;
        }

        public bool RepeatItems { get; set; }
        public string Id { get; set; }
    }
}
