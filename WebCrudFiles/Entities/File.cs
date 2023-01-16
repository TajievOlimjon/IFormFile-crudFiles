namespace WebCrudFiles.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAd { get; set; }
        public DateTime UpdateAd { get; set; }
    }
}
