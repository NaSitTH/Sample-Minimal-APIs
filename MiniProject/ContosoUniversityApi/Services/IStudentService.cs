using ContosoUniversityApi.Models;
using ContosoUniversityApi.Wrapper;

namespace ContosoUniversityApi.Services
{
    public interface IStudentService
    {
        Task Add(Student entity);
        Task<Response<List<Student>>> GetAll();
        Task<Response<Student>> GetById(int id);
        Task<Response<Student>> GetDetail(int id);
        Task<PagedResponse<List<Student>>> GetByPageFilter(HttpRequest request);
        Task<Response<Student>> Remove(int id);
        Task<Response<Student>> Update(Student entity, int id);
    }
}