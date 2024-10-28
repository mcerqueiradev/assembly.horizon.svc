using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Invoices.Queries.Retrieve;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(ISender sender) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(InvoiceResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetInvoice(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveInvoiceQuery(id);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }
}