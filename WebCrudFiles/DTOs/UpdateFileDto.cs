namespace WebCrudFiles.DTOs
{
    public class UpdateFileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? FilePath { get; set; }
    }
}
