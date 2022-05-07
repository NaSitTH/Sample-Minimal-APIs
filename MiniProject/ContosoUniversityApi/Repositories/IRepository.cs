using ContosoUniversityApi.Filter;

namespace ContosoUniversityApi.Repositories;

public interface IRepository<T> where T : class
{
    Task Add(T entity);
    Task<List<T>> GetAll();
    Task<T> GetById(int id);
    Task<List<T>> GetByPageFilter(PaginationFilter filter);
    Task<T> GetDetail(int id);
    Task<int> GetTotalRecord();
    Task<bool> Remove(int id);
    Task<T> Update(T entity, int id);

}