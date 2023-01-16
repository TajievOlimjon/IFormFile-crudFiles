namespace WebCrudFiles.Services
{
    public class FileService : IFileService
    {
        public readonly IWebHostEnvironment _webHost;
        public FileService(IWebHostEnvironment web)
        {
            _webHost = web;
        }
        public string AddFile(IFormFile file)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                var path = Path.Combine(_webHost.WebRootPath, "Files", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return fileName;
            }
            return null;
        }

        public int DeleteFile(string file)
        {
            var imagePath = Path.Combine(_webHost.WebRootPath, "Files", file);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return 1;
            }
            return 0;
        }


        public string UpdateFile(IFormFile file, string filePath)
        {
            string fileName = "", path;

            if (!file.Equals(null))
            {
                var fullPath = _webHost.WebRootPath + filePath;
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);

                    fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                    path = Path.Combine(_webHost.WebRootPath, "Files", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return fileName;
                }
                return null;
            }
                return null;
        }
    }
}
