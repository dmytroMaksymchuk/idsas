using IDsas.Server.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace IDsas.Server;

public class DatabaseContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //AutoInclude most of the related entities. For the Proof of Concept, this was deemed acceptable.
        modelBuilder.Entity<DocumentLink>().Navigation(documentLink => documentLink.Document).AutoInclude();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Document> Documents { get; set; }

    public DbSet<DocumentLink> DocumentLinks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=documents.db");
    }
}