using Domain.Entities;

namespace Application.Interfaces;

public interface ITaskRepositoryAsync
{
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetListAsync(CancellationToken cancellationToken = default);
    Task<TaskItem> AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync();
}
