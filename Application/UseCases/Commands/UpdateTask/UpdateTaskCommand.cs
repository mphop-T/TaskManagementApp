using Application.Common;
using MediatR;
using Application.DTOs;
namespace Application.UseCases.Commands.UpdateTask
{
    public record UpdateTaskCommand(Guid Id, UpdateTaskDto updateTaskDto):IRequest<Response<TaskDto>>
    {
    }
}
