using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RazorEngine;

namespace JordyCodeGenerator
{
    public class RazorTemplate
    {
        private string _templateContents = null;
        public RazorTemplate(string templateFile,bool contents=true)
        {
            _templateContents = templateFile;
        }
        public RazorTemplate(string templateFileName)
        {
            try
            {
                _templateContents = File.ReadAllText(templateFileName, System.Text.Encoding.Default);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string _result = null;
        public void Parse(object model)
        {
            string result = Razor.Parse(_templateContents,model);
            _result = result;
        }
        public void SaveFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.Write(_result);
                sw.Flush();
                sw.Close();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
