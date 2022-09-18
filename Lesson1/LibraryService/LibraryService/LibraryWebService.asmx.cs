using LibraryService.Models;
using LibraryService.Services;
using LibraryService.Services.Impl;
using System.Linq;
using System.Web.Services;

namespace LibraryService
{
    /// <summary>
    /// Сервис "Библиотеки"
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class LibraryWebService : System.Web.Services.WebService
    {

        #region Services

        private readonly ILibraryRepositoryService _libraryRepositoryService;

        #endregion

        public LibraryWebService()
        {
            _libraryRepositoryService = new LibraryRepository(new LibraryDatabaseContext());
        }

        [WebMethod]
        public Book[] GetBooksByTitle(string title)
        {
            return _libraryRepositoryService.GetByTitle(title).ToArray();
        }

        [WebMethod]
        public Models.Book[] GetBooksByAuthor(string authorName)
        {
            return _libraryRepositoryService.GetByAuthor(authorName).ToArray();
        }

        [WebMethod]
        public Models.Book[] GetBooksByCategory(string category)
        {
            return _libraryRepositoryService.GetByCategory(category).ToArray();
        }


    }
}
