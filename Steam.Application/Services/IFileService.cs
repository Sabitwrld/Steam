using Microsoft.AspNetCore.Http;

namespace Steam.Application.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
        Task DeleteFile(string fileUrl);
    }
}
