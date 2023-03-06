using PharmaMgmtApi.Helpers;
using PharmaMgmtApi.Interfaces.Services;

namespace PharmaMgmtApi.Services;

public class FileService : IFileService
{
    private readonly string _basePath;
    private const string ImageFolderName = "Images";

    public FileService(IWebHostEnvironment environment)
    {
        _basePath = environment.WebRootPath;
    }
    public Task<bool> DeleteImageAsync(string relativeFilePath)
    {
        var absoluteFilePath = Path.Combine(_basePath, relativeFilePath);

        if (!File.Exists(absoluteFilePath)) return Task.FromResult(false);

        try
        {
            File.Delete(absoluteFilePath);
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        var fileName = ImageHelper.MakeImageName(image.FileName);
        var partPath = Path.Combine(ImageFolderName, fileName);
        var path = Path.Combine(_basePath, partPath);

        var stream = File.Create(path);
        await image.CopyToAsync(stream);
        stream.Close();
        return partPath;
    }
}