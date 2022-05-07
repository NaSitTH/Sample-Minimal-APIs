using ContosoUniversity.Data;
using ContosoUniversityApi.Filter;
using ContosoUniversityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversityApi.Repositories;

public class StudentsRepository : IRepository<Student>
{
    private readonly SchoolContext _context;
    public StudentsRepository(SchoolContext context)
    {
        _context = context;
    }

    public async Task Add(Student entity)
    {
        _context.Students.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Student>> GetAll()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetById(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task<List<Student>> GetByPageFilter(PaginationFilter filter)
    {
        return await _context.Students
           .Skip((filter.PageNumber - 1) * filter.PageSize)
           .Take(filter.PageSize)
           .ToListAsync();
    }

    public async Task<Student> GetDetail(int id)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> GetTotalRecord()
    {
        return await _context.Students.CountAsync();
    }

    public async Task<bool> Remove(int id)
    {
        if (await _context.Students.FindAsync(id) is Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<Student> Update(Student entity, int id)
    {
        if (await _context.Students.FindAsync(id) is Student student)
        {
            student.LastName = entity.LastName;
            student.FirstMidName = entity.FirstMidName;

            await _context.SaveChangesAsync();
            return student;
        }

        return null;
    }
}