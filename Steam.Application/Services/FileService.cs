using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Saves the uploaded file to the wwwroot/uploads folder.
        /// </summary>
        /// <param name="file">The IFormFile to save.</param>
        /// <returns>The relative path of the saved file (e.g., /uploads/filename.jpg).</returns>
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty.");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative path to be stored in the database
            return "/uploads/" + uniqueFileName;
        }

        /// <summary>
        /// Deletes a file from the wwwroot folder.
        /// </summary>
        /// <param name="filePath">The relative path of the file (e.g., /uploads/filename.jpg).</param>
        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            // Convert relative path to full physical path
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
