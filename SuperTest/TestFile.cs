using Jordy.MvcExtense.KendoUI.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Jordy.MvcExtense.KendoUI;
using System.Collections;
using Newtonsoft.Json;

namespace SuperTest
{
    public class TestFile
    {
        public void TestAutocomplete()
        {
            ViewContext view = new ViewContext();
            MyViewData viewData = new MyViewData();
            HtmlHelper helper=new HtmlHelper(view,viewData);
            MvcHtmlString c=helper.UI().AutoComplete(a=>{
                a.Id = "autocomplete";
                //a.URL = "Home/Index";
                a.DataSource = new List<string> { "one","two","three"};
            });
            string b = c.ToString();
        }
        public void Json()
        {
            List<string> list = new List<string>();
            for(int i=0;i<10;i++)
            {
                list.Add("bbb"+i.ToString());
            }
            string json = ToJson(list);
        }
        public string ToJson(IEnumerable value)
        {
            string a = JsonConvert.SerializeObject(value);
            return a;
        }
    }
    public class MyViewData:IViewDataContainer
    {

        public ViewDataDictionary ViewData
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

   
}
