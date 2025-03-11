using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class PropertyProposal : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public string ProposalNumber { get; private set; }
    public decimal ProposedValue { get; set; }
    public ProposalType Type { get; set; }
    public ProposalStatus Status { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime IntendedMoveDate { get; set; }

    // Relacionamentos
    public virtual Property Property { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<ProposalNegotiation> Negotiations { get; set; }
    public virtual ICollection<ProposalDocument> Documents { get; set; }

    public PropertyProposal(
         Guid propertyId,
         Guid userId,
         decimal proposedValue,
         ProposalType type,
         string paymentMethod,
         DateTime intendedMoveDate)
    {
        Id = Guid.NewGuid();
        PropertyId = propertyId;
        UserId = userId;
        ProposalNumber = GenerateProposalNumber();
        ProposedValue = proposedValue;
        Type = type;
        PaymentMethod = paymentMethod;
        IntendedMoveDate = intendedMoveDate;
        Status = ProposalStatus.Pending;

        Negotiations = new HashSet<ProposalNegotiation>();
        Documents = new HashSet<ProposalDocument>();
    }

    private string GenerateProposalNumber()
    {
        var year = DateTime.UtcNow.Year;
        var randomNumber = Random.Shared.Next(100, 999).ToString();
        var randomSuffix = Guid.NewGuid().ToString().Substring(0, 3).ToUpper();

        return $"PROP-{year}-{randomNumber}-{randomSuffix}";
    }

    public void SubmitProposal()
    {
        Status = ProposalStatus.Pending;
    }

    public void StartAnalysis()
    {
        Status = ProposalStatus.UnderAnalysis;
    }

    public void ApproveProposal()
    {
        Status = ProposalStatus.Approved;
    }

    public void RejectProposal(string reason)
    {
        Status = ProposalStatus.Rejected;
    }

    public ProposalNegotiation AddNegotiation(Guid senderId, string message, decimal? counterOffer = null)
    {
        var negotiation = new ProposalNegotiation
        {
            ProposalId = Id,
            SenderId = senderId,
            Message = message,
            CounterOffer = counterOffer,
            Status = NegotiationStatus.Sent
        };

        Negotiations.Add(negotiation);
        Status = ProposalStatus.InNegotiation;

        return negotiation;
    }
}

public enum ProposalType
{
    Purchase,
    Rent
}

public enum ProposalStatus
{
    Pending,
    UnderAnalysis,
    InNegotiation,
    Approved,
    Rejected,
    Cancelled,
    Completed
}