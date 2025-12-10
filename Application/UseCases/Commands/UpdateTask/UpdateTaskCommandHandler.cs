using MediatR;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;

namespace Application.UseCases.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Response<TaskDto>>
{
    private readonly ITaskRepositoryAsync _taskRepository;
    private readonly IMapper _mapper;
    public UpdateTaskCommandHandler(ITaskRepositoryAsync taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<Response<TaskDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (taskItem == null)
        {
            return Response<TaskDto>.Fail($"Task with ID {request.Id} does not exist.");
        }
        _mapper.Map(request.updateTaskDto, taskItem);
        await _taskRepository.UpdateAsync(taskItem);
        await _taskRepository.SaveChangesAsync();

        return Response<TaskDto>.Success(_mapper.Map<TaskDto>(taskItem), "Task updated successfully.");
    }
}
