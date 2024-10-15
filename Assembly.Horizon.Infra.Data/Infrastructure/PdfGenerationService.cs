using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.Logging;

namespace Assembly.Horizon.Infra.Data.Infrastructure;

public class PdfGenerationService : IPdfGenerationService
{
    private readonly string _outputDirectory;
    private readonly ILogger<PdfGenerationService> _logger;

    public PdfGenerationService(string outputDirectory, ILogger<PdfGenerationService> logger)
    {
        _outputDirectory = outputDirectory;
        _logger = logger;
    }

    public async Task<string> GenerateContractPdfAsync(Contract contract, Customer customer, Realtor realtor, Property property)
    {
        try
        {
            string outputPath = Path.Combine(_outputDirectory, $"{contract.Id}.pdf");
            Directory.CreateDirectory(_outputDirectory);

            using (var writer = new PdfWriter(outputPath))
            using (var pdfDocument = new PdfDocument(writer))
            using (var document = new Document(pdfDocument))
            {
                // Definindo o texto do contrato com placeholders
                string contractText = @"
                    CONTRACT OF SALE

                    This Contract of Sale (""Agreement"") is made and entered into on [Date] by and between the following parties:

                    Seller:
                    Name: [RealtorName]
                    Phone: [RealtorPhone]
                    Email: [RealtorEmail]

                    Buyer:
                    Name: [CustomerName]
                    Phone: [CustomerPhone]
                    Email: [CustomerEmail]

                    1. PROPERTY DESCRIPTION
                    The Seller hereby agrees to sell, and the Buyer agrees to buy, the real property located at the address described as follows:
                    Address: [PropertyAddress]
                    City: [PropertyCity]
                    State: [PropertyState]
                    Zip Code: [PropertyZip]
                    Property ID: [PropertyId]

                    The Property includes all improvements, fixtures, and appurtenances thereon, including but not limited to buildings, structures, and any attached machinery or equipment, unless otherwise specified.

                    2. PURCHASE PRICE
                    The total purchase price for the Property shall be $[PurchasePrice]. The Buyer agrees to pay the Seller in the following manner:

                    Earnest Money Deposit: The Buyer shall provide an earnest money deposit in the amount of $[EarnestMoney] to be held in escrow by [EscrowAgentName] until closing. This deposit shall be applied toward the purchase price at closing.

                    Balance Due at Closing: The remaining balance of the purchase price, after accounting for the earnest money deposit, shall be due and payable at the time of closing. Payment shall be made in the form of certified funds, bank check, or wire transfer as mutually agreed upon by both parties.

                    3. CLOSING DATE
                    The closing of the sale shall take place on or before [ClosingDate]. The specific date shall be confirmed in writing by both parties no later than ten (10) days prior to the closing. If the closing does not occur on the agreed-upon date due to any fault of the Buyer or Seller, the non-defaulting party shall have the right to pursue any available remedies under this Agreement or applicable law.

                    4. CONDITIONS OF SALE
                    The sale is contingent upon the following conditions, which must be satisfied prior to the closing date:

                    Inspection Contingency: The Buyer shall have the right to conduct a satisfactory inspection of the Property within ____ days from the execution of this Agreement. Should any significant issues be discovered, the Buyer may request repairs or a price adjustment, and the Seller shall have the right to respond to such requests.

                    5. POSSESSION
                    Possession of the Property shall be delivered to the Buyer on the closing date unless otherwise mutually agreed in writing. The Buyer acknowledges that they have had the opportunity to inspect the Property and are purchasing it in its current condition, subject to any disclosures made by the Seller.

                    6. REPRESENTATIONS AND WARRANTIES
                    The Seller represents and warrants to the Buyer that:
                    Legal Ownership: The Seller is the lawful owner of the Property and has the legal authority to enter into this Agreement. The Property is free from all liens, encumbrances, and restrictions, except as disclosed in this Agreement.

                    Property Condition: The Seller has provided the Buyer with a complete and accurate disclosure of the Property's condition, including any known defects or issues. The Seller shall not be liable for any defects not disclosed to the Buyer prior to the execution of this Agreement.

                    7. DEFAULT
                    In the event of a default by either party under the terms of this Agreement, the non-defaulting party may choose to pursue one or more of the following remedies:

                    Specific Performance: The non-defaulting party may seek specific performance of this Agreement, requiring the defaulting party to fulfill their obligations hereunder.

                    Liquidated Damages: In the event of the Buyer’s default, the Seller shall have the right to retain the earnest money deposit as liquidated damages, which shall be the Seller’s sole remedy.

                    Legal Fees: The defaulting party shall be responsible for all legal fees and costs incurred by the non-defaulting party in enforcing this Agreement.

                    8. ENTIRE AGREEMENT
                    This Agreement constitutes the entire understanding between the parties with respect to the subject matter hereof and supersedes all prior negotiations, discussions, or agreements, whether written or oral. Any amendments or modifications to this Agreement must be in writing and signed by both parties.

                    IN WITNESS WHEREOF, the parties hereto have executed this Agreement as of the date first above written.




                    Seller's Signature  Buyer's Signature 

                    Date: ________________________ Date: ________________________
                    ";

                // Substitua os placeholders pelas informações reais
                contractText = contractText.Replace("[Date]", DateTime.UtcNow.ToShortDateString())
                                             .Replace("[RealtorName]", realtor.User.Name.FirstName + " " + realtor.User.Name.LastName)
                                             .Replace("[RealtorPhone]", realtor.User.PhoneNumber)
                                             .Replace("[RealtorEmail]", realtor.OfficeEmail)
                                             .Replace("[CustomerName]", customer.User.Name.FirstName + " " + customer.User.Name.LastName)
                                             .Replace("[CustomerPhone]", customer.User.PhoneNumber)
                                             .Replace("[CustomerEmail]", customer.User.Account.Email)
                                             .Replace("[PropertyAddress]", property.Address.Street)
                                             .Replace("[PropertyCity]", property.Address.City)
                                             .Replace("[PropertyState]", property.Address.State)
                                             .Replace("[PropertyZip]", property.Address.PostalCode)
                                             .Replace("[PropertyId]", property.Id.ToString())
                                             .Replace("[PurchasePrice]", contract.Value.ToString("C")) // Exemplo para formatar como moeda
                                             .Replace("[EarnestMoney]", ((decimal)contract.SecurityDeposit).ToString("C")) // Agora o depósito de segurança está formatado corretamente como moeda
                                             .Replace("[EscrowAgentName]", "Escrow Agent Name") // Suponha que você tenha um nome para o agente de custódia
                                             .Replace("[ClosingDate]", contract.EndDate.ToShortDateString())
                                             .Replace("[AdditionalConditions]", contract.Notes ?? "None");

                // Adicione o texto ao documento PDF
                document.Add(new Paragraph(contractText));
            }

            return outputPath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating PDF for contract {ContractId}", contract.Id);
            throw new ApplicationException("Error generating PDF", ex);
        }
    }
}
