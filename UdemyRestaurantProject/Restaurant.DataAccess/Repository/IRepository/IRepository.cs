using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        //Get Add, Get by Id, Add, Remove, Removerange

        void Add(T entity); 
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        IEnumerable<T> GetAll(string? includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T,bool>>? filter=null);
    }
    
}
