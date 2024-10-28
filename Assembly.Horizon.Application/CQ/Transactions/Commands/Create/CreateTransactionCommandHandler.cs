using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Transactions.Commands.Create;

public class CreateTransactionCommandHandler(
    IUnitOfWork unitOfWork,
    INotificationStrategy notificationStrategy)
    : IRequestHandler<CreateTransactionCommand, Result<CreateTransactionResponse, Success, Error>>
{
    public async Task<Result<CreateTransactionResponse, Success, Error>> Handle(
      CreateTransactionCommand request,
      CancellationToken cancellationToken)
    {
            var contract = await unitOfWork.ContractRepository.RetrieveAsync(request.ContractId, cancellationToken);
            if (contract == null)
            {
                return Error.NotFound;
            }

            var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(contract.RealtorId, cancellationToken);
            if (realtor == null)
            {
                return Error.NotFound;
            }

            // Verificar se o customer existe
            var customer = await unitOfWork.CustomerRepository.RetrieveByUserIdAsync(request.UserId, cancellationToken);
            if (customer == null)
            {
                return Error.NotFound;
            }

            var transaction = new Transaction(
                request.ContractId,
                Guid.Empty,
                customer.UserId,
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

            var notification = new Notification(
                customer.UserId,  // UserId do customer que está pagando
                realtor.UserId,  // UserId do realtor associado ao contrato
                $"New payment received: ${transaction.Amount} for contract #{contract.Id}",
                NotificationType.Payment,
                NotificationPriority.High,
                transaction.Id,
                "Transaction"
            );

            await notificationStrategy.StorePersistentNotification(notification);
            await unitOfWork.NotificationRepository.AddAsync(notification);
            await unitOfWork.CommitAsync(cancellationToken);

            return new CreateTransactionResponse
            {
                TransactionNumber = transaction.TransactionNumber,
                Id = transaction.Id,
                ContractId = transaction.ContractId,
                InvoiceId = transaction.InvoiceId,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Description = transaction.Description,
                PaymentMethod = transaction.PaymentMethod,
                Status = transaction.Status
            };
        }

    }