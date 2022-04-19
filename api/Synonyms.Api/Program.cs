using Serilog;
using Synonyms.Core.Repositories;
using Synonyms.Core.Services;
using Synonyms.Infrastructure.Context;
using Synonyms.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISynonymService, SynonymService>();
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<IWordRepository, WordRepository>();
builder.Services.AddScoped<ISynonymRepository, SynonymRepository>();

builder.Services.AddSingleton<InMemoryDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();