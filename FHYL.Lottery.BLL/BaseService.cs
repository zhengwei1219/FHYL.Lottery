using FHYL.Lottery.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FHYL.Lottery.BLL
{

    public class BaseService<T> where T : class,new()
    {
        static private BaseDal<T> dao = new BaseDal<T>();
        #region
        internal BaseService()
        {
            dao = new BaseDal<T>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="dao">The DAO.</param>
        internal BaseService(BaseDal<T> theDao)
        {
            dao = theDao;
        }
        #endregion
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return dao.LoadEntities(whereLambda);
        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return dao.LoadPageEntities(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        public bool DeleteEntity(T entity)
        {
            return dao.DeleteEntity(entity);
        }
        public bool UpdateEntity(T entity)
        {
            return dao.UpdateEntity(entity);
        }
        public T AddEntity(T entity)
        {
            return dao.AddEntity(entity);
        }
        public void UpdateEntityNoCommit(T entity)
        {
            dao.UpdateEntityNoCommit(entity);
        }
        public void AddEntityNoCommit(T entity)
        {
            dao.AddEntityNoCommit(entity);
        }
    }
}
