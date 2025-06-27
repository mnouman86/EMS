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
using Castle.DynamicProxy;
using CleanArc.WebFramework.Interceptor;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using CleanArc.Application.Common;
using System.Net.Http.Headers;
using CleanArc.Web.Api;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new TimeModelBinderProvider());
});
//builder.Host.UseSerilog(LoggingConfiguration.ConfigureLogger);

// Read Serilog settings from configuration
var serilogSettings = builder.Configuration.GetSection("SerilogSettings").Get<SerilogSettings>();

builder.Host.UseSerilog();
//builder.Host.UseKestrel(options =>
//{
//    options.AllowSynchronousIO = true;
//}); 

var configuration = builder.Configuration;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

builder.Services.Configure<IdentitySettings>(configuration.GetSection(nameof(IdentitySettings)));

var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
var emailSettings = configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();
//builder.Services.AddSingleton<IProxyGenerator, ProxyGenerator>();
//builder.Services.AddScoped<LoggingInterceptor>(provider =>
//{
//    var logger = provider.GetRequiredService<ILogger<LoggingInterceptor>>();
//    return new LoggingInterceptor(logger);
//});
//builder.Services.AddScoped<ILogger<LoggingInterceptor>, Logger<LoggingInterceptor>>();
//builder.Services.AddScoped<LoggingInterceptor>();

//builder.Services.AddScoped<ICustomHttpContext, CustomHttpContextWrapper>();

builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
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
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwagger();

builder.Services.AddApplicationServices()
    .RegisterIdentityServices(identitySettings,configuration)
    .AddPersistenceServices(configuration)
    .AddWebFrameworkServices(configuration);

builder.Services.RegisterValidatorsAsServices();

 
#region Plugin Services Configuration

builder.Services.ConfigureGrpcPluginServices();

#endregion

builder.Services.AddAutoMapper(typeof(User), typeof(JwtService), typeof(UserController));

var app = builder.Build();

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
configureLogging(app.Services.GetRequiredService<IHttpContextAccessor>());


//IConfiguration _configuration = new ConfigurationBuilder()
//                        .SetBasePath(Directory.GetCurrentDirectory())
//                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
//               .Build();

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

#region Pipleline Configuration

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCustomExceptionHandler();
app.UseCustomLoggingHandler();

//app.UseSwaggerAndUI();
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
//app.UseMiddleware<RateLimitingMiddleware>();
app.MapControllers();

app.ConfigureGrpcPipeline();

await app.RunAsync();
#endregion

void configureLogging(IHttpContextAccessor httpContextAccessor)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{environment}.json", optional: true
    ).Build();
    

    Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .Enrich.WithThreadId()
    .Enrich.WithCorrelationId()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    //.WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
    .WriteTo.MSSqlServer(
            connectionString: configuration.GetConnectionString("DBConnection1"),
            sinkOptions: new MSSqlServerSinkOptions
            {
                TableName = "Logs", // Customize the table name
                AutoCreateSqlTable = true,
                BatchPostingLimit = 1, // Adjust batch posting limit as needed
                BatchPeriod = TimeSpan.FromSeconds(1), // Adjust period as needed                
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

                    // Add more columns as needed
                }
            })
    .Enrich.WithProperty("Environment", environment)
    //.Enrich.WithProperty("UserId", GetUserIdFromContext(httpContextAccessor)) // Add UserId to log properties
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
}
string GetUserIdFromContext(IHttpContextAccessor httpContextAccessor)
{
    var userId = "Unknown"; // Set a default value if UserId is not available

    var httpContext = httpContextAccessor.HttpContext;
    if (httpContext?.User?.Identity?.IsAuthenticated == true)
    {
        userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    return userId;
}
