using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Horizon.Domain.Model;

public class ProposalNegotiation : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid ProposalId { get; set; }
    public Guid SenderId { get; set; }
    public string Message { get; set; }
    public decimal? CounterOffer { get; set; }
    public NegotiationStatus Status { get; set; }

    // Relacionamentos
    public virtual PropertyProposal Proposal { get; set; }
    public virtual User Sender { get; set; }
    public virtual ICollection<ProposalDocument> Documents { get; set; }

    public ProposalNegotiation()
    {
        Documents = new HashSet<ProposalDocument>();
    }

    public void Accept()
    {
        Status = NegotiationStatus.Accepted;
    }

    public void Reject()
    {
        Status = NegotiationStatus.Rejected;
    }

    public void AddCounterOffer(decimal value)
    {
        CounterOffer = value;
        Status = NegotiationStatus.CounterOffered;
    }

    public ProposalDocument AttachDocument(string name, string path, DocumentType type, string contentType)
    {
        var document = new ProposalDocument
        {
            ProposalId = ProposalId,
            NegotiationId = Id,
            DocumentName = name,
            DocumentPath = path,
            Type = type,
            Status = DocumentStatus.Pending,
            ContentType = contentType,
            Description = $"Document uploaded for proposal negotiation: {name}" // Adding a default description
        };

        Documents.Add(document);
        return document;
    }

}

public enum NegotiationStatus
{
    Sent,
    Received,
    Accepted,
    Rejected,
    CounterOffered
}