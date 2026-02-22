using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartProperty.Application.Features.LocationFeatures.CreateLocation;
using SmartProperty.Application.Features.LocationFeatures.DeleteLocation;
using SmartProperty.Application.Features.LocationFeatures.GetLocationById;
using SmartProperty.Application.Features.LocationFeatures.UpdateLocation;
using SmartProperty.Result;

namespace SmartProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetLocationByIdRequest(id);
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateLocationRequest body, CancellationToken cancellationToken)
    {
        var request = new UpdateLocationRequest(id, body);
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteLocationRequest(id);
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }
}
