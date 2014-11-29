using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace JordyCodeGenerator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //Assembly.LoadFile(@"D:\ProCode\JordyLibary\JordyCodeGenerator\bin\Debug\System.Data.SQLite.dll");
            Application.Run(new CodeGeneratorFrm());
        }
    }
}
