using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Transactions.Commands.Create;

public class CreateTransactionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTransactionCommand, Result<CreateTransactionResponse, Success, Error>>
{
    public async Task<Result<CreateTransactionResponse, Success, Error>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var contract = await unitOfWork.ContractRepository.RetrieveAsync(request.ContractId, cancellationToken);
        if (contract == null)
        {
            return Error.NotFound;
        }

        var transaction = new Transaction(
            request.ContractId,
            Guid.Empty,
            request.UserId,
            request.Amount,
            DateTime.UtcNow,
            request.Description,
            request.PaymentMethod,
            Transaction.TransactionStatus.Completed
        );

        var invoice = transaction.GenerateInvoice();
        transaction.InvoiceId = invoice.Id;

        await unitOfWork.TransactionRepository.AddAsync(transaction, cancellationToken);
        await unitOfWork.InvoiceRepository.AddAsync(invoice, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateTransactionResponse
        {
            Id = transaction.Id,
            ContractId = transaction.ContractId,
            InvoiceId = transaction.InvoiceId,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
            PaymentMethod = transaction.PaymentMethod,
            Status = transaction.Status
        };

        return response;
    }
}
