using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data;

namespace JordyLibary.Core
{
    /// <summary>
    /// 数据库操作帮助类
    /// </summary>
    public class ADOHelper:IDisposable
    {
        /// <summary>
        /// 获取配置文件中的所有数据库连接字符串
        /// </summary>
        private static ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

        private IDbConnection _connection = null;
        private string _dbProviderName = null;
        private string _connString=null;
        private IDbTransaction _transaction = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connName">配置文件中对应的Name</param>
        public ADOHelper(string connName)
        {
            _dbProviderName=connectionStrings[connName].ProviderName;
            _connString=connectionStrings[connName].ConnectionString;
            this._connection = CreateConnection();
        }
        /// <summary>
        /// 构造函数，根据连接字符串和Provider来新建一个实例
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dbProviderName"></param>
        public ADOHelper(string connectionString,string dbProviderName)
        {
            this._dbProviderName=dbProviderName;
            this._connString=connectionString;
            this._connection = CreateConnection();
        }
        /// <summary>
        /// 创建一个数据库连接，并打开连接
        /// </summary>
        /// <returns></returns>
        private IDbConnection CreateConnection()
        {
            try
            {
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(_dbProviderName);
                IDbConnection dbconn = dbfactory.CreateConnection();
                dbconn.ConnectionString = _connString;
                if (dbconn.State != ConnectionState.Open)
                    dbconn.Open();
                return dbconn;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        #region 开启事务
        public void StartTransaction()
        {
            if (_transaction != null)
                return;

            _transaction = _connection.BeginTransaction();
        }
        public void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();
        }
        #endregion
        #region 创建Command对象
        /// <summary>
        /// 返回一个存储过程Command
        /// </summary>
        /// <param name="storedProcedure">存储过程名</param>
        /// <returns></returns>
        public IDbCommand GetStoredProcCommond(string storedProcedure)
        {
            IDbCommand dbCommand = this._connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }
        /// <summary>
        /// 返回一个SQL语句的Command
        /// </summary>
        /// <param name="sqlQuery">SQL语句</param>
        /// <returns></returns>
        public IDbCommand GetSqlStringCommond(string sqlQuery)
        {
            IDbCommand dbCommand = _connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }
        #endregion
 
        #region 增加参数
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }
        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="cmd">command对象</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size"></param>
        public void AddOutParameter(IDbCommand cmd, string parameterName, DbType dbType, int size)
        {
            IDbDataParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }
        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">值</param>
        public void AddInParameter(IDbCommand cmd, string parameterName, DbType dbType, object value)
        {
            IDbDataParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }
        /// <summary>
        /// 输入结果参数
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">参数类型</param>
        public void AddReturnParameter(IDbCommand cmd, string parameterName, DbType dbType)
        {
            IDbDataParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="parameterName">参数名</param>
        /// <returns>参数对象</returns>
        public IDbDataParameter GetParameter(IDbCommand cmd, string parameterName)
        {
            return (IDbDataParameter)cmd.Parameters[parameterName];
        }
 
        #endregion
 
        #region 执行
        /// <summary>
        /// Excute
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="isStartTransaction">是否开启事务</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(IDbCommand cmd,bool startTransaction=false)
        {
            try
            {
                if(startTransaction)
                {
                    this.StartTransaction();
                    cmd.Connection = _transaction.Connection;
                    cmd.Transaction = _transaction;
                }
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(this._dbProviderName);
                IDbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                dbDataAdapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dbDataAdapter.Fill(ds);
                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行一个Excute
        /// </summary>
        /// <param name="sql">sql 查询语句</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql,bool startTransaction=false)
        {
            try
            {
                IDbCommand cmd = this.GetSqlStringCommond(sql);
                return this.ExecuteDataSet(cmd,startTransaction);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
 
        public DataTable ExecuteDataTable(IDbCommand cmd,bool startTransaction=false)
        {
            try
            {
                if (startTransaction)
                {
                    this.StartTransaction();
                    cmd.Connection = _transaction.Connection;
                    cmd.Transaction = _transaction;
                }
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(_dbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                dbDataAdapter.SelectCommand = (DbCommand)cmd;
                DataTable dataTable = new DataTable();
                dbDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ExecuteDataTable(string sql,bool startTransaction=false)
        {
            try
            {
                IDbCommand cmd = this.GetSqlStringCommond(sql);
                return ExecuteDataTable(cmd,startTransaction);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        /// <summary>
        /// 执行Reader操作，在Reader操作结束以后起调用Reader.Close()方法。
        /// </summary>
        /// <param name="cmd">Command对象</param>
        /// <param name="startTransaction">是否开启事务</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(IDbCommand cmd,bool startTransaction=false)
        {
            try
            {
                if (startTransaction)
                {
                    this.StartTransaction();
                    cmd.Connection = _transaction.Connection;
                    cmd.Transaction = _transaction;
                }
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行Reader操作，在Reader操作结束以后起调用Reader.Close()方法。
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="startTransaction">是否开启事务</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sql,bool startTransaction=false)
        {
            try
            {
                IDbCommand cmd = GetSqlStringCommond(sql);
                return ExecuteReader(cmd,startTransaction);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        /// <summary>
        /// 执行插入，更新，删除操作，如果开启了事务，在结束的时候请执行，Commit()方法
        /// </summary>
        /// <param name="cmd">Command 对象</param>
        /// <param name="startTransaction">是否开启事务</param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbCommand cmd,bool startTransaction=false)
        {
            try
            {
                if (startTransaction)
                {
                    this.StartTransaction();
                    cmd.Connection = _transaction.Connection;
                    cmd.Transaction = _transaction;
                }
                int ret = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行插入，更新，删除操作，如果开启了事务，在结束的时候请执行，Commit()方法
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="startTransaction">是否开启事务</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql,bool startTransaction=false)
        {
            try
            {
                IDbCommand cmd = GetSqlStringCommond(sql);
                return ExecuteNonQuery(cmd,startTransaction);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        /// <summary>
        /// 返回查询的第一行第一列值
        /// </summary>
        /// <param name="cmd">Command 对象</param>
        /// <param name="startTransaction">是否开启事务</param>
        /// <returns></returns>
        public object ExecuteScalar(IDbCommand cmd,bool startTransaction=false)
        {
            try
            {
                if (startTransaction)
                {
                    this.StartTransaction();
                    cmd.Connection = _transaction.Connection;
                    cmd.Transaction = _transaction;
                }
                object ret = cmd.ExecuteScalar();
                cmd.Connection.Close();
                return ret;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }  
        }
        /// <summary>
        /// 返回查询的第一行第一列值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="startTransaction"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql,bool startTransaction=false)
        {
            try
            {
                IDbCommand cmd = GetSqlStringCommond(sql);
                return ExecuteScalar(cmd,startTransaction);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public T ExecuteScalar<T>(string sql,bool startTransaction=false)
        {
            try
            {
                return Uility.ChangeType<T>(ExecuteScalar(sql,startTransaction));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion
    
        public void Dispose()
        {
            Close();
        }
        public void Close()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }
        public void Open()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
