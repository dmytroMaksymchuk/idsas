using IDsas.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDsas.Server
{
    public class DocumentContext : DbContext
    {

        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentLink>().Navigation(documentLink => documentLink.Document).AutoInclude();
            modelBuilder.Entity<DocumentLink>().Navigation(documentLink => documentLink.AssociatedUser).AutoInclude();
                
                //.Include(documentLink => documentLink.AssociatedUser)
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Document> Documents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<DocumentLink> DocumentLinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=documents.db");
        }
    }
}
