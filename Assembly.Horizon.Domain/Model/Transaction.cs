using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model
{
    public class Transaction : AuditableEntity, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        public Guid RealtorId { get; set; }
        public Realtor Realtor { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Invoice { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionHistory { get; set; }

        protected Transaction()
        {
            Id = Guid.NewGuid();
        }

        public Transaction(
            Guid userId,
            double value,
            DateTime date,
            string description,
            int invoice,
            string paymentMethod,
            string transactionStatus,
            string transactionHistory)
        {
            Id = Guid.NewGuid(); // Gera um novo GUID
            UserId = userId;
            Value = value;
            Date = date;
            Description = description;
            Invoice = invoice;
            PaymentMethod = paymentMethod;
            TransactionStatus = transactionStatus;
            TransactionHistory = transactionHistory;
        }
    }
}
