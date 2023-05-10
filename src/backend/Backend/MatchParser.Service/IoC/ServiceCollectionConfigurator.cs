using AutoMapper;
using Common.Data;
using Common.Tasks;
using Http;
using MatchParser.Contract.Application.MatchManagement;
using MatchParser.Contract.Application.MatchResultProvider;
using MatchParser.Contract.Application.MatchStampManagement;
using MatchParser.Contract.Mappings;
using MatchParser.Contract.Models;
using MatchParser.Contract.Storage;
using MatchParser.Contract.Validators;
using MatchParser.Service.Settings.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchParser.Service.IoC;

public static class ServiceCollectionConfigurator
{
    public static void Configure(IServiceCollection services, MatchParserSettings settings)
    {
        services.AddControllers();
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        services
            .AddLogging()
            .AddAutoMapper(
                typeof(MatchResultMappingProfile),
                typeof(MatchStampMappingProfile))
            // TODO: change to Postgres
            .AddDbContextFactory<MatchParserDbContext>(options =>
                options.UseSqlite(settings.DbConnectionString))
            .AddSingleton<IHttpClientFactory<GenericHttpClient>>(p =>
                new GenericHttpClientFactory()
                    .Register(MatchIdClient, settings.MatchesDataUrl)
                    .Register(ParsedMatchesClient, settings.MatchesDataUrl))
            .AddTransient<IDataManager<MatchResult>>(p =>
                new CsvManager(
                    settings.PathToDirectory,
                    settings.MatchSettings,
                    p.GetRequiredService<ILogger<IDataManager<MatchResult>>>()))
            .AddTransient<IMatchStampManager>(p =>
                new MatchStampManager(
                    p.GetRequiredService<IDbContextFactory<MatchParserDbContext>>(),
                    p.GetRequiredService<IMapper>()))
            .AddTransient<IMatchStampProvider>(p =>
                new MatchStampProvider(
                    p.GetRequiredService<IHttpClientFactory<GenericHttpClient>>(),
                    ParsedMatchesClient,
                    p.GetRequiredService<IDbContextFactory<MatchParserDbContext>>(),
                    settings.BatchSettings.StartId,
                    p.GetRequiredService<IMapper>()))
            .AddTransient<IMatchResultChecker<MatchResult>>(p =>
                new UsualMatchResultChecker<MatchResult>(
                    settings.MatchSettings,
                    p.GetRequiredService<ILogger<IMatchResultChecker<MatchResult>>>()))
            .AddTransient<IMatchResultProvider<MatchResult>>(p =>
                new MatchResultProvider<MatchResult>(
                    p.GetRequiredService<IHttpClientFactory<GenericHttpClient>>(),
                    MatchIdClient,
                    p.GetRequiredService<IMapper>()))
            .AddSingleton<ITaskManager<IDistributionTask>>(p =>
                {
                    var task = new MatchManagementDistributionTask<MatchResult>(
                        p.GetRequiredService<IMatchResultProvider<MatchResult>>(),
                        p.GetRequiredService<IDataManager<MatchResult>>(),
                        p.GetRequiredService<IMatchStampProvider>(),
                        p.GetRequiredService<IMatchStampManager>(),
                        p.GetRequiredService<IMatchResultChecker<MatchResult>>(),
                        settings.BatchSettings,
                        p.GetRequiredService<ILogger<IDistributionTask>>()
                    );
                    return new DefaultTaskManager<IDistributionTask>(
                        task,
                        p.GetRequiredService<ILogger<ITaskManager<IDistributionTask>>>());
                });
    }

    public const string MatchIdClient = nameof(MatchIdClient);
    public const string ParsedMatchesClient = nameof(ParsedMatchesClient);
}