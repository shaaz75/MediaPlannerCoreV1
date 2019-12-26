using Microsoft.EntityFrameworkCore;
using MediaPlannerCore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MediaPlannerCore.Data.Infrastructure
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        internal MediaPlannerCoreContext context;
        internal DbSet<T> dbSet;

        public BaseRepository(MediaPlannerCoreContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            //Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            //if (orderBy != null)
            //{
            //    return orderBy(query);
            //}
            //else
            //{
                return query;
            //}
        }

        public virtual T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }

        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
