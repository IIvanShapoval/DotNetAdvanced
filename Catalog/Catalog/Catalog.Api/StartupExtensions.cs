using Catalog.Application;
using Catalog.Persistance;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace Catalog.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
                            this WebApplicationBuilder builder)
        {
            //builder.Services.AddDefaultCorrelationId(options =>
            //{
            //    options.AddToLoggingScope = true;
            //    options.IgnoreRequestHeader = false;
            //    options.IncludeInResponse = true;
            //    options.RequestHeader = "X-Correlation-ID-Request";
            //    options.ResponseHeader = "X-Correlation-ID";
            //    options.UpdateTraceIdentifier = false;
            //});

            AddSwagger(builder.Services);

            //builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            //        .ReadFrom.Configuration(hostingContext.Configuration)
            //        .WriteTo.ApplicationInsights(new TelemetryConfiguration 
            //        { InstrumentationKey = "xxxxxxxxx" }, TelemetryConverter.Traces)
            // );

            builder.Services.AddApplicationInsightsTelemetry();

            builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.WithCorrelationId()
                    .WriteTo.ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(),
                                                                                        TelemetryConverter.Traces)
            //.WriteTo.ApplicationInsights(new TelemetryConfiguration
            //{ InstrumentationKey = "xxxxxxxxx" }, TelemetryConverter.Traces)
            );

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
            //builder.Services.AddHeaderPropagation(options =>
            //                                options.Headers.Add("X-Correlation-ID"));

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration);

            var app = builder.Build();
            app.UseCorrelationId();
            app.UseSerilogRequestLogging();

            return app;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog Service API");
                });
            }

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseCors("Open");

            app.MapControllers();

            return app;

        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Catalog Management API",

                });
            });
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<CatalogDbContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                //logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
