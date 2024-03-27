using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;

namespace WebBanSachRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetbyId(Guid id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(Guid id);
        bool softDelete(Guid id);
        T FindOne(Expression<Func<T, bool>> predicate);
    }
}
