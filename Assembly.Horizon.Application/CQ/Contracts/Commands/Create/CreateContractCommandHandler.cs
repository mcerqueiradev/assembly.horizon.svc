using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assembly.Horizon.Application.CQ.Contracts.Commands.Create;

public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, Result<CreateContractResponse, Success, Error>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IPdfGenerationService pdfGenerationService;

    public CreateContractCommandHandler(IUnitOfWork unitOfWork, IPdfGenerationService pdfGenerationService)
    {
        this.unitOfWork = unitOfWork;
        this.pdfGenerationService = pdfGenerationService;
    }

    public async Task<Result<CreateContractResponse, Success, Error>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        // Carrega a propriedade existente
        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.PropertyId, cancellationToken);
        if (property == null)
        {
            return Error.NotFound; // Retorna erro se a propriedade não for encontrada
        }

        // Carrega o cliente existente baseado no UserId
        var customer = await unitOfWork.CustomerRepository.RetrieveByUserIdAsync(request.CustomerId, cancellationToken);
        if (customer == null)
        {
            return Error.NotFound; // Retorna erro se o cliente não for encontrado
        }

        // Carrega o corretor existente baseado no UserId
        var realtor = await unitOfWork.RealtorRepository.RetrieveByUserIdAsync(request.RealtorId, cancellationToken);
        if (realtor == null)
        {
            return Error.NotFound; // Retorna erro se o corretor não for encontrado
        }

        // Criação do contrato sem tentar inserir entidades existentes
        var contract = new Contract
        {
            Id = Guid.NewGuid(),
            PropertyId = property.Id, // Referencia o Id da propriedade existente
            CustomerId = customer.Id, // Referencia o Id do cliente existente
            RealtorId = realtor.Id, // Referencia o Id do corretor existente
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Value = request.Value,
            AdditionalFees = request.AdditionalFees,
            PaymentFrequency = request.PaymentFrequency,
            RenewalOption = request.RenewalOption,
            IsActive = true,
            LastActiveDate = DateTime.UtcNow,
            ContractType = request.ContractType,
            Status = request.Status,
            SignatureDate = request.SignatureDate,
            SecurityDeposit = request.SecurityDeposit,
            InsuranceDetails = request.InsuranceDetails,
            Notes = request.Notes,
            TemplateVersion = request.TemplateVersion,
            DocumentPath = string.Empty // Pode ser preenchido posteriormente
        };

        // Adiciona o novo contrato ao repositório
        await unitOfWork.ContractRepository.AddAsync(contract, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        // Geração do PDF
        string pdfPath = await pdfGenerationService.GenerateContractPdfAsync(contract, customer, realtor, property);

        // Atualiza o caminho do documento no contrato
        contract.DocumentPath = pdfPath;
        await unitOfWork.ContractRepository.UpdateAsync(contract, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        // Cria a resposta
        var response = new CreateContractResponse
        {
            Id = contract.Id,
            PropertyId = contract.PropertyId,
            CustomerId = contract.CustomerId,
            RealtorId = contract.RealtorId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Value = contract.Value,
            IsActive = contract.IsActive,
            ContractType = contract.ContractType,
            Status = contract.Status,
            SignatureDate = contract.SignatureDate,
            DocumentPath = contract.DocumentPath
        };

        return response;
    }

}
