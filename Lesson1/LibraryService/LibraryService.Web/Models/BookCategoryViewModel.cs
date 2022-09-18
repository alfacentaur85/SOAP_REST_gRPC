using LibraryServiceReference;

namespace LibraryService.Web.Models
{
    /// <summary>
    /// Модель данных для отображения на клиенте
    /// </summary>
    public class BookCategoryViewModel
    {
        public Book[] Books { get; set; }

        public SearchType SearchType { get; set; }

        public string? SearchString { get; set; }
    }
}
