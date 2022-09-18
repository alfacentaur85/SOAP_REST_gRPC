namespace LibraryService.Web.Models
{
    /// <summary>
    /// Ошибка
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
