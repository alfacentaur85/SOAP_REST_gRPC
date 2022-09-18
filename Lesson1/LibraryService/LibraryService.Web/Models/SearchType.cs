using System.ComponentModel.DataAnnotations;

namespace LibraryService.Web.Models
{
    /// <summary>
    /// Критерии поиска
    /// </summary>
    public enum SearchType
    {
        [Display(Name = "Заголовок")]
        Title,
        [Display(Name = "Автор")]
        Author,
        [Display(Name = "Категория")]
        Category
    }
}
