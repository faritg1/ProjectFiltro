using System.Reflection;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Data;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
// var logger = new LoggerConfiguration()
// 					.ReadFrom.Configuration(builder.Configuration)
// 					.Enrich.FromLogContext()
// 					.CreateLogger();

// Add services to the container.

/* builder.Services.AddControllers(options => {
    options.RespectBrowserAcceptHeader = true;
}).AddXmlSerializerFormatters(); */

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddJwt(builder.Configuration); */

builder.Services.AddDbContext<ProyectoJardineriaContext>(options =>
{
    string  connectionString = builder.Configuration.GetConnectionString("MySqlConex");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try{
        var context = services.GetRequiredService<ProyectoJardineriaContext>();
        await context.Database.MigrateAsync();
    }
    catch(Exception ex)
    {
        var _logger = loggerFactory.CreateLogger<ProyectoJardineriaContext>();
        _logger.LogError(ex, "Ocurrio un error durante la migracion");
    }
}


/* app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication(); */
app.UseAuthorization();

app.MapControllers();

app.Run();
