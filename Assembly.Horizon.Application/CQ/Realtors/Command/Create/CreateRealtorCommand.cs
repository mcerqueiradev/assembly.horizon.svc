using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Realtors.Command.Create;

public class CreateRealtorCommand : IRequest<Result<CreateRealtorResponse, Success, Error>>
{
    public required Guid UserId { get; init; }
    public required string OfficeEmail { get; init; }
    public int TotalSales { get; init; }
    public int TotalListings { get; init; }
    public List<string> Certifications { get; init; }
    public List<string> LanguagesSpoken { get; init; } = new();
}
