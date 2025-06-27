using System.Diagnostics;
using CleanArc.Application.ServiceConfiguration;
using CleanArc.Domain.Entities.User;
using CleanArc.Infrastructure.CrossCutting.Logging;
using CleanArc.Infrastructure.Identity.Identity.Dtos;
using CleanArc.Infrastructure.Identity.Identity.SeedDatabaseService;
using CleanArc.Infrastructure.Identity.Jwt;
using CleanArc.Infrastructure.Identity.ServiceConfiguration;
using CleanArc.Infrastructure.Persistence;
using CleanArc.Infrastructure.Persistence.ServiceConfiguration;
using CleanArc.SharedKernel.Extensions;
using CleanArc.Web.Api.Controllers.V1.UserManagement;
using CleanArc.Web.Plugins.Grpc;
using CleanArc.WebFramework.Filters;
using CleanArc.WebFramework.Middlewares;
using CleanArc.WebFramework.ServiceConfiguration;
using CleanArc.WebFramework.Swagger;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Exceptions;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.Identity;
using Serilog.Enrichers;
using Microsoft.AspNetCore.Http;
using CleanArc.Application.Common;
using System.Net.Http.Headers;
using CleanArc.Web.Api;


var builder = WebApplication.CreateBuilder(args);

// Read Serilog settings from configuration
var serilogSettings = builder.Configuration.GetSection("SerilogSettings").Get<SerilogSettings>()
    ?? new SerilogSettings();

// Configure logging based on settings
ConfigureLogging(builder, serilogSettings);

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

// Configure services
builder.Services.Configure<IdentitySettings>(builder.Configuration.GetSection(nameof(IdentitySettings)));
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
var emailSettings = builder.Configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();

builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new TimeModelBinderProvider());
    options.Filters.Add(typeof(OkResultAttribute));
    options.Filters.Add(typeof(NotFoundResultAttribute));
    options.Filters.Add(typeof(ContentResultFilterAttribute));
    options.Filters.Add(typeof(ModelStateValidationAttribute));
    options.Filters.Add(typeof(BadRequestResultFilterAttribute));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
});

builder.Services.AddSwagger();
builder.Services.AddApplicationServices()
    .RegisterIdentityServices(identitySettings, builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddWebFrameworkServices(builder.Configuration);

builder.Services.RegisterValidatorsAsServices();

#region Plugin Services Configuration
builder.Services.ConfigureGrpcPluginServices();
#endregion

builder.Services.AddAutoMapper(typeof(User), typeof(JwtService), typeof(UserController));

var app = builder.Build();

#region Middleware Pipeline
app.MapPost("/api/v1/uploadFile", async (HttpRequest request) =>
{
    try
    {
        var formCollection = await request.ReadFormAsync();
        var file = formCollection.Files.FirstOrDefault();

        if (file == null || file.Length == 0)
        {
            return Results.BadRequest("No file uploaded");
        }

        var folderName = Path.Combine("Resources", "Images2");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        var fullPath = Path.Combine(pathToSave, fileName);
        var dbPath = Path.Combine(folderName, fileName);

        if (!Directory.Exists(pathToSave))
        {
            Directory.CreateDirectory(pathToSave);
        }

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Results.Ok(new { dbPath });
    }
    catch (Exception ex)
    {
        return Results.StatusCode(500);
    }
});

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

#region Seeding and creating database
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

    if (context is null)
        throw new Exception("Database Context Not Found");

    await context.Database.MigrateAsync();

    var seedService = scope.ServiceProvider.GetRequiredService<ISeedDataBase>();
    await seedService.Seed();
}
#endregion

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCustomExceptionHandler();
app.UseCustomLoggingHandler();

try
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        c.RoutePrefix = "swagger";
    });

    Log.Information("Swagger initialized successfully.");
}
catch (Exception ex)
{
    Log.Error(ex, "Swagger UI failed to initialize.");
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiting();
app.UseRateLimiter();
app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();
app.ConfigureGrpcPipeline();

await app.RunAsync();
#endregion

void ConfigureLogging(WebApplicationBuilder webBuilder, SerilogSettings settings)
{
    if (!settings.EnableLogging)
    {
        webBuilder.Logging.ClearProviders();
        webBuilder.Logging.AddConsole();
        webBuilder.Logging.AddDebug();
        return;
    }

    webBuilder.Host.UseSerilog((context, services, configuration) =>
    {
        var env = context.HostingEnvironment;
        var config = context.Configuration;
        var httpContextAccessor = services.GetService<IHttpContextAccessor>();

        configuration
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithCorrelationId()
            .Enrich.WithProperty("Environment", env.EnvironmentName)
            .ReadFrom.Configuration(config);

        if (settings.EnableConsoleLogging)
        {
            configuration.WriteTo.Console();
        }

        if (settings.EnableFileLogging)
        {
            configuration.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
        }

        if (settings.EnableDatabaseLogging)
        {
            configuration.WriteTo.MSSqlServer(
                connectionString: config.GetConnectionString("DBConnection1"),
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = 1,
                    BatchPeriod = TimeSpan.FromSeconds(1)
                },
                columnOptions: new Serilog.Sinks.MSSqlServer.ColumnOptions
                {
                    AdditionalColumns = new List<SqlColumn>
                    {
                        new SqlColumn { ColumnName = "MachineName", PropertyName = "MachineName" },
                        new SqlColumn { ColumnName = "ActionId", PropertyName = "ActionId" },
                        new SqlColumn { ColumnName = "RequestId", PropertyName = "RequestId" },
                        new SqlColumn { ColumnName = "CorrelationId", PropertyName = "CorrelationId" },
                        new SqlColumn { ColumnName = "UserId", PropertyName = "UserId" },
                        new SqlColumn { ColumnName = "Token", PropertyName = "Token" }
                    }
                });
        }
    });
}