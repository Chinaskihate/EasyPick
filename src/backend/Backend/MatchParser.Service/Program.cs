using MatchParser.Contract.Storage;
using MatchParser.Service.IoC;
using MatchParser.Service.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var settings = MatchParserSettingsReader.ReadSettings(builder.Configuration);

ServiceCollectionConfigurator.Configure(builder.Services, settings);

var app = builder.Build();

ConfigureApplication(app, app.Environment);

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var factory = serviceProvider.GetRequiredService<IDbContextFactory<MatchParserDbContext>>();
using var dbContext = factory.CreateDbContext();
dbContext.Database.EnsureCreated();

app.Run();

void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}