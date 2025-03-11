namespace Assembly.Horizon.Infra.Data.Infrastructure.Models;

public interface IEmailService
{
    Task SendVisitScheduledEmailAsync(VisitScheduledEmail model);
    Task SendVisitConfirmedEmailAsync(VisitConfirmedEmail model);
    Task SendVisitDeclinedEmailAsync(VisitDeclinedEmail model);
    Task SendProposalSubmittedEmailAsync(ProposalSubmittedEmail model);
    Task SendContractSubmittedEmailAsync(ContractSubmittedEmail model);

}