using MediatR;
using Application.DTOs;
using Application.Common;
using Application.Interfaces;
using AutoMapper;

namespace Application.UseCases.Queries.GetTaskById;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Response<TaskDto>>
{
    private readonly ITaskRepositoryAsync _taskRepository;
    private readonly IMapper _mapper;
    public GetTaskByIdQueryHandler(ITaskRepositoryAsync taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<Response<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var taskItem = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (taskItem == null)
        {
            return Response<TaskDto>.Fail($"Task with ID {request.Id} does not exist.");
        }
        var dto = _mapper.Map<TaskDto>(taskItem);
        return Response<TaskDto>.Success(dto);
    }
}
