using Application.Common;
using MediatR;
using Application.Interfaces;

namespace Application.UseCases.Commands.DeleteTask;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Response>
{
    private readonly ITaskRepositoryAsync _taskRepository;
    public DeleteTaskCommandHandler(ITaskRepositoryAsync taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<Response> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var taskExists = await _taskRepository.ExistsAsync(request.TaskId, cancellationToken);
        if (!taskExists)
        {
            return Response.Fail($"Task with ID {request.TaskId} does not exist.");
        }
        await _taskRepository.DeleteAsync(request.TaskId);
        await _taskRepository.SaveChangesAsync();
        return Response.Success();
    }
}
