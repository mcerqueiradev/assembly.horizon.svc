using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

using Microsoft.AspNetCore.Http;

public class UserPost : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public PostType Type { get; set; }
    public bool IsActive { get; set; }
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public string? MediaUrl { get; set; }
    public MediaType? MediaType { get; set; }
    public IFormFile? MediaFile { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<UserPostLike> Likes { get; set; }
    public virtual ICollection<UserPostShare> Shares { get; set; }

    public UserPost()
    {
        Id = Guid.NewGuid();
        Likes = new List<UserPostLike>();
        Shares = new List<UserPostShare>();
    }

    public UserPost(Guid userId, string content, PostType type, IFormFile? mediaFile = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Content = content;
        Type = type;
        IsActive = true;
        MediaFile = mediaFile;
        MediaType = DetermineMediaType(mediaFile?.ContentType);
        Likes = new List<UserPostLike>();
        Shares = new List<UserPostShare>();
    }

    private MediaType? DetermineMediaType(string? contentType)
    {
        if (string.IsNullOrEmpty(contentType))
            return null;

        if (contentType.StartsWith("image/"))
            return Model.MediaType.Image;
        if (contentType.StartsWith("video/"))
            return Model.MediaType.Video;

        return null;
    }
}

public enum MediaType
{
    Image = 1,
    Video = 2
}

public enum PostType
{
    UserIdea,
    NewProperty,
    SystemUpdate
}
