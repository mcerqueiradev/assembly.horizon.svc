using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Contracts.Commands.Create;
using Assembly.Horizon.Application.CQ.Contracts.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Contracts.Queries.RetrieveAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractController(ISender sender) : Controller
{
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateContractResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateContract([FromForm] CreateContractCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveContractQuery { Id = id };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            // Aqui você pode construir a URL para o documento
            var contractResponse = result.Value;
            contractResponse.DocumentPath = $"{Request.Scheme}://{Request.Host}/api/contract/Outputs/Contracts/{Path.GetFileName(contractResponse.DocumentPath)}";

            return Ok(contractResponse);
        }

        return NotFound(result.Error);
    }


    [HttpGet("RetrieveAll")]
    public async Task<IActionResult> RetrieveAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveAllContractsQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            // Supondo que RetrieveAllContractsResponse contém uma lista de contratos
            var contracts = result.Value.Contracts; // Altere isso com o nome real da lista de contratos

            // Para cada contrato na lista, adicione a URL do documento
            foreach (var contract in contracts)
            {
                contract.DocumentPath = $"{Request.Scheme}://{Request.Host}/api/contract/Outputs/Contracts/{Path.GetFileName(contract.DocumentPath)}";
            }

            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }


    [HttpGet("Outputs/Contracts/{filename}")]
    public IActionResult GetContractDocument(string filename)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Outputs", "Contracts", filename);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(); // Retorna 404 se o arquivo não for encontrado
        }

        return PhysicalFile(filePath, "application/pdf", filename); // Retorna o arquivo PDF
    }
}
