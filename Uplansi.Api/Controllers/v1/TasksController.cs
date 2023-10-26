using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplansi.Core.Contracts.Services;
using Uplansi.Core.DTOs;

namespace Uplansi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
// [Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    /* [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAll();

        return StatusCode(StatusCodes.Status200OK, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetById(id);

        return StatusCode(StatusCodes.Status200OK, result);
    } */

    [HttpPost]
    public async Task<IActionResult> Create(TaskAddOrUpdate item)
    {
        var result = await _service.Add(item);

        return StatusCode(StatusCodes.Status200OK, result);
    }

    /* [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, RiskCreateOrUpdateDto item)
    {
        var result = await _service.Update(id, item);

        return StatusCode(StatusCodes.Status200OK, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var result = await _service.Remove(id);

        return StatusCode(StatusCodes.Status200OK, result);
    }
    
    [HttpPost]
    [Route("add-range")]
    public async Task<IActionResult> AddRange(IEnumerable<RiskCreateOrUpdateDto> items)
    {
        var result = await _service.AddRange(items);

        return StatusCode(StatusCodes.Status200OK, result);
    } */
}