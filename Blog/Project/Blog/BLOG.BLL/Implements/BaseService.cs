using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLOG.DAL;
using BLOG.Models;

namespace BLOG.BLL
{
    public abstract class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private readonly IBaseDAL<T> _ibaseDal;
        //注入DAL服务
        public BaseService(IBaseDAL<T> ibaseDal)
        {
            _ibaseDal = ibaseDal;
        }
       
        //普通查询
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return _ibaseDal.LoadEntities(whereLambda);
        }
        // 加载分页列表
        public IQueryable<T> LoadPageEntities<s>(int pageIdex, int pageSize, out int toalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return _ibaseDal.LoadPageEntities<s>(pageIdex, pageSize, out toalCount, whereLambda, orderbyLambda, isAsc);
        }
        // 单个删除
        public bool DeleteEntity(T entity)
        {
            return _ibaseDal.DeleteEntity(entity);
        }
        // 单个修改
        public bool EditEntity(T entity)
        {
            return _ibaseDal.EditEntity(entity);
        }
        // 单个增加
        public bool AddEntity(T entity)
        {
            return _ibaseDal.AddEntity(entity);
        }
        public T GetSingleData(Expression<Func<T, bool>> wherelambda)
        {
            return _ibaseDal.GetSingleData(wherelambda);
        }
        //分页
        public IQueryable<T> LoadNormalEntities(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            try
            {
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                MethodCallExpression resultExp = null;
                IQueryable<T> tempData = _ibaseDal.LoadEntities(predicate);
                foreach (string item in _order)
                {
                    string _orderPart = item;
                    _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                    string[] _orderArry = _orderPart.Split(' ');
                    string _orderField = _orderArry[0];
                    bool sort = isAsc;
                    if (_orderArry.Length == 2)
                    {
                        isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                    }
                    var parameter = Expression.Parameter(typeof(T), "t");
                    var property = typeof(T).GetProperty(_orderField);
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                }
                tempData = tempData.Provider.CreateQuery<T>(resultExp);
                List<T> list = new List<T>();
                //由于泛型在进行分页时feth等操作导致报错 对对象进行强转处理
                foreach (var item in tempData)
                {
                    list.Add((T)item);
                }
                pagination.records = tempData.Count();
                tempData = list.Skip<T>(pagination.rows * (pagination.page - 1)).Take<T>(pagination.rows).AsQueryable();
                return tempData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
