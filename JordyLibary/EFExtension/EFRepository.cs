using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using JordyLibary.Core;

namespace JordyLibary.EFExtension
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepository<TEntity>:IRepository<TEntity>
        where TEntity:class
    {
        private readonly DbContext _db;
        private IDbSet<TEntity> _entites;
        public EFRepository(IUnitOfWork uow)
        {
            _db = (DbContext)uow;
            if(_db!=null)
            {
                _entites=_db.Set<TEntity>();
            }
        }
        /// <summary>
        /// 根据条件获取所有的实体
        /// </summary>
        /// <param name="where">可以为null</param>
        /// <returns>如果为null返回所有的实体集合，否则根据条件查找</returns>
        public IQueryable<TEntity> GetAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null)
        {
            if(where==null)
                return _entites.AsQueryable();
            
            return _entites.Where(where);
        }
        /// <summary>
        /// 根据Key查找单一的实体对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity GetSingleByKey(System.Linq.Expressions.Expression<Func<TEntity, bool>> where)
        {
            if(where==null)
                throw new ArgumentNullException("where");

            return _entites.FirstOrDefault(where);
        }
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="ent">实体对象</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public int Insert(TEntity ent, bool isSave = true)
        {
            if(ent==null)
                throw new ArgumentNullException("ent");

            _entites.Add(ent);
            return isSave==true?_db.SaveChanges():0;
        }
        /// <summary>
        /// 插入多个实体
        /// </summary>
        /// <param name="ents">需要插入的实体集合</param>
        /// <param name="isSave">是否保存到数据库，如果为false，则没有保存数据库，必须执行SaveChanges()
        /// 来实现事务</param>
        /// <returns></returns>
        public int Insert(IEnumerable<TEntity> ents, bool isSave = true)
        {
            ExceptionExtension.IsNull(ents,"ents");

            foreach(var ent in ents)
            {
                _entites.Add(ent);
            }

            return isSave==true?_db.SaveChanges():0;
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="isSave">是否保存到数据库，如果为false，则没有保存数据库，必须执行SaveChanges()
        /// 来实现事务</param>
        /// <returns></returns>
        public int Update(TEntity ent,bool isSave=true)
        {
            ExceptionExtension.IsNull(ent,"ent");

            var state=_db.Entry<TEntity>(ent).State;
            if(state==EntityState.Detached)
                _entites.Attach(ent);

            _db.Entry<TEntity>(ent).State=EntityState.Modified;

            return isSave==true?_db.SaveChanges():0;
        }
        /// <summary>
        /// 批量更新多个实体
        /// </summary>
        /// <param name="ents"></param>
        /// <param name="isSave">是否保存到数据库，如果为false，则没有保存数据库，必须执行SaveChanges()
        /// 来实现事务</param>
        /// <returns></returns>
        public int Update(IEnumerable<TEntity> ents, bool isSave = true)
        {
            ExceptionExtension.IsNull(ents,"ents");

            foreach(var ent in ents)
            {
                var state=_db.Entry<TEntity>(ent).State;
                if(state==EntityState.Detached)
                    _entites.Attach(ent);

                _db.Entry<TEntity>(ent).State=EntityState.Modified;
            }

            return isSave==true?_db.SaveChanges():0;
        }
        /// <summary>
        /// 批量更新条件相同的实体
        /// </summary>
        /// <param name="where">需要更新的实体条件</param>
        /// <param name="isSave">是否保存到数据库，如果为false，则没有保存数据库，必须执行SaveChanges()
        /// 来实现事务</param>
        /// <returns></returns>
        public int Update(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, 
            bool isSave = true)
        {
            ExceptionExtension.IsNull(where,"where");

            var ents=_entites.Where(where).ToList();
            return Update(ents,isSave);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="isSave">是否保存到数据库，如果为false，则没有保存数据库，必须执行SaveChanges()
        /// 来实现事务</param>
        /// <returns></returns>
        public int Delete(TEntity ent, bool isSave = true)
        {
            ExceptionExtension.IsNull(ent,"ent");

            _entites.Remove(ent);
            return isSave?_db.SaveChanges():0;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ents"></param>
        /// <param name="isSave">是否保存到数据库，如果为false，则没有保存数据库，必须执行SaveChanges()
        /// 来实现事务</param>
        /// <returns></returns>
        public int Delete(IEnumerable<TEntity> ents, bool isSave = true)
        {
            ExceptionExtension.IsNull(ents,"ents");

            foreach(var ent in ents)
            {
                _entites.Remove(ent);
            }
            return isSave?_db.SaveChanges():0;
        }
        /// <summary>
        /// 批量删除（条件）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSave">是否保存到数据库，如果为false，则没有保存数据库，必须执行SaveChanges()
        /// 来实现事务</param>
        /// <returns></returns>
        public int Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> where, bool isSave = true)
        {
            ExceptionExtension.IsNull(where,"where");

            var ents=_entites.Where(where).ToList();
            return isSave?_db.SaveChanges():0;
        }
    }
}
