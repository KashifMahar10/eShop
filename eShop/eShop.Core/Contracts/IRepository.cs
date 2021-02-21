using System.Linq;
using eShop.Core.Models;

namespace eShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();

        void Commit();

        void Delete(string id);

        T Find(string id);

        void Insert(T t);

        void Update(T t);
    }
}