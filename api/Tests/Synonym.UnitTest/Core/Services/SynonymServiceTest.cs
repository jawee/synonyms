using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Synonym.Core.Services;
using Synonym.Infra.Repositories;
using Synonym.Test.Utils;

namespace Synonym.Test.Core.Services;

[TestFixture]
public class SynonymServiceTest
{

    [Test]
    public async Task AddSynonym_AddAB_ReturnsB()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new SynonymRepository(ctx);
        var wordLogger = new Mock<ILogger<WordService>>().Object;
        var wordService = new WordService(new WordRepository(ctx), wordLogger);
        var logger = new Mock<ILogger<SynonymService>>().Object;
        var service = new SynonymService(repository, wordService, logger);

        _ = await wordService.CreateWord("A");
        _ = await wordService.CreateWord("B");

        await service.CreateSynonym("A", "B");
        var res = await service.GetSynonymsForWord("A");
        

        Assert.That(res.Count == 1, $"Expected to get 1 synonym for 'b', got {res.Count}");
        Assert.That(res.Contains("b"), "Expected to get 'b' as a synonym for 'a'");

        res = await service.GetSynonymsForWord("B");
        Assert.That(res.Count == 1, $"Expected to get 1 synonym for 'b', got {res.Count}");
        Assert.That(res.Contains("a"), "Expected to get 'a' as a synonym for 'b'");
    }

    [Test]
    public async Task AddSynonym_AddAB_AC_ReturnsBC()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new SynonymRepository(ctx);
        var wordLogger = new Mock<ILogger<WordService>>().Object;
        var wordService = new WordService(new WordRepository(ctx), wordLogger);
        var logger = new Mock<ILogger<SynonymService>>().Object;
        var service = new SynonymService(repository, wordService, logger);

        _ = await wordService.CreateWord("A");
        _ = await wordService.CreateWord("B");
        _ = await wordService.CreateWord("C");
        
        await service.CreateSynonym("A", "B");
        await service.CreateSynonym("A", "C");
        var res = await service.GetSynonymsForWord("A");

        Assert.That(res.Count == 2, $"Expected to get 2 synonyms for 'A', got {res.Count}. {String.Join(", ", res)}");
        Assert.That(res.Contains("b"), "Expected to get 'b' as a synonym for 'a'");
        Assert.That(res.Contains("c"), "Expected to get 'c' as a synonym for 'a'");

        res = await service.GetSynonymsForWord("B");
        Assert.That(res.Count == 2, $"Expected to get 2 synonyms for 'B', got {res.Count}");
        Assert.That(res.Contains("a"), "Expected to get 'a' as a synonym for 'b'");
        Assert.That(res.Contains("c"), "Expected to get 'c' as a synonym for 'b'");
    }

    [Test]
    public async Task AddSynonym_AddAB_AB_ReturnsB()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new SynonymRepository(ctx);
        var wordLogger = new Mock<ILogger<WordService>>().Object;
        var wordService = new WordService(new WordRepository(ctx), wordLogger);
        var logger = new Mock<ILogger<SynonymService>>().Object;
        var service = new SynonymService(repository, wordService, logger);
        
        _ = await wordService.CreateWord("A");
        _ = await wordService.CreateWord("B");

        await service.CreateSynonym("A", "B");
        await service.CreateSynonym("A", "B");
        var res = await service.GetSynonymsForWord("A");

        Assert.That(res.Count == 1, $"Expected to get 1 synonyms for 'a', got {res.Count}");
        Assert.That(res.Contains("b"), "Expected to get 'b' as a synonym for 'a'");

        res = await service.GetSynonymsForWord("B");
        Assert.That(res.Count == 1, $"Expected to get 1 synonyms for 'b', got {res.Count}");
        Assert.That(res.Contains("a"), "Expected to get 'a' as a synonym for 'b'");
    }
    
    [Test]
    public async Task AddSynonym_AddAA__ReturnsEmpty()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new SynonymRepository(ctx);
        var wordLogger = new Mock<ILogger<WordService>>().Object;
        var wordService = new WordService(new WordRepository(ctx), wordLogger);
        var logger = new Mock<ILogger<SynonymService>>().Object;
        var service = new SynonymService(repository, wordService, logger);
        
        _ = await wordService.CreateWord("A");

        await service.CreateSynonym("A", "A");
        var res = await service.GetSynonymsForWord("A");

        Assert.That(res.Count == 0, $"Expected to get 0 synonyms for 'a', got {res.Count}");
    }


    [Test]
    public async Task AddSynonym_AddAB_BC_ReturnsBC()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new SynonymRepository(ctx);
        var wordLogger = new Mock<ILogger<WordService>>().Object;
        var wordService = new WordService(new WordRepository(ctx), wordLogger);
        var logger = new Mock<ILogger<SynonymService>>().Object;
        var service = new SynonymService(repository, wordService, logger);
        
        _ = await wordService.CreateWord("A");
        _ = await wordService.CreateWord("B");
        _ = await wordService.CreateWord("C");
        
        await service.CreateSynonym("A", "B");
        await service.CreateSynonym("B", "C");

        var res = await service.GetSynonymsForWord("A");

        Assert.That(res.Count == 2, $"Expected to get 2 synonyms for 'A', got {res.Count}");
        Assert.That(res.Contains("b"), $"Expected to get 'b' as a synonym for 'a'");
        Assert.That(res.Contains("c"), $"Expected to get 'c' as a synonym for 'a'");
    }
}