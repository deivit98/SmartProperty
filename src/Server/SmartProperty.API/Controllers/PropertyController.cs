using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartProperty.Application.Features.PropertyFeatures.CreateProperty;
using SmartProperty.Application.Features.PropertyFeatures.DeleteProperty;
using SmartProperty.Application.Features.PropertyFeatures.GetPropertyById;
using SmartProperty.Result;

namespace SmartProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetPropertyByIdRequest(id);
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeletePropertyRequest(id);
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }
}
