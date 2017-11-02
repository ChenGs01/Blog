using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using BLOG.Models;
using BLOG.DAL;

namespace BLOG.BLL
{
    public interface IBaseService<T> where T : class, new()
    {
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<s>(int pageIdex, int pageSize, out int toalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc);
        IQueryable<T> LoadNormalEntities(Expression<Func<T, bool>> predicate, Pagination pagination);
        T GetSingleData(Expression<Func<T, bool>> wherelambda);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        bool AddEntity(T entity);
    }
}
