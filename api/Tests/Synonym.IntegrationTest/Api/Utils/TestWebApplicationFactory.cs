using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Synonym.Core.Models;
using Synonym.Infra.Context;

namespace Synonym.IntegrationTest.Api.Utils;

public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = builder.Build();
        host.Start();
        
        var serviceProvider = host.Services;

        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<SynonymDbContext>();
            
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            
            db.Words.RemoveRange(db.Words);
            db.Synonyms.RemoveRange(db.Synonyms);
            
            var a = new Word {Value = "a"};
            var b = new Word {Value = "b"};
            var c = new Word {Value = "c"};
            var d = new Word {Value = "d"};
            db.Add(a);
            db.Add(b);
            db.Add(c);
            db.Add(d);
            var syn1 = new Synonym.Core.Models.Synonym
            {
                Word1 = a,
                Word2 = b
            };
            var syn2 = new Synonym.Core.Models.Synonym
            {
                Word1 = b,
                Word2 = a
            };
            db.Add(syn1);
            db.Add(syn2);
            db.SaveChanges();
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(SynonymDbContext));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<SynonymDbContext>(options =>
                {
                    options.UseSqlite("DataSource=file::memory:");
                });
            });
        
    }
}