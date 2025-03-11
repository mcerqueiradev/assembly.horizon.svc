using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;
using iText.StyledXmlParser.Jsoup.Nodes;

namespace Assembly.Horizon.Domain.Model;

public class ProposalDocument : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid ProposalId { get; set; }
    public Guid? NegotiationId { get; set; }  // Opcional, para documentos vinculados a uma negociação específica
    public string DocumentName { get; set; }
    public string DocumentPath { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    public DocumentType Type { get; set; }
    public DocumentStatus Status { get; set; }
    public string Description { get; set; }

    // Relacionamentos
    public virtual PropertyProposal Proposal { get; set; }
    public virtual ProposalNegotiation? Negotiation { get; set; }
}

public enum DocumentStatus
{
    Pending,
    UnderReview,
    Approved,
    Rejected,
    Expired
}

public enum DocumentType
{
    Contract,
    Identification,
    ProofOfIncome,
    BankStatement,
    PropertyDocument,
    Other
}