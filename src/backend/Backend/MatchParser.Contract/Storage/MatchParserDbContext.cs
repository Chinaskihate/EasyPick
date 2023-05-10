using MatchParser.Contract.Models.DataTransferObjects;
using MatchParser.Contract.Storage.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MatchParser.Contract.Storage;

public class MatchParserDbContext : DbContext, IMatchParserDbContext
{
    public MatchParserDbContext(DbContextOptions options) : base(options) { }

    public DbSet<MatchStampDto> MatchStamps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MatchStampConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}