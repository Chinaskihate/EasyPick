using DraftPrediction.Contract.Application;
using DraftPrediction.Contract.Mappings;
using DraftPrediction.Contract.Models.DataTransferObjects;
using DraftPrediction.Contract.Storage;
using DraftPrediction.Settings.Entities;
using MessageBroker.Common;
using MessageBroker.RabbitMQ;

namespace DraftPrediction.IoC;

public class ServiceCollectionConfigurator
{
    public static void Configure(IServiceCollection services, DraftPredictionServiceSettings settings)
    {
        services.AddControllers();
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddLogging()
            .AddCors(o =>
            {
                o.AddPolicy(settings.CorsPolicyName, policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            })
            .AddAutoMapper(
                typeof(PredictionMappingProfile));

        services
            .AddHostedService<RabbitMqMessageListener<RecommendationsDto>>(p =>
                new RabbitMqMessageListener<RecommendationsDto>(
                    (dto) =>
                        p.GetRequiredService<IDraftPredictionStorage>()
                            .UpdateAsync(dto, CancellationToken.None),
                    settings.PredictionConsumer,
                    p.GetRequiredService<ILogger<IMessageListener<RecommendationsDto>>>()))
            .AddTransient<IMessageProducer<PredictDraftDto>, RabbitMqMessageProducer<PredictDraftDto>>(p => 
                new RabbitMqMessageProducer<PredictDraftDto>(
                    settings.PredictionProducer,
                    p.GetRequiredService<ILogger<IMessageProducer<PredictDraftDto>>>()))
            .AddSingleton<IDraftPredictionStorage, DraftPredictionStorage>()
            .AddTransient<IPredictionProvider, PredictionProvider>()
            .AddTransient<IPredictionManager, PredictionManager>();
    }
}