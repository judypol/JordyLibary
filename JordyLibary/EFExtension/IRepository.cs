using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JordyLibary.EFExtension
{
    /// <summary>
    /// 数据仓储接口，定义对数据进行的一些基本操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 获取所有的数据对象，可以是条件
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity,bool>> where = null);
        /// <summary>
        /// 通过key来查找单一的数据
        /// </summary>
        /// <param name="where">key条件</param>
        /// <returns></returns>
        TEntity GetSingleByKey(Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 插入一条数据到数据集中
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Insert(TEntity ent,bool isSave=true);
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="ents"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Insert(IEnumerable<TEntity> ents, bool isSave = true);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        int Update(TEntity ent);
        /// <summary>
        /// 更新数据集
        /// </summary>
        /// <param name="ents"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> ents, bool isSave = true);
        /// <summary>
        /// 根据条件批量更新数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> where, bool isSave = true);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        int Delete(TEntity ent,bool isSave=true);
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="ents"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Delete(IEnumerable<TEntity> ents, bool isSave = true);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> where, bool isSave = true);
    }
}
