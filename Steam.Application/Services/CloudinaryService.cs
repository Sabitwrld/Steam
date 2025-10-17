using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Steam.Application.Services
{
    public class CloudinaryService : IFileService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["CloudinarySettings:CloudName"],
                configuration["CloudinarySettings:ApiKey"],
                configuration["CloudinarySettings:ApiSecret"]);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty.");
            }

            // Faylın tipini təyin edirik (video və ya şəkil)
            var isVideo = file.ContentType.ToLower().StartsWith("video");
            UploadResult uploadResult;

            await using var stream = file.OpenReadStream();

            if (isVideo)
            {
                // Video yükləmək üçün VideoUploadParams istifadə edirik
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            else
            {
                // Şəkil yükləmək üçün ImageUploadParams istifadə edirik
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return uploadResult.SecureUrl.ToString();
        }

        public async Task DeleteFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return;

            var uri = new Uri(fileUrl);
            var publicId = System.IO.Path.GetFileNameWithoutExtension(uri.AbsolutePath);

            // URL-də "/video/" və ya "/image/" hissəsinə baxaraq tipi təyin edirik
            var resourceType = uri.Segments.Any(s => s.Contains("video")) ? ResourceType.Video : ResourceType.Image;

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = resourceType
            };

            await _cloudinary.DestroyAsync(deletionParams);
        }
    }
}
