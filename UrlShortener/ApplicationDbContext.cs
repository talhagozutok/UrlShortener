using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Services;

namespace UrlShortener;

public class ApplicationDbContext : DbContext
{
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ShortenedUrl>(builder =>
        {
            builder.HasIndex(s => s.Code).IsUnique();
            builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberOfCharsInShortLink);

            // Use case sensitive collation
            builder.Property(s => s.Code).UseCollation("SQL_Latin1_General_CP1_CS_AS");
        });
    }
}
