namespace Assembly.Horizon.Application.CQ.UserProfiles.Queries.Retrieve;

public class RetrieveUserProfileResponse
{
    public Guid Id { get; set; }
    public string Bio { get; set; }
    public string ProfileCoverUrl { get; set; }
    public string Location { get; set; }
    public string Website { get; set; }
    public string Occupation { get; set; }
}
