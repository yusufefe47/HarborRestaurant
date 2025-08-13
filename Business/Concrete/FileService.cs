using Microsoft.AspNetCore.Hosting;

namespace HarborRestaurant.Business.Concrete
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file, string folder = "uploads");
        Task<bool> DeleteImageAsync(string imagePath);
        string GetImagePath(string fileName, string folder = "uploads");
        List<string> GetAllowedImageExtensions();
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder = "uploads")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya seçilmedi.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                throw new ArgumentException($"Sadece şu formatlar destekleniyor: {string.Join(", ", _allowedExtensions)}");

            // Dosya boyutu kontrolü (5MB)
            if (file.Length > 5 * 1024 * 1024)
                throw new ArgumentException("Dosya boyutu 5MB'dan büyük olamaz.");

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", folder);
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{folder}/{uniqueFileName}";
        }

    public Task<bool> DeleteImageAsync(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
        return Task.FromResult(false);

            try
            {
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
                
                if (File.Exists(fullPath))
                {
                    // Dosya silme IO-bound sayılabilir ancak .NET'te File.Delete senkron.
                    // Gelecekte async IO eklendiğinde bekleme eklenebilir.
                    File.Delete(fullPath);
            return Task.FromResult(true);
                }
            }
            catch
            {
                // Log error
            }

        return Task.FromResult(false);
        }

        public string GetImagePath(string fileName, string folder = "uploads")
        {
            return $"/images/{folder}/{fileName}";
        }

        public List<string> GetAllowedImageExtensions()
        {
            return _allowedExtensions.ToList();
        }
    }
}
