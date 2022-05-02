using ContosoUniversityApi.Filter;

namespace ContosoUniversityApi.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}