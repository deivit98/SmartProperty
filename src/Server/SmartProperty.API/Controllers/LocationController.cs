using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartProperty.Application.Features.GetLocationById;
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
}
