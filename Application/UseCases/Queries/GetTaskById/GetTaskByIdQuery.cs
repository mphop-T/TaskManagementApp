using MediatR;
using Application.Common;
using Application.DTOs;

namespace Application.UseCases.Queries.GetTaskById
{
    public record GetTaskByIdQuery(Guid Id):IRequest<Response<TaskDto>>
    {
    }
}
