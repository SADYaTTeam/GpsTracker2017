using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GpsTracker.Models.DataContext.Interfaces
{
    public interface IDbContext<T, U>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBy(Expression<Func<U, bool>> expression);
        bool Insert(T newItem);
        bool Delete(int id);
        bool Update(int id, T newItem);
    }
}
