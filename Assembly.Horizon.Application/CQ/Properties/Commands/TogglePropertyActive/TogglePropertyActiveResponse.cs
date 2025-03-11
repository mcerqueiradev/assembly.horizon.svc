namespace Assembly.Horizon.Application.CQ.Properties.Commands.TogglePropertyActive;

public record TogglePropertyActiveResponse
{
    public Guid Id { get; init; }
    public bool IsActive { get; init; }
    public DateTime? LastActiveDate { get; init; }
}
