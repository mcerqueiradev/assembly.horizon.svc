using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;

public class RetrieveRealtorResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public RetrieveUserResponse UserResponse { get; init; }
    public string OfficeEmail { get; init; }
    public int TotalSales { get; init; }
    public int TotalListings { get; init; }
    public List<string> Certifications { get; init; }
    public List<string> LanguagesSpoken { get; init; }
}