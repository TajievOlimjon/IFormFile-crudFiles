using Microsoft.EntityFrameworkCore;

namespace WebCrudFiles.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
        public DbSet<WebCrudFiles.Entities.File> Files { get; set; }
    }
}
