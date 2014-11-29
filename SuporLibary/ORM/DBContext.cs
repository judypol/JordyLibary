using JordyLibary.Core;
using JordyLibary.ORM.Mappings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;

namespace JordyLibary.ORM
{
    /// <summary>
    /// 数据处理对象
    /// </summary>
    public class DBContext
    {
        internal static BlowFishCS.BlowFish Blowfish = new BlowFishCS.BlowFish("abcd87uier");

        public static int DefaultListMaxSize
        {
            get;
            set;
        }
        public static Dictionary<string,ConnectionStringSettings> ConnectionCollection { get; set; }

        static DBContext()
        {
            DefaultListMaxSize = 100;
            try
            {   //从配置文件中获取所有的连接字符串
                ConnectionCollection = new Dictionary<string, ConnectionStringSettings>();
                foreach (ConnectionStringSettings settings in ConfigurationManager.ConnectionStrings)
                {
                    if(!ConnectionCollection.ContainsKey(settings.Name))
                        ConnectionCollection.Add(settings.Name, settings);
                }
                if(ConnectionCollection.Count>1)
                    CurrentConnectionName = (ConnectionCollection.ElementAt(1)).Key;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //LoadEntity();
        }

        private static Dictionary<Type, Mappings.PropertyCastAttribute> mCasts = new Dictionary<Type, PropertyCastAttribute>();

        public static void AddCast(Type type, Mappings.PropertyCastAttribute cast)
        {
            lock (mCasts)
            {
                if (!mCasts.ContainsKey(type))
                {
                    mCasts.Add(type, cast);
                }
            }
        }
        public static void AddCast<T>(Mappings.PropertyCastAttribute cast)
        {
            AddCast(typeof(T), cast);
        }
        public static void AddCast<T, CAST>() where CAST : Mappings.PropertyCastAttribute, new()
        {
            AddCast<T>(new CAST());
        }

        internal static Mappings.PropertyCastAttribute GetCast(Type type)
        {
            Mappings.PropertyCastAttribute result = null;
            mCasts.TryGetValue(type, out result);
            return result;
        }

        internal static void Init()
        {
        }

        public static IDbCommand LastCommand
        {
            get;
            set;
        }

        public static void LoadEntityByAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass && Utils.GetTypeAttributes<Mappings.TableAttribute>(type, false).Length > 0)
                {
                    Mappings.ObjectMapper.GetOM(type);
                }
            }
        }

        public static void LoadEntity(params Type[] types)
        {
            foreach (Type item in types)
            {
                Mappings.ObjectMapper.GetOM(item);
            }
        }

        [ThreadStatic]
        private static AsyncDelegate<IDbCommand> mExecuting;

        public static AsyncDelegate<IDbCommand> Executing
        {
            get
            {
                return mExecuting;
            }
            set
            {
                mExecuting = value;
            }

        }

        public static string CurrentConnectionName { get; set; }
        /// <summary>
        /// 当前数据库连接
        /// </summary>
        /// <param name="connName"></param>
        /// <returns></returns>
        internal static ConnectionHandler CurrentHandler(string connName)
        {
            if (string.IsNullOrEmpty(connName))
                throw new PeanutException(DataMsg.CONNECTION_STRING_ERROR);
            CurrentConnectionName = connName;
            if (HandlerTable.ContainsKey(connName))
                return HandlerTable[connName];
            return null;
        }

        [ThreadStatic]
        static Dictionary<string, ConnectionHandler> mHandlerTable = new Dictionary<string, ConnectionHandler>(200);

        internal static Dictionary<string, ConnectionHandler> HandlerTable
        {
            get
            {
                if (mHandlerTable == null)
                    mHandlerTable = new Dictionary<string, ConnectionHandler>(10);
                return mHandlerTable;

            }
        }

        /// <summary>
        /// 添加一个连接操作
        /// </summary>
        /// <param name="connName"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static ConnectionHandler AddConnectionHandler(string connName, IDriver driver)
        {
            try
            {
                IDbConnection conn = driver.Connection;
                conn.ConnectionString = ConnectionCollection[connName].ConnectionString;
                conn.Open();
                ConnectionHandler ch = new ConnectionHandler(conn);
                ch.Driver = driver;
                if(!HandlerTable.ContainsKey(connName))
                    HandlerTable.Add(connName, ch);
                return ch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static void RemoveConnetionHandler(string db)
        {

            if (HandlerTable.ContainsKey(db))
                HandlerTable.Remove(db);

        }

        /// <summary>
        /// 设置一个数据库连接
        /// </summary>
        /// <param name="connName"></param>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        public static void SetConnectionString(string connName, string connectionString,string providerName)
        {
            ConnectionStringSettings settings = new ConnectionStringSettings(connName, connectionString, providerName);
            if(!ConnectionCollection.ContainsKey(connName))
                ConnectionCollection.Add(connName,settings);

            CurrentConnectionName = connName;
        }

        public static string GetConnectionString(string connName)
        {
            return ConnectionCollection[connName].ConnectionString;
        }

        public static IConnectinContext GetConnection(string connName)
        {
            if (connName == null)
                connName = CurrentConnectionName;
            ConnectionStringSettings settings = ConnectionCollection[connName];
            if (settings == null)
                throw new Exception("没有找到对应的数据库");
            IDriver driver = CreateDriver(settings.ProviderName);
            return new ConnectionContext(settings.ConnectionString, driver, connName);
        }
        /// <summary>
        /// 根据数据提高者创建IDriver
        /// </summary>
        /// <param name="providerName">数据提供者</param>
        /// <returns></returns>
        private static IDriver CreateDriver(string providerName)
        {
            string tProviderName = providerName.ToLower();
            if (tProviderName.Contains("sqlclient"))
                return new MSSQL();
            else if (tProviderName.Contains("sqlite"))
                return new Sqlite();
            else if (tProviderName.Contains("odbc"))
                return new ODBC();
            else
                return new MSSQL();
        }

        public static IConnectinContext DB1
        {
            get
            {
                return GetConnection(CurrentConnectionName);
            }
        }

        public static object ExecProc(object parameter)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return ExecProc(cc, parameter);
            }
        }

        public static object ExecProc(string connName, object parameter)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return ExecProc(cc, parameter);
            }
        }

        public static object ExecProc(IConnectinContext cc, object parameter)
        {
            return cc.ExecProc(parameter);
        }

        public static T ExecProcToObject<T>(object parameter) where T : new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return ExecProcToObject<T>(cc, parameter);
            }
        }

        public static object ExecProcToObject(Type type, IConnectinContext cc, object parameter)
        {
            IList items = cc.ListProc(type, parameter);
            if (items != null && items.Count > 0)
                return items[0];
            return null;
        }

        public static T ExecProcToObject<T>(string connName, object parameter) where T : new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return ExecProcToObject<T>(cc, parameter);
            }
        }

        public static T ExecProcToObject<T>(IConnectinContext cc, object parameter) where T : new()
        {
            return (T)ExecProcToObject(typeof(T), cc, parameter);
        }

        public static IList<T> ExecProcToObjects<T>(object parameter) where T : new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return ExecProcToObjects<T>(cc, parameter);
            }
        }

        public static IList<T> ExecProcToObjects<T>(string connName, object parameter) where T : new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return ExecProcToObjects<T>(cc, parameter);
            }
        }
        public static IList<T> ExecProcToObjects<T>(IConnectinContext cc, object parameter) where T : new()
        {
            return cc.ListProc<T>(parameter);
        }

        public static T Load<T>(object id) where T : IEntityState, new()
        {
            using (IConnectinContext cc = DB1)
            {
                return Load<T>(id, cc);
            }
        }

        public static int Add(params Mappings.DataObject[] obj)
        {
            int result = 0;
            using (IConnectinContext cc = DBContext.DB1)
            {
                cc.BeginTransaction();
                result = Add(cc, obj);
                cc.Commit();
            }
            return result;
        }

        public static int Add(IConnectinContext cc, params Mappings.DataObject[] obj)
        {
            int result = 0;
            for (int i = 0; i < obj.Length; i++)
            {

                obj[i].EntityState._Loaded = false;
                result = result + obj[i].Save(cc);
            }
            return result;
        }

        public static int Save(params Mappings.DataObject[] obj)
        {
            int result = 0;
            using (IConnectinContext cc = DBContext.DB1)
            {

                result = Save(cc, obj);

            }
            return result;
        }
        public static int Save(string connName, params Mappings.DataObject[] obj)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return Save(cc, obj);
            }
        }
        public static int Save(IConnectinContext cc, params Mappings.DataObject[] obj)
        {
            int result = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                result = result + obj[i].Save(cc);
            }
            return result;
        }

        public static int Delete(params Mappings.DataObject[] obj)
        {
            int result = 0;
            using (IConnectinContext cc = DBContext.DB1)
            {

                result = result + Delete(cc, obj);

            }
            return result;
        }


        public static int Delete(string connName, params Mappings.DataObject[] obj)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return Delete(cc, obj);
            }

        }
        public static int Delete(IConnectinContext cc, params Mappings.DataObject[] obj)
        {
            int result = 0;
            for (int i = 0; i < obj.Length; i++)
            {

                result = result + obj[i].Delete(cc);
            }
            return result;
        }

        public static IDisposable ChangeTable<T>(string tn) where T : IEntityState, new()
        {
            Mappings.ChangeTables change = new Mappings.ChangeTables();
            change.Add<T>(tn);
            return change;
        }

        public static IDisposable ChangeTable<T, T1>(string tn1, string tn2)
            where T : IEntityState, new()
            where T1 : IEntityState, new()
        {
            Mappings.ChangeTables change = new Mappings.ChangeTables();
            change.Add<T>(tn1);
            change.Add<T1>(tn2);
            return change;
        }
        public static IDisposable ChangeTable<T, T1, T2>(string tn1, string tn2, string tn3)
            where T : IEntityState, new()
            where T1 : IEntityState, new()
            where T2 : IEntityState, new()
        {
            Mappings.ChangeTables change = new Mappings.ChangeTables();
            change.Add<T>(tn1);
            change.Add<T1>(tn2);
            change.Add<T2>(tn3);
            return change;
        }

        public static IDisposable ChangeTable<T, T1, T2, T3>(string tn1, string tn2, string tn3, string tn4)
            where T : IEntityState, new()
            where T1 : IEntityState, new()
            where T2 : IEntityState, new()
            where T3 : IEntityState, new()
        {
            Mappings.ChangeTables change = new Mappings.ChangeTables();
            change.Add<T>(tn1);
            change.Add<T1>(tn2);
            change.Add<T2>(tn3);
            change.Add<T3>(tn4);
            return change;
        }

        public static IDisposable ChangeTable<T, T1, T2, T3, T4>(string tn1, string tn2, string tn3, string tn4, string tn5)
            where T : IEntityState, new()
            where T1 : IEntityState, new()
            where T2 : IEntityState, new()
            where T3 : IEntityState, new()
            where T4 : IEntityState, new()
        {
            Mappings.ChangeTables change = new Mappings.ChangeTables();
            change.Add<T>(tn1);
            change.Add<T1>(tn2);
            change.Add<T2>(tn3);
            change.Add<T3>(tn4);
            change.Add<T4>(tn5);
            return change;
        }

        public static T Load<T>(object id, string connName) where T : IEntityState, new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return Load<T>(id, cc);
            }
        }

        public static T Load<T>(object id, IConnectinContext cc) where T : IEntityState, new()
        {
            return (T)Load(typeof(T), id, cc);

        }

        internal static object Load(Type type, object id, IConnectinContext cc)
        {
            Mappings.ObjectMapper om = Mappings.ObjectMapper.GetOM(type);
            Mappings.SelectDataReader sr = om.GetSelectReader(type);
            if (om.ID == null)
                throw new PeanutException(DataMsg.ID_MAP_NOTFOUND);
            Expression exp = new Expression();
            exp.SqlText.Append(om.ID.ColumnName + "=@p1");
            exp.Parameters.Add(new Command.Parameter { Name = "p1", Value = id });
            return EntityBase.ExOnListFirst(type, cc, om.GetSelectTable(sr), exp, null, null);
        }

        public static void TransactionExecute(string connName, Action<IConnectinContext> handler)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                cc.BeginTransaction();
                handler(cc);
                cc.Commit();
            }
        }

        public static void Transaction(Action<IConnectinContext> handler)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                cc.BeginTransaction();
                handler(cc);
                cc.Commit();
            }
        }
        public static void Transaction(string connName, Action<IConnectinContext> handler)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                cc.BeginTransaction();
                handler(cc);
                cc.Commit();
            }
        }
        public static void Transaction(IConnectinContext cc, Action<IConnectinContext> handler)
        {
            using (cc)
            {
                cc.BeginTransaction();
                handler(cc);
                cc.Commit();
            }
        }

        public static int SaveAsClone(string connName, Mappings.DataObject data)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return SaveAsClone(cc, data);
            }
        }
        public static int SaveAsClone(IConnectinContext cc, Mappings.DataObject data)
        {
            Type type = data.GetType();
            Mappings.ObjectMapper om = Mappings.ObjectMapper.GetOM(type);
            Mappings.DataObject result = (Mappings.DataObject)Activator.CreateInstance(type);
            OnClone(data, result, om);
            return Save(cc, result);
        }

        public static int SaveAsClone(Mappings.DataObject data)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return SaveAsClone(cc, data);
            }

        }

        private static void OnClone(object source, object newobj, Mappings.ObjectMapper om)
        {
            object value = null;
            Mappings.PropertyMapper pm = om.ID;
            if (pm != null)
            {
                value = pm.Handler.Get(source);
                if (value != null)
                    pm.Handler.Set(newobj, value);
            }
            for (int i = 0; i < om.Properties.Count; i++)
            {
                Mappings.PropertyMapper p = om.Properties[i];
                value = p.Handler.Get(source);
                if (value != null)
                    p.Handler.Set(newobj, value);
            }
        }

        public static T Clone<T>(T source) where T : IEntityState, new()
        {
            T item = new T();

            Mappings.ObjectMapper om = Mappings.ObjectMapper.GetOM(typeof(T));
            OnClone(source, item, om);
            return item;

        }

        public static IList<ValidaterError> Verify(object data)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return Verify(data, cc);
            }
        }

        public static IList<ValidaterError> Verify(object data, IConnectinContext cc)
        {
            Mappings.ObjectMapper om = Mappings.ObjectMapper.GetOM(data.GetType());
            List<ValidaterError> errors = new List<ValidaterError>(om.Properties.Count);
            for (int i = 0; i < om.Properties.Count; i++)
            {
                PropertyMapper pm = om.Properties[i];
                object value = pm.Handler.Get(data);
                for (int k = 0; k < pm.Validaters.Length; k++)
                {
                    ValidaterAttribute val = pm.Validaters[k];

                    if (!val.Validating(value, data, pm, cc))
                    {
                        errors.Add(new ValidaterError { Name = pm.ColumnName, Error = val.Message });
                    }
                }
            }
            return errors;
        }

        public static void MemberCopyTo(object source, object target)
        {
            ModuleCast mc = ModuleCast.GetCast(source.GetType(), target.GetType());
            mc.Cast(source, target);
        }
        public static EventGetRegionValues GetRegionValues;


        [ThreadStatic]
        private static Region mCurrentRegion;

        public static Region CurrentRegion
        {
            get
            {
                return mCurrentRegion;
            }
            set
            {
                mCurrentRegion = value;
            }
        }
        [ThreadStatic]
        private static string[] mCurrentOrderBy = null;

        public static string[] CurrentOrderBy
        {
            get
            {
                return mCurrentOrderBy;
            }
            set
            {
                mCurrentOrderBy = value;
            }
        }
        [ThreadStatic]
        private static DB mCurrentConnectonType = DB.DB1;

        public static DB CurrentConnectonType
        {
            get
            {
                return mCurrentConnectonType;
            }
            set
            {
                mCurrentConnectonType = value;
            }
        }
    }
#if NET_4 || NET_3
    public static class M2M
    {
        public static void MemberCopyTo(this object source, object target)
        {
            DBContext.MemberCopyTo(source, target);
        }
    }
#endif
    public partial class Expression
    {
        public Expression(string sql)
        {
            SqlText.Append(sql);
        }

        public static Expression operator &(Expression exp1, string sql)
        {
            return exp1 & new Expression(sql);
        }

        public static Expression operator |(Expression exp1, string sql)
        {
            return exp1 | new Expression(sql);
        }

        public Expression this[string name, object value]
        {
            get
            {
                return Add(name, value);
            }
        }



    }

    public class StoredProcedure
    {
    }
#if NET_4 || NET_3
    public static class DataHelper
    {

        public static object Exec(this StoredProcedure parameter)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return Exec(parameter, cc);
            }
        }
        public static object Exec(this StoredProcedure parameter, string connName)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return Exec(parameter, cc);
            }
        }
        public static object Exec(this StoredProcedure parameter, IConnectinContext cc)
        {
            return cc.ExecProc(parameter);
        }

        public static T ListFirst<T>(this StoredProcedure parameter) where T : new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return ListFirst<T>(parameter, cc);
            }
        }

        public static T ListFirst<T>(this StoredProcedure parameter, DB db) where T : new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(DBContext.CurrentConnectionName))
            {
                return ListFirst<T>(parameter, cc);
            }
        }

        public static T ListFirst<T>(this StoredProcedure parameter, IConnectinContext cc) where T : new()
        {
            IList<T> items = cc.ListProc<T>(parameter);
            if (items != null && items.Count > 0)
                return items[0];
            return default(T);
        }

        public static IList<T> List<T>(this StoredProcedure parameter) where T : new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return List<T>(parameter, cc);
            }
        }
        public static IList<T> List<T>(this StoredProcedure parameter, DB db) where T : new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(DBContext.CurrentConnectionName))
            {
                return List<T>(parameter, cc);
            }
        }

        public static IList<T> List<T>(this StoredProcedure parameter, IConnectinContext cc) where T : new()
        {
            return cc.ListProc<T>(parameter);
        }
        public static T Load<T>(this ValueType source, DB db) where T : IEntityState, new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(DBContext.CurrentConnectionName))
            {
                return Load<T>(source, cc);
            }
        }
        public static T Load<T>(this ValueType source, IConnectinContext cc) where T : IEntityState, new()
        {
            return DBContext.Load<T>(source, cc);
        }
        public static void Save(this EntityBase source, string connName)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                 Save(source, cc);
            }
        }
        public static void Save(this EntityBase source, IConnectinContext cc)
        {
            DBContext.Save(cc, (Mappings.DataObject)source);
        }

        public static void Save(this EntityBase source)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                Save(source, cc);
            }
        }

        public static T Load<T>(this ValueType source) where T : IEntityState, new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return Load<T>(source, cc);
            }
        }

        public static Expression ToExpression(this string sql)
        {
            return new Expression(sql);
        }

        public static Expression Add(this string sql, string name, object value)
        {
            Expression exp = new Expression(sql);
            exp.Add(name, value);
            return exp;
        }

        public static SQL Parameter(this string sql, string name, object value)
        {
            SQL select = new SQL(sql);
            select.Parameter(name, value);
            return select;
        }

        public static T GetValue<T>(this string sql)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return GetValue<T>(sql, cc);
            }
        }

        public static int Execute(this string sql)
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return Execute(sql, cc);
            }
        }


        public static int Execute(this string sql, string connName)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return Execute(sql, cc);
            }
        }

        public static int Execute(this string sql, IConnectinContext cc)
        {
            Command cmd = new Command(sql);
            return cc.ExecuteNonQuery(cmd);
        }

        public static T GetValue<T>(this string sql, string connName)
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return GetValue<T>(sql, cc);
            }
        }

        public static T GetValue<T>(this string sql, IConnectinContext cc)
        {
            SQL select = new SQL(sql);
            return select.GetValue<T>(cc);
        }

        public static T ListFirst<T>(this string sql) where T : new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return ListFirst<T>(sql, cc);
            }
        }


        public static T ListFirst<T>(this string sql, string connName) where T : new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return ListFirst<T>(sql, cc);
            }
        }

        public static T ListFirst<T>(this string sql, IConnectinContext cc) where T : new()
        {
            SQL select = new SQL(sql);
            return select.ListFirst<T>(cc);

        }

        public static IList<T> List<T>(this string sql) where T : new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return List<T>(sql, cc);
            }
        }

        public static IList<T> List<T>(this string sql, Region region) where T : new()
        {
            using (IConnectinContext cc = DBContext.DB1)
            {
                return List<T>(sql, cc, region);
            }
        }


        public static IList<T> List<T>(this string sql, string connName ) where T : new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return List<T>(sql, cc);
            }
        }
        public static IList<T> List<T>(this string sql, IConnectinContext cc) where T : new()
        {
            return List<T>(sql, cc, null);
        }


        public static IList<T> List<T>(this string sql, string connName, Region region) where T : new()
        {
            using (IConnectinContext cc = DBContext.GetConnection(connName))
            {
                return List<T>(sql, cc, region);
            }
        }

        public static IList<T> List<T>(this string sql, IConnectinContext cc, Region region) where T : new()
        {
            SQL select = new SQL(sql);
            return select.List<T>(cc, region);
        }

    }

#endif
}
