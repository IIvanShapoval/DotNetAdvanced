using Catalog.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.ConfigureServices()
    .ConfigurePipeline();

//await app.ResetDatabaseAsync();

app.Run();
