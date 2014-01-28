using System.Collections.Generic;

namespace OpenCBS.Web.Interface.Repository
{
    public interface IRepository<T>
    {
        IList<T> FindAll();
        T FindById(int id);
        void Update(T entity);
        int Add(T entity);
        void Remove(int id);
    }
}
