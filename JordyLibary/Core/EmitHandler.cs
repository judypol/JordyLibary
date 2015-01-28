using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JordyLibary.Core
{
    /// <summary>
    /// 反射获取Field值
    /// </summary>
    public class FieldHandler
    {
        public FieldInfo Field
        { get; set; }
        public GetValueHandler GetValue
        {
            get;
            set;
        }
        public SetValueHandler SetValue
        {
            get;
            set;
        }
        public FieldHandler(FieldInfo field)
        {
            GetValue = ReflectionHandlerFactory.FieldGetHandler(field);
            SetValue = ReflectionHandlerFactory.FieldSetHandler(field);
            Field = field;
        }

    }
    /// <summary>
    /// 获取属性值
    /// </summary>
    public class PropertyHandler
    {
        public PropertyHandler(PropertyInfo property)
        {
            if (property.CanWrite)
                Set = ReflectionHandlerFactory.PropertySetHandler(property);
            if (property.CanRead)
                Get = ReflectionHandlerFactory.PropertyGetHandler(property);
            Property = property;
            IndexProperty = Property.GetGetMethod().GetParameters().Length > 0;
        }
        public bool IndexProperty { get; set; }
        public PropertyInfo Property
        {
            get;
            set;
        }
        public GetValueHandler Get
        {
            get;
            set;
        }
        public SetValueHandler Set
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 执行方法
    /// </summary>
    public class MethodHandler
    {
        public MethodHandler(MethodInfo method)
        {
            Execute = ReflectionHandlerFactory.MethodHandler(method);
            Info = method;
        }
        public MethodInfo Info
        {
            get;
            private set;
        }
        public FastMethodHandler Execute
        {
            get;
            private set;
        }
    }
    /// <summary>
    /// 实例化Handler
    /// </summary>
    public class InstanceHandler
    {
        public InstanceHandler(Type type)
        {
            Instance = ReflectionHandlerFactory.InstanceHandler(type);
        }
        public ObjectInstanceHandler Instance
        {
            get;
            private set;
        }
    }
    /// <summary>
    /// Get 方法获取值委托
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public delegate object GetValueHandler(object source);
    /// <summary>
    /// 实例化委托
    /// </summary>
    /// <returns></returns>
    public delegate object ObjectInstanceHandler();
    /// <summary>
    /// Set 设置器
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    public delegate void SetValueHandler(object source,object value);
    /// <summary>
    /// 方法执行委托
    /// </summary>
    /// <param name="target"></param>
    /// <param name="paramters"></param>
    /// <returns></returns>
    public delegate object FastMethodHandler(object target,object[] paramters);
}
