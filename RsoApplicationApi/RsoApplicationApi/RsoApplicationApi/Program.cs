using Interfaces;
using NLog;
using RsoApplicationApi.Extensions;
using RsoApplicationApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/logger.cfg"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureApiDma(builder.Configuration);
builder.Services.ConfigureSwagger();

builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();

// Configure the HTTP request pipeline.
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
