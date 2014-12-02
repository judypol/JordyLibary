using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jordy.MvcExtense.Extensions;
using Jordy.MvcExtense.Enums;

namespace Jordy.MvcExtense.KendoUI.UI
{
    public class AutoComplete:MvcExtenseBase
    {
        #region field
        public string ID { get; set; }
        public object DataSource { get; set; }
        public string DataTextField { get; set; }
        public string URL { get; set; }
        public DataType? DataType { get; set; }
        public bool? Enable { get; set; }
        #endregion
        public AutoComplete(string id)
        {
            ID = id;
        }
        #region Public Set Method
        public AutoComplete SetID(string id)
        {
            ID = id;
            return this;
        }
        public AutoComplete SetData(object datasource)
        {
            DataSource = datasource;
            return this;
        }
        public AutoComplete SetDataTextField(string dataTextField)
        {
            DataTextField = dataTextField;
            return this;
        }
        public AutoComplete SetURL (string url)
        {
            URL = url;
            return this;
        }
        public AutoComplete SetDataType (DataType dataType)
        {
            DataType = dataType;
            return this;
        }
        #endregion
        
    }
}
