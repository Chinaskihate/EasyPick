using DraftPrediction.Contract.Models.DataTransferObjects;
using MessageBroker.Common;
using MessageBroker.RabbitMQ;
using ModelPredictionRedirector.Services;
using ModelPredictionRedirector.Settings;

var builder = WebApplication.CreateBuilder(args);

var settings = ModelPredictionRedirectorServiceSettingsReader.ReadSettings(builder.Configuration);

var producerSettings = settings.RedirectorProducer;
var consumerSettings = settings.RedirectorConsumer;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddLogging()
    .AddSwaggerGen()
    .AddTransient<IMessageProducer<RecommendationsDto>, RabbitMqMessageProducer<RecommendationsDto>>(p =>
        new RabbitMqMessageProducer<RecommendationsDto>(
            producerSettings,
            p.GetRequiredService<ILogger<IMessageProducer<RecommendationsDto>>>()))
    .AddSingleton<RequestsStorage>()
    .AddHostedService<RabbitMqMessageListener<PredictDraftDto>>(p =>
        new RabbitMqMessageListener<PredictDraftDto>(
            (dto) =>
            {
                p.GetRequiredService<RequestsStorage>().AddRequest(dto);
                return Task.CompletedTask;
            },
            consumerSettings,
            p.GetRequiredService<ILogger<IMessageListener<PredictDraftDto>>>()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
