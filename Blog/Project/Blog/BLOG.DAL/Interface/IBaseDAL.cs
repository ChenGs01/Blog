using System;
using System.Linq;
using System.Linq.Expressions;


namespace BLOG.DAL
{
    /// <summary>
    /// 封装的公共方法
    /// </summary>
    public interface IBaseDAL<T> 
    {
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> wherelambda);
        IQueryable<T> LoadPageEntities<s>(int pageIdex, int pageSize, out int totalCount, Expression<Func<T, bool>> wherelambda, Expression<Func<T, s>> orderbyLambda, bool isAsc);
        T GetSingleData(Expression<Func<T, bool>> wherelambda);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        bool AddEntity(T entity);
    }
}
