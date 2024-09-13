namespace Assembly.Horizon.Security.Interface
{
    public interface IUserResolverService
    {
        Guid GetUserId();
        string GetUserName();
        string GetUserEmail();
    }
}