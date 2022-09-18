using System.Collections.Generic;

namespace LibraryService.Services
{
    /// <summary>
    /// Интерфейс IRepository<T, TId>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IRepository<T, TId>
    {
        TId Add(T item);

        int Update(T item);

        int Delete(T item);

        IList<T> GetAll();

        T GetById(TId id);
    }
}
