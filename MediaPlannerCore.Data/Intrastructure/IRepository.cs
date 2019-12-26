using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlannerCore.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Delete(T entityToDelete);
        void Delete(object id);
        IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            //Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T GetByID(object id);
        //IEnumerable<T> GetWithRawSql(string query,
        //    params object[] parameters);
        void Insert(T entity);
        void Update(T entityToUpdate);
    }
}
