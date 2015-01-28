using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JordyLibary;
using JordyLibary.Core;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using Model;
using JordyLibary.ORM;

namespace SuperTest
{
    class Program
    {
        static void Main(string[] args)
        {

            TestFile testf = new TestFile();
            testf.TestAutocomplete();
            //testf.Json();
        }
        public static void Test1()
        {
            
        }
        public static void Test2()
        { 
            
        }
        public static void TestAdoHelper()
        {
            ADOHelper ado = new ADOHelper("AdventureWorks2008R2");
            string sql = "Select * from HumanResources.Employee";
            try
            {
                DataSet ds=ado.ExecuteDataSet(sql);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private static void TestReflect()
        {
            //1
            int times = 10000000;
            List<TestClass> tcList = new List<TestClass>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for(int i=0;i<times;i++)
            {
                TestClass tc = new TestClass();
                tc.Name = i.ToString();
                tc.Tip = i.ToString();
                tcList.Add(tc);
            }
            sw.Stop();
            long cs1 = sw.ElapsedMilliseconds;
            Console.WriteLine(sw.ElapsedMilliseconds+"---准备") ;

            #region 正常
            sw = new Stopwatch();
            sw.Start();
            for(int i=0;i<tcList.Count;i++)
            {
                string name = tcList[i].Name;
                string tip = tcList[i].Tip;
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "-----正常测试");
            #endregion

            #region 反射
            PropertyInfo[] pis = (typeof(TestClass)).GetProperties(BindingFlags.Public);
            sw=new Stopwatch();
            sw.Start();
            for (int i = 0; i < tcList.Count; i++)
            {
                foreach(PropertyInfo pi in pis)
                {
                    string val = pi.GetValue(tcList[i], null).ToString();
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "-----反射");
            #endregion

            #region Emit
            sw = new Stopwatch();
            sw.Start();
            Dictionary<string, PropertyHandler> emitDic = new Dictionary<string, PropertyHandler>();
            foreach(PropertyInfo pi in pis)
            {
                if (emitDic.ContainsKey(pi.Name))
                    continue;
                emitDic.Add(pi.Name, new PropertyHandler(pi));
            }
            
            for (int i = 0; i < tcList.Count; i++)
            {
                foreach (PropertyInfo pi in pis)
                {
                    string val = emitDic[pi.Name].Get(tcList[i]).ToString();
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "-----反射Emit");
            #endregion

            #region Dynamic
            sw = new Stopwatch();
            sw.Start();
            Dictionary<string, DynamicMethodExecutor> dynamicDic = new Dictionary<string, DynamicMethodExecutor>();
            foreach (PropertyInfo pi in pis)
            {
                if (emitDic.ContainsKey(pi.Name))
                    continue;
                dynamicDic.Add(pi.Name, new DynamicMethodExecutor(pi.GetGetMethod()));
            }

            for (int i = 0; i < tcList.Count; i++)
            {
                foreach (PropertyInfo pi in pis)
                {
                    string val = dynamicDic[pi.Name].Execute(tcList[i],null).ToString();
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "-----反射Dynamic");
            #endregion
        }

        private static void TestFunValue()
        {
            //1
            int times = 1000000;
            List<TestClass> tcList = new List<TestClass>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < times; i++)
            {
                TestClass tc = new TestClass();
                tc.Name = i.ToString();
                tc.Tip = i.ToString();
                tc.P1 = i.ToString();
                //tcList.Add(tc);
            }
            sw.Stop();
            long cs1 = sw.ElapsedMilliseconds;
            Console.WriteLine(sw.ElapsedMilliseconds + "---准备");


            #region 反射
            PropertyInfo[] pis = (typeof(TestClass)).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < times; i++)
            {
                object obj = Activator.CreateInstance(typeof(TestClass));
                foreach (PropertyInfo pi in pis)
                {
                    pi.SetValue(obj,i.ToString(),null);
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "-----反射");
            #endregion

            #region Emit
            sw = new Stopwatch();
            Dictionary<string, PropertyHandler> emitDic = new Dictionary<string, PropertyHandler>();
            foreach (PropertyInfo pi in pis)
            {
                if (emitDic.ContainsKey(pi.Name))
                    continue;
                emitDic.Add(pi.Name, new PropertyHandler(pi));
            }
            sw.Start();
            

            for (int i = 0; i < times; i++)
            {
                object obj=Activator.CreateInstance(typeof(TestClass));
                foreach (PropertyInfo pi in pis)
                {
                    emitDic[pi.Name].Set(obj, i.ToString());
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "-----反射Emit");
            #endregion

            #region Dynamic
            sw = new Stopwatch();
            
            Dictionary<string, DynamicMethodExecutor> dynamicDic = new Dictionary<string, DynamicMethodExecutor>();
            foreach (PropertyInfo pi in pis)
            {
                if (dynamicDic.ContainsKey(pi.Name))
                    continue;
                dynamicDic.Add(pi.Name, new DynamicMethodExecutor(pi.GetSetMethod()));
            }
            sw.Start();
            for (int i = 0; i < times; i++)
            {
                object obj = Activator.CreateInstance(typeof(TestClass));
                foreach (PropertyInfo pi in pis)
                {
                    dynamicDic[pi.Name].Execute(obj,new object[]{i.ToString()});
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "-----反射Dynamic");
            #endregion
        }

        public static void TestORM()
        {
            DBContext.CurrentConnectionName = "AW2008R2";
            //Address add = (Address.addressID == 1).ListFirst<Address>();
            Address add=new Address ();
            //Query<Address> bb = "";
            //Query<List<Address>> addList = "";

            Stopwatch sw = new Stopwatch();

            DBContext.CurrentConnectionName = "Sqlite";
            Query<List<Customer>> customers = "";               //查询数据

            Customer custor = new Customer();
            //custor.CompanyName = "KKK";
            //custor.Save();                                      //保存数据

            //custor = (Customer.customerID == 1).ListFirst<Customer>();
            //custor.CompanyName = "DDD";
            //custor.Save();                                          //更新数据

            //var aa = (Customer.companyName.Match("D")).List<Customer>();
            Expression exp=Customer.customerID==1;
            exp&=Customer.companyName.Match("F");
            var aa = exp.List<Customer>();
        }

        private static void TestXML()
        {
            XMLHelper xmlHelper = new XMLHelper(@"D:\ProCode\CableRouteModule\CableEntity\ExcelHeaderMap.xml");
            xmlHelper.Read<Table>();
        }
    }
    
    public class Table
    {
        public Table()
        {
            Columns = new List<Column>();
        }
        public string Name { get; set; }
        public IList<Column> Columns { get; set; }
    }
    public class Column
    {
        public string Chinese { get; set; }
        public string English { get; set; }
        public int Index { get; set; }
    }
    public class TestClass
    {
        public string Name { get; set; }
        public string Tip { get; set; }
        public string P1 { get; set; }
    }
}
