using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Domain.Core.Interfaces
{
    public interface IPdfGenerationService
    {
        Task<string> GenerateContractPdfAsync(Contract contract, Customer customer, Realtor realtor, Property property);
    }
}
