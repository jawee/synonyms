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
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISynonymService, SynonymService>();
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<IWordRepository, WordRepository>();
builder.Services.AddScoped<ISynonymRepository, SynonymRepository>();

builder.Services.AddDbContext<SynonymDbContext>(options =>
    options.UseSqlite("Data Source=SynonymDb.sqlite"));


// builder.Services.AddScoped<SynonymDbContext>(provider => provider.GetRequiredService<SynonymDbContext>());

var app = builder.Build();
// app.UseSerilogRequestLogging(); // <-- Add this line

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SynonymDbContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();