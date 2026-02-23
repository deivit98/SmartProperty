using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartProperty.Application.Features.SellerFeatures.CreateSeller;
using SmartProperty.Application.Features.SellerFeatures.DeleteSellerById;
using SmartProperty.Application.Features.SellerFeatures.GetSellerByEmail;
using SmartProperty.Application.Features.SellerFeatures.GetSellerById;
using SmartProperty.Application.Features.SellerFeatures.UpdateSellerContact;
using SmartProperty.Result;

namespace SmartProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SellerController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSellerRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSellerByIdRequest(id);
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email, CancellationToken cancellationToken)
    {
        var request = new GetSellerByEmailRequest(email);
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSellerByIdRequest(id);
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPut("{id:guid}/contact")]
    public async Task<IActionResult> UpdateContact(Guid id, [FromBody] UpdateSellerContactBody body, CancellationToken cancellationToken)
    {
        var request = new UpdateSellerContactRequest(id, body);
        var result = await mediator.Send(request, cancellationToken);
        return result.ToActionResult(this);
    }
}
