using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Synonym.Core.Models;
using Synonym.Core.Services;
using Synonym.Infra.Repositories;
using Synonym.Test.Utils;

namespace Synonym.Test.Core.Services;

[TestFixture]
public class WordServiceTest
{
    [Test]
    public async Task TestGetWordByString_A_NotFound()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var logger = new Mock<ILogger<WordService>>().Object;
        var service = new WordService(new WordRepository(ctx), logger);

        var res = await service.GetWordByString("a");

        Assert.That(res == null, $"Res is not null, expected null");
    }
    [Test]
    public async Task TestGetWordByString_A_ReturnsA()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        await ctx.AddAsync(new Word {Value = "a"});
        await ctx.SaveChangesAsync();
        var repository = new WordRepository(ctx);
        var logger = new Mock<ILogger<WordService>>().Object;
        var service = new WordService(new WordRepository(ctx), logger);

        var res = await service.GetWordByString("a");

        Assert.That(res != null, $"Res is null, expected not null");
        Assert.That(res is {Value: "a"}, $"Expected res to be 'a', got {res?.Value}");
    }
    
    [Test]
    public async Task TestCreateWord_A()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new WordRepository(ctx);
        var logger = new Mock<ILogger<WordService>>().Object;
        var service = new WordService(repository, logger);

        var res = await service.CreateWord("a");

        Assert.That(res != null, $"Res is null, expected not null");
        Assert.That(res is {Value: "a"}, $"Expected res to be 'a', got {res?.Value}");
        Assert.That(ctx.Words.Count() == 1, $"Expected ctx.Words to only contain 1 object, contains {ctx.Words.Count()}");
    }
    [Test]
    public async Task TestCreateWord_A_A_OnlyOneCreated()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new WordRepository(ctx);
        var logger = new Mock<ILogger<WordService>>().Object;
        var service = new WordService(repository, logger);

        _ = await service.CreateWord("a");
        var res = await service.CreateWord("a");

        Assert.That(res != null, $"Res is null, expected not null");
        Assert.That(res is {Value: "a"}, $"Expected res to be 'a', got {res?.Value}");
        Assert.That(ctx.Words.Count() == 1, $"Expected ctx.Words to only contain 1 object, contains {ctx.Words.Count()}");
    }
}