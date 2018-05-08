using IRS.Dal.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IRS.BLL.Implements
{
    public class BaseService<T> where T : class, new()
    {
        private IBaseDal<T> _baseDal;
        public BaseService(IBaseDal<T> baseDal)
        {
            _baseDal = baseDal;
        }

        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return _baseDal.LoadEntities(whereLambda);
        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return _baseDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            _baseDal.DeleteEntity(entity);
            return _baseDal.SaveChanges();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            _baseDal.EditEntity(entity);
            return _baseDal.SaveChanges();
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            _baseDal.AddEntity(entity);
            _baseDal.SaveChanges();
            return entity;
        }
    }
}