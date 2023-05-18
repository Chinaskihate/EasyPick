using DraftPrediction.IoC;
using DraftPrediction.Settings;

var builder = WebApplication.CreateBuilder(args);
var settings = DraftPredictionServiceSettingsReader.ReadSettings(builder.Configuration);

ServiceCollectionConfigurator.Configure(builder.Services, settings);

var app = builder.Build();

ConfigureApplication(app, app.Environment);

app.Run();

void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();
    app.UseAuthorization();
    app.UseCors(settings.CorsPolicyName);
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}