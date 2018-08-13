using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        Task<TEntity> GetById(int id);
        bool IsExist();
        bool IsExist(string name);
        Task Insert(TEntity item);
        Task Delete(TEntity item);
        Task Delete(int id);
        Task Update(TEntity item);
        Task Save();
    }
}
