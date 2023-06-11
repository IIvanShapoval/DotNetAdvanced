using Catalog.Api;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{

    var builder = WebApplication.CreateBuilder(args);

    var app = builder.ConfigureServices()
        .ConfigurePipeline();

    //await app.ResetDatabaseAsync();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
