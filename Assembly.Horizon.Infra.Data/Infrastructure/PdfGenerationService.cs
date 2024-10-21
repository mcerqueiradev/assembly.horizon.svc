using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;
using Microsoft.Extensions.Logging;
using iText.Layout.Borders;
using iText.IO.Image;
using Microsoft.Extensions.Configuration;

namespace Assembly.Horizon.Infra.Data.Infrastructure;

public class PdfGenerationService : IPdfGenerationService
{
    private readonly string _outputDirectory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PdfGenerationService> _logger;

    public PdfGenerationService(string outputDirectory, ILogger<PdfGenerationService> logger, IConfiguration configuration)
    {
        _outputDirectory = outputDirectory;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<string> GenerateContractPdfAsync(Contract contract, Customer customer, Realtor realtor, Domain.Model.Property property)
    {
        try
        {
            string outputPath = Path.Combine(_outputDirectory, $"{contract.Id}.pdf");
            Directory.CreateDirectory(_outputDirectory);

            using (var writer = new PdfWriter(outputPath))
            using (var pdfDocument = new PdfDocument(writer))
            using (var document = new Document(pdfDocument))
            {
                // Define fonts
                PdfFont titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                // Add title
                document.Add(new Paragraph("CONTRACT OF SALE")
                    .SetFont(titleFont)
                    .SetFontSize(18)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetMarginBottom(20));

                // Add contract introduction
                AddParagraph(document, $"This Contract of Sale (\"Agreement\") is made and entered into on {DateTime.UtcNow.ToShortDateString()} by and between the following parties:", normalFont);

                // Add seller and buyer information
                AddPartyInfo(document, "Seller:", realtor, normalFont);
                AddPartyInfo(document, "Buyer:", customer, normalFont);

                // Add property description
                AddSection(document, "1. PROPERTY DESCRIPTION", titleFont);
                AddParagraph(document, "The Seller hereby agrees to sell, and the Buyer agrees to buy, the real property located at the address described as follows:", normalFont);
                AddPropertyInfo(document, property, normalFont);

                // Add purchase price
                AddSection(document, "2. PURCHASE PRICE", titleFont);
                AddParagraph(document, $"The total purchase price for the Property shall be {contract.Value:C}. The Buyer agrees to pay the Seller in the following manner:", normalFont);
                AddParagraph(document, $"Earnest Money Deposit: The Buyer shall provide an earnest money deposit in the amount of {(decimal)contract.SecurityDeposit:C} to be held in escrow by [EscrowAgentName] until closing. This deposit shall be applied toward the purchase price at closing.", normalFont);
                AddParagraph(document, "Balance Due at Closing: The remaining balance of the purchase price, after accounting for the earnest money deposit, shall be due and payable at the time of closing. Payment shall be made in the form of certified funds, bank check, or wire transfer as mutually agreed upon by both parties.", normalFont);

                // Add closing date
                AddSection(document, "3. CLOSING DATE", titleFont);
                AddParagraph(document, $"The closing of the sale shall take place on or before {contract.EndDate.ToShortDateString()}. The specific date shall be confirmed in writing by both parties no later than ten (10) days prior to the closing. If the closing does not occur on the agreed-upon date due to any fault of the Buyer or Seller, the non-defaulting party shall have the right to pursue any available remedies under this Agreement or applicable law.", normalFont);

                // Add section 4 - CONDITIONS OF SALE
                AddSection(document, "4. CONDITIONS OF SALE", titleFont);
                AddParagraph(document, "The sale is contingent upon the following conditions, which must be satisfied prior to the closing date:", normalFont);
                AddParagraph(document, "Inspection Contingency: The Buyer shall have the right to conduct a satisfactory inspection of the Property within ____ days from the execution of this Agreement. Should any significant issues be discovered, the Buyer may request repairs or a price adjustment, and the Seller shall have the right to respond to such requests.", normalFont);

                // Add section 5 - POSSESSION
                AddSection(document, "5. POSSESSION", titleFont);
                AddParagraph(document, "Possession of the Property shall be delivered to the Buyer on the closing date unless otherwise mutually agreed in writing. The Buyer acknowledges that they have had the opportunity to inspect the Property and are purchasing it in its current condition, subject to any disclosures made by the Seller.", normalFont);

                // Add section 6 - REPRESENTATIONS AND WARRANTIES
                AddSection(document, "6. REPRESENTATIONS AND WARRANTIES", titleFont);
                AddParagraph(document, "The Seller represents and warrants to the Buyer that:", normalFont);
                AddParagraph(document, "Legal Ownership: The Seller is the lawful owner of the Property and has the legal authority to enter into this Agreement. The Property is free from all liens, encumbrances, and restrictions, except as disclosed in this Agreement.", normalFont);
                AddParagraph(document, "Property Condition: The Seller has provided the Buyer with a complete and accurate disclosure of the Property's condition, including any known defects or issues. The Seller shall not be liable for any defects not disclosed to the Buyer prior to the execution of this Agreement.", normalFont);

                // Add section 7 - DEFAULT
                AddSection(document, "7. DEFAULT", titleFont);
                AddParagraph(document, "In the event of a default by either party under the terms of this Agreement, the non-defaulting party may choose to pursue one or more of the following remedies:", normalFont);
                AddParagraph(document, "Specific Performance: The non-defaulting party may seek specific performance of this Agreement, requiring the defaulting party to fulfill their obligations hereunder.", normalFont);
                AddParagraph(document, "Liquidated Damages: In the event of the Buyer’s default, the Seller shall have the right to retain the earnest money deposit as liquidated damages, which shall be the Seller’s sole remedy.", normalFont);
                AddParagraph(document, "Legal Fees: The defaulting party shall be responsible for all legal fees and costs incurred by the non-defaulting party in enforcing this Agreement.", normalFont);

                // Add section 8 - ENTIRE AGREEMENT
                AddSection(document, "8. ENTIRE AGREEMENT", titleFont);
                AddParagraph(document, "This Agreement constitutes the entire understanding between the parties with respect to the subject matter hereof and supersedes all prior negotiations, discussions, or agreements, whether written or oral. Any amendments or modifications to this Agreement must be in writing and signed by both parties.", normalFont);


                // Add remaining sections (4-8) similarly...

                // Add signatures
                AddSignatures(document, normalFont);
            }

            return outputPath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating PDF for contract {ContractId}", contract.Id);
            throw new ApplicationException("Error generating PDF", ex);
        }
    }

    private void AddParagraph(Document document, string text, PdfFont font)
    {
        document.Add(new Paragraph(text).SetFont(font).SetFontSize(12).SetMarginBottom(10));
    }

    private void AddSection(Document document, string title, PdfFont font)
    {
        document.Add(new Paragraph(title).SetFont(font).SetFontSize(14).SetMarginTop(20).SetMarginBottom(10).SetTextAlignment(TextAlignment.LEFT));
    }

    private void AddPartyInfo(Document document, string title, dynamic party, PdfFont font)
    {
        document.Add(new Paragraph(title).SetFont(font).SetFontSize(14).SetMarginBottom(5));
        document.Add(new Paragraph($"Name: {party.User.Name.FirstName} {party.User.Name.LastName}").SetFont(font).SetMarginLeft(20));
        document.Add(new Paragraph($"Phone: {party.User.PhoneNumber}").SetFont(font).SetMarginLeft(20));
        document.Add(new Paragraph($"Email: {(title == "Seller:" ? party.OfficeEmail : party.User.Account.Email)}").SetFont(font).SetMarginLeft(20).SetMarginBottom(10));
    }

    private void AddPropertyInfo(Document document, Domain.Model.Property property, PdfFont font)
    {
        document.Add(new Paragraph($"Address: {property.Address.Street}").SetFont(font).SetMarginLeft(20));
        document.Add(new Paragraph($"City: {property.Address.City}").SetFont(font).SetMarginLeft(20));
        document.Add(new Paragraph($"State: {property.Address.State}").SetFont(font).SetMarginLeft(20));
        document.Add(new Paragraph($"Zip Code: {property.Address.PostalCode}").SetFont(font).SetMarginLeft(20));
        document.Add(new Paragraph($"Property ID: {property.Id}").SetFont(font).SetMarginLeft(20).SetMarginBottom(10));
    }

    private void AddSignatures(Document document, PdfFont font)
    {
        document.Add(new Paragraph("\n\n"));
        document.Add(new Paragraph("IN WITNESS WHEREOF, the parties hereto have executed this Agreement as of the date first above written.").SetFont(font).SetMarginBottom(20));

        Table table = new Table(2).UseAllAvailableWidth();
        table.AddCell(new Cell().Add(new Paragraph("Seller's Signature").SetFont(font)).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Buyer's Signature").SetFont(font)).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("_______________________________").SetFont(font)).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("_______________________________").SetFont(font)).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Date: ________________________").SetFont(font)).SetBorder(Border.NO_BORDER));
        table.AddCell(new Cell().Add(new Paragraph("Date: ________________________").SetFont(font)).SetBorder(Border.NO_BORDER));

        document.Add(table);
    }
}