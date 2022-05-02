using ContosoUniversity.Data;
using ContosoUniversityApi.Filter;
using ContosoUniversityApi.Helpers;
using ContosoUniversityApi.Models;
using ContosoUniversityApi.Services;
using ContosoUniversityApi.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversityApi.Repositories;

public class StudentsRepository : IRepository<Student>
{
    private readonly SchoolContext _context;
    private readonly IUriService _uriService;

    public StudentsRepository(SchoolContext context, IUriService uriService)
    {
        _context = context;
        _uriService = uriService;
    }

    public async Task Add(Student entity)
    {
        _context.Students.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<PagedResponse<List<Student>>> GetPaged(HttpRequest request)
    {
        var route = request.Path.Value;
        var validFilter = new PaginationFilter(request.Query["pageNumber"], request.Query["pageSize"]);

        var pagedData = await _context.Students
           .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
           .Take(validFilter.PageSize)
           .ToListAsync();

        var totalRecords = await _context.Students.CountAsync();
        var pagedReponse = PaginationHelper.CreatePagedReponse<Student>(pagedData,
                                                                        validFilter,
                                                                        totalRecords,
                                                                        _uriService,
                                                                        route);

        return pagedReponse;
    }

    public async Task<Response<Student>> GetById(int id)
    {
        return await _context.Students.FindAsync(id) is Student student
        ? new Response<Student>(student)
        : new Response<Student>(isError: true, new string[] { "Not Found" });
    }

    public async Task<Response<Student>> GetDetail(int id)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id) is Student student
            ? new Response<Student>(student)
            : new Response<Student>(isError: true, new string[] { "Not Found" });
    }

    public async Task<Response<Student>> Remove(int id)
    {
        if (await _context.Students.FindAsync(id) is Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return new Response<Student>(student);
        }

        return new Response<Student>(isError: true, new string[] { "Not Found" });
    }

    public async Task<Response<Student>> Update(Student entity, int id)
    {
        if (await _context.Students.FindAsync(id) is Student student)
        {
            student.LastName = entity.LastName;
            student.FirstMidName = entity.FirstMidName;

            await _context.SaveChangesAsync();
            return new Response<Student>(student);
        }

        return new Response<Student>(isError: true, new string[] { "Not Found" });
    }
}