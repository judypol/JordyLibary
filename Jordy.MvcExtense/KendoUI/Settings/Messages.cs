using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jordy.MvcExtense.Common;

namespace Jordy.MvcExtense.KendoUI.Settings
{
    public class EditorMessages
    {
        /// <summary>
        /// The title of the tool that makes text bold
        /// </summary>
        public string Bold{get;set;}
        /// <summary>
        /// The title of the tool that makes text italicized
        /// </summary>
        public string Italic{get;set;}
        /// <summary>
        /// The title of the tool that underlines text.
        /// </summary>
        public string Underline{get;set;}
        /// <summary>
        /// The title of the tool that strikes through text.
        /// </summary>
        public string Strikethrough{get;set;}
        /// <summary>
        /// The title of the tool that makes text superscript.
        /// </summary>
        public string Superscript{get;set;}
        /// <summary>
        /// The title of the tool that makes text subscript.
        /// </summary>
        public string Subscript{get;set;}
        /// <summary>
        /// The title of the tool that aligns text in the center.
        /// </summary>
        public string JustifyCenter{get;set;}
        /// <summary>
        /// The title of the tool that aligns text on the left.
        /// </summary>
        public string JustifyLeft{get;set;}
        /// <summary>
        /// The title of the tool that aligns text on the right.
        /// </summary>
        public string JustifyRight{get;set;}
        /// <summary>
        /// The title of the tool that justifies text both left and right.
        /// </summary>
        public string JustifyFull{get;set;}
        /// <summary>
        /// The title of the tool that inserts an unordered list.
        /// </summary>
        public string InsertUnorderedList{get;set;}
        /// <summary>
        /// The title of the tool that inserts an ordered list.
        /// </summary>
        public string InsertOrderedList{get;set;}
        /// <summary>
        /// The title of the tool that indents the content.
        /// </summary>
        public string Indent{get;set;}
        /// <summary>
        /// The title of the tool that outdents the content.
        /// </summary>
        public string Outdent{get;set;}
        /// <summary>
        /// The title of the tool that creates hyperlinks.
        /// </summary>
        public string CreateLink{get;set;}
        /// <summary>
        /// The title of the tool that removes hyperlinks.
        /// </summary>
        public string Unlink{get;set;}
        /// <summary>
        /// The title of the tool that inserts images.
        /// </summary>
        public string InsertImage{get;set;}
        /// <summary>
        /// The title of the tool that inserts links to files.
        /// </summary>
        public string InsertFile{get;set;}
        public string InsertHtml{get;set;}
        public string FontName{get;set;}
        public string FontNameInherit{get;set;}
        public string FontSize{get;set;}
        public string FontSizeInherit{get;set;}
        public string FormatBlock{get;set;}
        public string Formatting{get;set;}
        public string Style{get;set;}
        public string ViewHtml{get;set;}
        public string EmptyFolder{get;set;}
        public string UploadFile{get;set;}
        public string OrderBy{get;set;}
        public string OrderBySize{get;set;}
        public string OrderByName{get;set;}
        public string InvalidFileType{get;set;}
        public string DeleteFile{get;set;}    //: "Are you sure you want to delete \"{0}\"?",
        public string OverwriteFile{get;set;}   //: "A file with name \"{0}\" already exists in the current directory. Do you want to overwrite it?",
        public string DirectoryNotFound{get;set;}   //: "A directory with this name was not found.",
        public string ImageWebAddress{get;set;}     //: "Web address",
        public string ImageAltText{get;set;}        //: "Alternate text",
        public string FileWebAddress{get;set;}      //: "Web address",
        public string FileTitle{get;set;}           //: "Title",
        public string LinkWebAddress{get;set;}       //: "Web address",
        public string LinkText{get;set;}            //: "Text",
        public string LinkToolTip{get;set;}         //: "ToolTip",
        public string LinkOpenInNewWindow{get;set;} //: "Open link in new window",
        public string DialogInsert{get;set;}        //: "Insert",
        public string DialogUpdate{get;set;}        //: "Update",
        public string DialogCancel{get;set;}        //: "Cancel",
        public string DialogCancel{get;set;}        //: "Cancel",
        public string CreateTable{get;set;}         //: "Create table",
        public string AddColumnLeft{get;set;}       //: "Add column on the left",
        public string AddColumnRight{get;set;}      //: "Add column on the right",
        public string AddRowAbove{get;set;}         //: "Add row above",
        public string AddRowBelow{get;set;}         //: "Add row below",
        public string DeleteRow{get;set;}           //: "Delete row",
        public string DeleteColumn{get;set;}        //: "Delete column"
        protected string ToJSSettings()
        {
            var js = SettingsEx.ToJSSettings(this);
            return js;
        }
        public override string ToString()
        {
            return ToJSSettings();
        }
    }
}
