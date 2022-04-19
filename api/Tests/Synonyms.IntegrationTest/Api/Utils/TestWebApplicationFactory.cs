using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Synonyms.Core.Models;
using Synonyms.Infrastructure.Context;

namespace Synonyms.IntegrationTest.Api.Utils;

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
            var db = scopedServices.GetRequiredService<InMemoryDbContext>();
            
            var a = new Word {Value = "a"};
            var b = new Word {Value = "b"};
            var c = new Word {Value = "c"};
            var d = new Word {Value = "d"};
            db.AddWord(a);
            db.AddWord(b);
            db.AddWord(c);
            db.AddWord(d);
            var syn1 = new Synonym
            {
                Word1 = a,
                Word2 = b
            };
            var syn2 = new Synonym
            {
                Word1 = b,
                Word2 = a
            };
            db.AddSynonym(syn1);
            db.AddSynonym(syn2);
        }

        return host;
    }
}