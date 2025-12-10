using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class TaskRepositoryAsync : ITaskRepositoryAsync
{
    private readonly TaskDbContext _context;
    public TaskRepositoryAsync(TaskDbContext taskDbContext)
    {
        _context = taskDbContext;
    }
    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        _ = await _context.TaskItems.AddAsync(task);
        return task;
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task is not null)
        {
            _ = _context.TaskItems.Remove(task);
        }
        else
        {
            throw new KeyNotFoundException($"TaskItem with id {id} not found");
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        bool exists = await _context.TaskItems.AnyAsync(x => x.Id == id, cancellationToken);
        return exists;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var taskItem = await _context.TaskItems.FindAsync(new object[] { id }, cancellationToken);
        return taskItem;
    }

    public async Task<IEnumerable<TaskItem>> GetListAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<TaskItem> taskItems = await _context.TaskItems.ToListAsync(cancellationToken);
        return taskItems;
    }

    public async Task SaveChangesAsync()
    {
        _ = await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _ = _context.TaskItems.Update(task);
        await Task.CompletedTask; 
    }
}
