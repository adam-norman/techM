using RequestApp.Filter;

namespace RequestApp.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
