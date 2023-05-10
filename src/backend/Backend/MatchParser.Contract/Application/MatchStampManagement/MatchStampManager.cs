using AutoMapper;
using MatchParser.Contract.Models;
using MatchParser.Contract.Models.DataTransferObjects;
using MatchParser.Contract.Storage;
using Microsoft.EntityFrameworkCore;

namespace MatchParser.Contract.Application.MatchStampManagement;

public class MatchStampManager : IMatchStampManager
{
    private readonly IDbContextFactory<MatchParserDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public MatchStampManager(
        IDbContextFactory<MatchParserDbContext> dbContextFactory,
        IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task CreateAsync(MatchStamp matchStamp, CancellationToken ct)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
        var dto = _mapper.Map<MatchStampDto>(matchStamp);
        await context.MatchStamps.AddAsync(dto, ct);
        await context.SaveChangesAsync(ct);
    }
}