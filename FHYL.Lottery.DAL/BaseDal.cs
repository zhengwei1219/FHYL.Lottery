using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace FHYL.Lottery.DAL
{
   public class BaseDal<T> where T:class,new()
    {
       // ExtractOilEntities Db = new ExtractOilEntities();
       public DbContext Db {
           get { return DbContextFactory.CreateDbContext(); }
       }
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = Db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

        public bool DeleteEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.EntityState.Deleted;
           return Db.SaveChanges() > 0;
            //return true;
        }

        public bool UpdateEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.EntityState.Modified;
           return Db.SaveChanges() > 0;
            //return true;
        }

        public T AddEntity(T entity)
        {
            Db.Set<T>().Add(entity);
           Db.SaveChanges();
            return entity;
        }
        public void UpdateEntityNoCommit(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.EntityState.Modified;
            
            //return true;
        }
        public void AddEntityNoCommit(T entity)
        {
            Db.Set<T>().Add(entity);

            //return true;
        }

    }
}
