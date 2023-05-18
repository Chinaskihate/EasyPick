using DraftPrediction.Contract.Models.DataTransferObjects;
using MessageBroker.Common;
using MessageBroker.RabbitMQ;
using MessageBroker.Settings.Entities;
using ModelPredictionRedirector.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var producerSettings = new RabbitMqConnectionSettings()
{
    HostName = "rabbitmq",
    Port = 5672,
    QueueName = "PredictionResponses"
};
var consumerSettings = new RabbitMqConnectionSettings()
{
    HostName = "rabbitmq",
    Port = 5672,
    QueueName = "PredictRequests"
};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddLogging()
    .AddSwaggerGen()
    .AddTransient<IMessageConsumer<PredictDraftDto>, RabbitMqMessageConsumer<PredictDraftDto>>(p =>
        new RabbitMqMessageConsumer<PredictDraftDto>(
            consumerSettings,
            p.GetRequiredService<ILogger<IMessageConsumer<PredictDraftDto>>>()))
    .AddTransient<IMessageProducer<RecommendationsDto>, RabbitMqMessageProducer<RecommendationsDto>>(p =>
        new RabbitMqMessageProducer<RecommendationsDto>(
            producerSettings,
            p.GetRequiredService<ILogger<IMessageProducer<RecommendationsDto>>>()))
    .AddSingleton<RequestsStorage>();


var app = builder.Build();

var storage = app.Services.GetRequiredService<RequestsStorage>();
//var consumer = app.Services.GetRequiredService<IMessageConsumer<PredictDraftDto>>();
//consumer.ExecuteAsync((dto) =>
//{
//    storage.AddRequest(dto);
//    return Task.CompletedTask;
//}, CancellationToken.None);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
