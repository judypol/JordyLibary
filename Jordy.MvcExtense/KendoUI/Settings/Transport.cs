using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class Transport
    {
        public void Read(Action<Read> action)
         {}
        public void Destory(Action<Read> action)    //: "imagebrowser/destroy",
        {}//: "imagebrowser/destroy",
        public void Create(Action<Read> action)                        //: "imagebrowser/createDirectory",
        { }
        public string UploadUrl{get;set;}       //: "imagebrowser/upload",
        public string ThumbnailUrl{get;set;}    //: "imagebrowser/thumbnail",
        public string ImageUrl{get;set;}        //: "/content/images/{0}",
        /// <summary>
        /// 通过Ajax读取数据
        /// </summary>
        public class Read
        {
            /// <summary>
            /// The content-type HTTP header sent to the server. Use "application/json" 
            /// if the content is JSON. Refer to the jQuery.ajax documentation for further info.
            /// </summary>
            public string ContentType{get;set;}
            public string Data{get;set;}
            public string DataType{get;set;}
            /// <summary>
            /// The type of request to make ("POST", "GET", "PUT" or "DELETE"), 
            /// default:get
            /// </summary>
            public string Type{get;set;}
            public string Url{get;set;}
        }
    }
}
