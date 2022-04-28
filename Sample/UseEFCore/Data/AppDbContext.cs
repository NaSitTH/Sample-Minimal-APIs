using Microsoft.EntityFrameworkCore;
using UseEFCore.Models;

namespace UseEFCore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Person => Set<Person>();
}