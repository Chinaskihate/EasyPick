using MatchParser.Contract.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace MatchParser.Contract.Storage;

public interface IMatchParserDbContext
{
    DbSet<MatchStampDto> MatchStamps { get; set; }
}