using MediatR;
using Application.DTOs;
using Application.Common;
using Application.Interfaces;
using AutoMapper;

namespace Application.UseCases.Queries.GetTasks;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, Response<IEnumerable<TaskDto>>>
{
    private readonly ITaskRepositoryAsync _taskRepository;
    private readonly IMapper _mapper;

    public GetTasksQueryHandler(ITaskRepositoryAsync taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<Response<IEnumerable<TaskDto>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks =  _taskRepository.GetListAsync(cancellationToken);
        var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks);
        return Response<IEnumerable<TaskDto>>.Success(taskDtos, "Tasks retrieved successfully.");
    }
}
