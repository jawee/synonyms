using Microsoft.EntityFrameworkCore;
using Serilog;
using Synonym.Core.Repositories;
using Synonym.Core.Services;
using Synonym.Infra.Context;
using Synonym.Infra.Repositories;

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

builder.Services.AddDbContext<SynonymDbContext>(options =>
    options.UseSqlite("DataSource=file::memory:?cache=shared"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SynonymDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();