using System;
using System.Linq;
using BLOG.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BLOG.DAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class
    {
        private readonly BLOGContext blogDb;
        //注入
        public BaseDAL(BLOGContext _blogDb)
        {
            blogDb = _blogDb;
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="wherelambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> wherelambda)
        {
            try
            {
                return blogDb.Set<T>().Where<T>(wherelambda);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIdex"></param>
        /// <param name="pageSize"></param>
        /// <param name="toalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc">true:表示升序, false：降序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIdex, int pageSize, out int totalCount, Expression<Func<T, bool>> wherelambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = blogDb.Set<T>().Where<T>(wherelambda);
            totalCount = temp.Count();
            if (isAsc)//升序
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIdex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIdex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            try
            {

                blogDb.Entry<T>(entity).State = EntityState.Deleted;
                return blogDb.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            try
            {
                blogDb.Entry<T>(entity).State = EntityState.Modified;
                return blogDb.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddEntity(T entity)
        {
            try
            {
                blogDb.Set<T>().Add(entity);
                blogDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="wherelambda"></param>
        /// <returns></returns>
        public T GetSingleData(Expression<Func<T, bool>> wherelambda)
        {
            try
            {
                return blogDb.Set<T>().SingleOrDefault(wherelambda);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
