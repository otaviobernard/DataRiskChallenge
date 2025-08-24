using DataRiskChallenge.Data;
using DataRiskChallenge.Services;
using Jint;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=Data/datarisk.db"));
builder.Services.AddCors();

builder.Services.AddScoped<Engine>(_ => new Engine());
builder.Services.AddScoped<ScriptExecutorService>();
builder.Services.AddScoped<ExecutionService>();
builder.Services.AddScoped<ScriptService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddJsonOptions(
        x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/swagger.json", "DataRisk API Otavio");
    c.RoutePrefix = "docs";
});

app.UseHttpsRedirection();
app.Run();