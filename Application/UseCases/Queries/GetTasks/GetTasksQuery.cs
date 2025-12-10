using MediatR;
using Application.Common;
using Application.DTOs;

namespace Application.UseCases.Queries.GetTasks;

public record GetTasksQuery: IRequest<Response<IEnumerable<TaskDto>>>
{
}
