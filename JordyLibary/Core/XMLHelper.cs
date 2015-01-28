using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Collections;

namespace JordyLibary.Core
{
    /// <summary>
    /// Linq XML帮助类
    /// </summary>
    public class XMLHelper
    {
        private XElement _xmlDoc = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">XML 文档路径</param>
        public XMLHelper(string path)
        {
            _xmlDoc = XElement.Load(path);
        }
        /// <summary>
        /// 读取指定的内容，XML文档中的节点名必须与类名一致，属性与节点中的属性一致，区分大小写
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <returns>读取到的所有内容</returns>
        public IList<T> Read<T>()
        {
            IList<T> listT = new List<T>();
            Type tType = typeof(T);
            var elems = _xmlDoc.Elements(tType.Name).ToList();      //获取节点
            var properties = tType.GetProperties(BindingFlags.Public|BindingFlags.Instance);     //获取所有的属性
            foreach(var element in elems)
            {
                T t=Activator.CreateInstance<T>();                  //创建一个实例
                ReadXML(element, t);                                //读取内容

                listT.Add(t);                                       //添加到List中
            }
            
            return listT;
        }
        static Dictionary<Type, PropertyInfo[]> _PropertyInfoDic = new Dictionary<Type, PropertyInfo[]>();
        /// <summary>
        /// 从XML中读取相应的节点到类中
        /// </summary>
        /// <param name="element"></param>
        /// <param name="obj"></param>
        public void ReadXML(XElement element,dynamic obj)
        {
            Type tType = obj.GetType();
            PropertyInfo[] pList=null;
            if (_PropertyInfoDic.ContainsKey(tType))
                pList = _PropertyInfoDic[tType];
            else
            {
                pList = tType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                _PropertyInfoDic.Add(tType, pList);
            }

            var attrs = element.Attributes();
            foreach(var p in pList)
            {
                if(p.PropertyType.IsGenericType)        //是否泛型
                {
                    #region 泛型处理
                    dynamic list=null;
                    var genericParmTypes = p.PropertyType.GetGenericArguments();
                    if (p.PropertyType.IsInterface)
                    {
                        string typeStr = "System.Collections.Generic.List`{0}[{1}]";
                        string types = "";
                        Assembly ass = null;
                        foreach(var genericParmType in genericParmTypes)
                        {
                            types = "["+genericParmType.ToString() + ","+genericParmType.Assembly.FullName+"],";
                            ass = genericParmType.Assembly;
                        }
                        typeStr = string.Format(typeStr,genericParmTypes.Length,types.TrimEnd(','));
                        try
                        {
                            list = Activator.CreateInstance(Type.GetType(typeStr));
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("没有找到对应的类库");
                        }
                    }
                    else
                        list= Activator.CreateInstance(p.PropertyType);

                    foreach(var genericParmType in genericParmTypes)
                    {
                        var subElements = element.Elements(genericParmType.Name).ToList();                   

                        foreach(var subElement in subElements)
                        {
                            dynamic genericParm = Activator.CreateInstance(genericParmType);
                            ReadXML(subElement, genericParm);
                            list.Add(genericParm);
                        }

                        p.SetValue(obj, list, null);
                    }
                    #endregion
                }
                else if(p.PropertyType.IsArray)         //是否数组
                {   

                }
                else
                {
                    var attr=attrs.FirstOrDefault(n=>n.Name==p.Name);   //其他类型
                    if (attr != null)
                        p.SetValue(obj,Uility.ChangeType(attr.Value,p.PropertyType), null);
                }
            }
        }

        /// <summary>
        /// 将实体写入ＸＭＬ文件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="parentElement"></param>
        /// <param name="t"></param>
        public static void WriteXML<TEntity>(XElement parentElement,TEntity t)
        {
            try
            {
                string cName=t.GetType().Name;
                XElement ele = new XElement(cName);
                PropertyInfo[] pis=t.GetType().GetProperties(BindingFlags.Public|BindingFlags.Instance);
                foreach(var pi in pis)
                {
                    var piValue=pi.GetValue(t,null);            //写入属性
                    ele.SetAttributeValue(pi.Name,piValue);
                }
                parentElement.Add(ele);
            }
            catch(Exception ex)
            {
                throw new Exception("WriteXML ParentElement",ex);
            }
        }
        /// <summary>
        /// 将实体对象写入XML文件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <param name="t"></param>
        public static void WriteXML<TEntity>(string filePath,TEntity t)
        {
            try
            {
                var parent = XElement.Load(filePath);
                
                if (parent == null)
                    throw new Exception("can not find the "+"xml element");

                WriteXML<TEntity>(parent, t);
                parent.Save(filePath);
            }
            catch(Exception ex)
            {
                throw new Exception("WriteXML filepath", ex);
            }
        }
    }
}
