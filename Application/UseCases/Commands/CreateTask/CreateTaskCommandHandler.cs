using MediatR;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Response<TaskDto>>
{
    private readonly ITaskRepositoryAsync _taskRepository;
    private readonly IMapper _mapper;
    public CreateTaskCommandHandler(ITaskRepositoryAsync taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<Response<TaskDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.TaskDto.Title))
        {
            return Response<TaskDto>.Fail("Task title cannot be empty.");
        }
        if(request.TaskDto.DueDate.HasValue && request.TaskDto.DueDate.Value < DateTime.UtcNow)
        {
            return Response<TaskDto>.Fail("Due date cannot be in the past.");
        }
        if(request.TaskDto.DueDate == default)
        {
            return Response<TaskDto>.Fail("Task must have a due date");
        }
        var taskEntity = _mapper.Map<TaskItem>(request.TaskDto);
        taskEntity.Id = Guid.NewGuid();
        if(await _taskRepository.ExistsAsync(taskEntity.Id))
        {
            return Response<TaskDto>.Fail("A task with the same ID already exists.");
        }
        taskEntity.CreatedAt = DateTime.UtcNow;
        taskEntity.Status = TaskStatusItem.Pending;
        await _taskRepository.AddAsync(taskEntity);
        await _taskRepository.SaveChangesAsync();

        return Response<TaskDto>.Success(_mapper.Map<TaskDto>(taskEntity), "Task created successfully.");
    }
}
