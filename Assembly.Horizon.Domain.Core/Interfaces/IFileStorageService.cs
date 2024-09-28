using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, CancellationToken cancellationToken);
    Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken);
}
