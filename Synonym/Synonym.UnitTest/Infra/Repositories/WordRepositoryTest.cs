using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Synonym.Core.Models;
using Synonym.Infra.Context;
using Synonym.Infra.Repositories;
using Synonym.Test.Utils;

namespace Synonym.Test.Infra.Repositories;

[TestFixture]
public class WordRepositoryTest
{
    
    
    [Test]
    public async Task TestAdd()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new WordRepository(ctx);

        var word = new Word {Value = "A"};
        await repository.AddAsync(word);

        Assert.That(word.Id != -1, $"Expected Id to be '1', got '{word.Id}'");
        Assert.That(word.Value.Equals("A"), $"Expected Value to be 'A', got '{word.Value}'");
    }

    [Test]
    public async Task TestAddTwoSameValue_ThrowsDbUpdateException()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new WordRepository(ctx);

        var word = new Word {Value = "A"};
        await repository.AddAsync(word);
        var word2 = new Word {Value = "A"};
        
        Assert.ThrowsAsync(typeof(DbUpdateException), () => repository.AddAsync(word2), "Expected DbUpdateException");
    }

    [Test]
    public async Task TestGetById()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new WordRepository(ctx);

        var word = new Word {Value = "A"};
        await repository.AddAsync(word);

        var res = await repository.GetByIdAsync(word.Id);
        if (res is null)
        {
            Assert.Fail("Expected to get a result.");
        }
        Assert.That(word.Id == res?.Id, "Expected Id to be equal.");
        Assert.That(word.Value.Equals(res?.Value), "Expected Value to be equal.");
    }

    [Test]
    public async Task TestGetWordByString()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new WordRepository(ctx);

        var word = new Word {Value = "A"};
        await repository.AddAsync(word);

        var res = await repository.GetWordByString("A");
        
        Assert.That(res != null, "Expected result to be not null.");
        Assert.That(word.Id == res?.Id, "Expected Id to be equal.");
        Assert.That(word.Value.Equals(res?.Value), "Expected Value to be equal.");
    }

}