using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JordyLibary.Core
{
    public class FieldHander
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
        public FieldHander(FieldInfo field)
        {
            GetValue = ReflectionHandlerFactory.FieldGetHandler(field);
            SetValue = ReflectionHandlerFactory.FieldSetHandler(field);
            Field = field;
        }

    }
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

    public delegate object GetValueHandler(object source);
    public delegate object ObjectInstanceHandler();
    public delegate void SetValueHandler(object source,object value);
    public delegate object FastMethodHandler(object target,object[] paramters);
}
