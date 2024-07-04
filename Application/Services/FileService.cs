using Domain.Repositories.Services;
using Shared.Commons;

namespace Application.Services;


public class FileService : IFileService
{
    public async Task<string> SaveImage(byte[] imageData)
    {
        var filename = Path.Combine(PathConstants.IMAGEPATH, $"{Guid.NewGuid()}.jpeg");
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);

        await File.WriteAllBytesAsync(filePath, imageData);

        return filename;
    }
}
