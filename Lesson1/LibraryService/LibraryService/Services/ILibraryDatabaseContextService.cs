using LibraryService.Models;
using System.Collections.Generic;


namespace LibraryService.Services
{
    /// <summary>
    /// Интерфейс ILibraryDatabaseContextService
    /// </summary>
    public interface ILibraryDatabaseContextService
    {
        IList<Book> Books { get; }
    }
}
