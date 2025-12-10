using Application.DTOs;
using MediatR;
using Application.Common;

namespace Application.UseCases.Commands.CreateTask;

public record CreateTaskCommand(CreateTaskDto TaskDto) : IRequest<Response<TaskDto>>
{
}
