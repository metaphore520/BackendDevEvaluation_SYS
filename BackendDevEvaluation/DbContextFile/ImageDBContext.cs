using BackendDevEvaluation.Models;
using Microsoft.EntityFrameworkCore;


namespace BackendDevEvaluation.DbContextFile
{
    public class ImageDBContext : DbContext
    {

        protected override void OnConfiguring
(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ImageDb");
        }
        public DbSet<ImageEntity> ImageEntity { get; set; }

    }
}
