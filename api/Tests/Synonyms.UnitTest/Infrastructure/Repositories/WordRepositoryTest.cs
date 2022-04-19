using System.Threading.Tasks;
using NUnit.Framework;
using Synonyms.Core.Models;
using Synonyms.Infrastructure.Repositories;
using Synonyms.Test.Utils;

namespace Synonyms.Test.Infrastructure.Repositories;

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