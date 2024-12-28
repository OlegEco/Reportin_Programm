using Reportin_Programm.Models;
using System.Linq.Expressions;

namespace Reportin_Programm
{
    public interface IGenericRepository<T> where T: BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Update(T item);
        Task<T> Delete(Guid id);
        Task<Guid> Add(T item);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
    }
}
