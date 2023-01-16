namespace WebCrudFiles.DTOs
{
    public class CreateFileDto
    {
        public string Name { get; set; }
        public IFormFile FilePath { get; set; }
    }
}
