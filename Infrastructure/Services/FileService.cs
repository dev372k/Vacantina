using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IFileService
    {
        Task<string> SaveImage(byte[] imageData);
        public void DeleteImage(string imageUrl);
    }

    public class FileService : IFileService
    {

        public async Task<string> SaveImage(byte[] imageData)
        {
            //var filename = $"{Guid.NewGuid()}.jpeg";
            //var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            //var imagePath = Path.Combine("Images", filename);
            //var absolutePath = Path.Combine(_httpContextAccessor.HttpContext.Request.PathBase, imagePath);
            //var filePath = Path.Combine(_httpContextAccessor.HttpContext.Request.PathBase, "wwwroot", imagePath);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await stream.WriteAsync(imageData, 0, imageData.Length);
            //}

            //return $"{baseUrl}/{absolutePath}";
            return string.Empty;
        }

        public void DeleteImage(string imageUrl)
        {
            //try
            //{
            //    var imagePath = imageUrl.Replace("/", "\\").Split("\\").Last(); // Extract filename from URL
            //    var absolutePath = Path.Combine(_httpContextAccessor.HttpContext.Request.PathBase, "wwwroot", "Images", imagePath);
            //    if (File.Exists(absolutePath))
            //    {
            //        File.Delete(absolutePath);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Failed to delete file: {ex.Message}");
            //}
        }
    }
}
