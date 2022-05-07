using ContosoUniversityApi.Filter;
using ContosoUniversityApi.Helpers;
using ContosoUniversityApi.Models;
using ContosoUniversityApi.Repositories;
using ContosoUniversityApi.Wrapper;

namespace ContosoUniversityApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepo;
        private readonly IUriService _uriService;

        public StudentService(IRepository<Student> studentRepo, IUriService uriService)
        {
            _studentRepo = studentRepo;
            _uriService = uriService;
        }
        public async Task Add(Student entity)
        {
            await _studentRepo.Add(entity);
        }

        public async Task<Response<List<Student>>> GetAll()
        {
            if (await _studentRepo.GetAll() is List<Student> student)
            {
                return new Response<List<Student>>(student);
            }

            return new Response<List<Student>>(isError: true, new string[] { "Not Found" });
        }

        public async Task<Response<Student>> GetById(int id)
        {
            if (await _studentRepo.GetById(id) is Student student)
            {
                return new Response<Student>(student);
            }

            return new Response<Student>(isError: true, new string[] { "Not Found" });
        }

        public async Task<Response<Student>> GetDetail(int id)
        {
            if (await _studentRepo.GetDetail(id) is Student student)
            {
                return new Response<Student>(student);
            }

            return new Response<Student>(isError: true, new string[] { "Not Found" });
        }

        public async Task<PagedResponse<List<Student>>> GetByPageFilter(HttpRequest request)
        {
            var route = request.Path.Value;
            var validFilter = new PaginationFilter(request.Query["pageNumber"], request.Query["pageSize"]);
            var totalRecords = await _studentRepo.GetTotalRecord();
            var pagedData = await _studentRepo.GetByPageFilter(validFilter);
            return PaginationHelper.CreatePagedReponse<Student>(pagedData,
                                                                validFilter,
                                                                totalRecords,
                                                                _uriService,
                                                                route);
        }

        public async Task<Response<Student>> Remove(int id)
        {
            if (await _studentRepo.Remove(id))
            {
                return new Response<Student>(null);
            }

            return new Response<Student>(isError: true, new string[] { "Not Found" });
        }

        public async Task<Response<Student>> Update(Student entity, int id)
        {
            if (await _studentRepo.Update(entity, id) is Student student)
            {
                return new Response<Student>(student);
            }

            return new Response<Student>(isError: true, new string[] { "Not Found" });
        }
    }
}