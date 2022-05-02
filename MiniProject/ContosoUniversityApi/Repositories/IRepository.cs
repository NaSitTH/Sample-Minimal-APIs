using ContosoUniversityApi.Wrapper;

namespace ContosoUniversityApi.Repositories;

public interface IRepository<T> where T : class
{
    Task Add(T entity);
    Task<PagedResponse<List<T>>> GetPaged(HttpRequest request);
    Task<IEnumerable<T>> GetAll();
    Task<Response<T>> GetById(int id);
    Task<Response<T>> GetDetail(int id);
    Task<Response<T>> Remove(int id);
    Task<Response<T>> Update(T entity, int id);

}