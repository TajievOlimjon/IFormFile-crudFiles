namespace WebCrudFiles.Services
{
    public interface IFileService
    {
        string AddFile(IFormFile file);
        string UpdateFile(IFormFile file, string filePath);
        int DeleteFile(string file);
    }
}
