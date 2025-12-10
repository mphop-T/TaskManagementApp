using MediatR;
using Application.DTOs;
using Application.Common;

namespace Application.UseCases.Commands.DeleteTask;

public record DeleteTaskCommand(Guid TaskId) : IRequest<Response>
{
}
