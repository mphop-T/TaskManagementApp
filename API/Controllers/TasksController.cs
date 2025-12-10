using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.UseCases.Commands.DeleteTask;
using Application.DTOs;
using Application.UseCases.Commands.CreateTask;
using Application.UseCases.Commands.UpdateTask;
using Application.Common;
using Domain.Entities;
using Application.UseCases.Queries.GetTaskById;
using Application.UseCases.Queries.GetTasks;

namespace API.Controllers;

public class TasksController : Controller
{
    private readonly IMediator _mediator;
    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
        var result = await _mediator.Send(new CreateTaskCommand(dto));
        
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto dto)
    {
        var result = await _mediator.Send(new UpdateTaskCommand(id, dto));
        
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var result = await _mediator.Send(new DeleteTaskCommand(id));
        
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest(result.Message);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(id));
        
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return NotFound(result.Message);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var result = await _mediator.Send(new GetTasksQuery());
        
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
}