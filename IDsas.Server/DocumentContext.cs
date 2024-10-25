using IDsas.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDsas.Server
{
    public class DocumentContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=documents.db");
        }
    }
}
