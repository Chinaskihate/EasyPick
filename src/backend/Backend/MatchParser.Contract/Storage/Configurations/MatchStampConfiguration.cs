using MatchParser.Contract.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchParser.Contract.Storage.Configurations;

public class MatchStampConfiguration : IEntityTypeConfiguration<MatchStampDto>
{
    public void Configure(EntityTypeBuilder<MatchStampDto> builder)
    {
        builder.HasKey(e => e.MatchId);
    }
}