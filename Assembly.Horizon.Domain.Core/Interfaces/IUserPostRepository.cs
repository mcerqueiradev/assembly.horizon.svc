using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IUserPostRepository : IRepository<UserPost, Guid>
{
    Task<IEnumerable<UserPost>> GetPostsByUserIdAsync(Guid userId);
    Task<IEnumerable<UserPost>> GetLatestPostsAsync(int take = 20);
    Task<bool> HasUserLikedPostAsync(Guid userId, Guid postId);
    Task<bool> HasUserSharedPostAsync(Guid userId, Guid postId);
    Task<int> GetPostLikesCountAsync(Guid postId);
    Task<int> GetPostSharesCountAsync(Guid postId);
}
