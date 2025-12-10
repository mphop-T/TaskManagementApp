using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistance;

public class TaskDbContext:DbContext
{
    public TaskDbContext(DbContextOptions options):base(options)
    {
        
    }
    public DbSet<TaskItem> TaskItems { get; set; }
}
