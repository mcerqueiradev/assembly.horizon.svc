using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Horizon.Application.CQ.Transactions.Commands.Payment.Completed;

public record CompletedTransactionCommand(Guid TransactionId, string PaymentMethod) : IRequest<Result<CompletedTransactionResponse, Success, Error>>;

public record CompletedTransactionResponse
{
    public Guid Id { get; init; }
    public string Status { get; init; }
    public string PaymentMethod { get; init; }
}

public class CompletedTransactionCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy) : IRequestHandler<CompletedTransactionCommand, Result<CompletedTransactionResponse, Success, Error>>
{
    public async Task<Result<CompletedTransactionResponse, Success, Error>> Handle(
        CompletedTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await unitOfWork.TransactionRepository.RetrieveAsync(request.TransactionId, cancellationToken);
        if (transaction == null)
        {
            return Error.NotFound;
        }

        var contract = await unitOfWork.ContractRepository.RetrieveAsync(transaction.ContractId, cancellationToken);
        if (contract == null)
        {
            return Error.NotFound;
        }

        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(contract.RealtorId, cancellationToken);
        if (realtor == null)
        {
            return Error.NotFound;
        }

        transaction.Status = Transaction.TransactionStatus.Completed;
        transaction.PaymentMethod = request.PaymentMethod;

        var notification = new Notification(
            transaction.UserId,
            realtor.UserId,
            $"Payment processed: ${transaction.Amount} for contract #{contract.Id}",
            NotificationType.Payment,
            NotificationPriority.High,
            transaction.Id,
            "Transaction"
        );

        await notificationStrategy.StorePersistentNotification(notification);
        await unitOfWork.NotificationRepository.AddAsync(notification);
        await unitOfWork.CommitAsync(cancellationToken);

        return new CompletedTransactionResponse
        {
            Id = transaction.Id,
            Status = transaction.Status.ToString(),
            PaymentMethod = transaction.PaymentMethod
        };
    }
}