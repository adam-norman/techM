namespace Dto.ViewModels
{
    public class QueryViewModel<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public T Model { get; set; }
    }
}
