using AutoMapper;
using Common.Tasks;
using DraftPrediction.Contract.Application.Processing;
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
            .AddTransient<IMessageConsumer<PredictDraftDto>, RabbitMqMessageConsumer<PredictDraftDto>>(p =>
                new RabbitMqMessageConsumer<PredictDraftDto>(
                    settings.PredictionConsumer,
                    p.GetRequiredService<ILogger<IMessageConsumer<PredictDraftDto>>>()))
            .AddTransient<IMessageProducer<PredictDraftDto>, RabbitMqMessageProducer<PredictDraftDto>>(p => 
                new RabbitMqMessageProducer<PredictDraftDto>(
                    settings.PredictionProducer,
                    p.GetRequiredService<ILogger<IMessageProducer<PredictDraftDto>>>()))
            .AddSingleton<IDraftPredictionStorage, DraftPredictionStorage>()
            .AddTransient<IPredictionProvider, PredictionProvider>()
            .AddTransient<IPredictionManager, PredictionManager>()
            .AddSingleton<ITaskManager<IDistributionTask>>(p =>
            {
                var task = new PredictionProcessingDistributionTask(
                    p.GetRequiredService<IMessageConsumer<PredictDraftDto>>(),
                    p.GetRequiredService<IDraftPredictionStorage>(),
                    p.GetRequiredService<IMapper>(),
                    p.GetRequiredService<ILogger<IDistributionTask>>());
                return new DefaultTaskManager<IDistributionTask>(
                    task,
                    p.GetRequiredService<ILogger<ITaskManager<IDistributionTask>>>());
            });
    }
}