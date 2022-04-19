using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Synonyms.Core.Models;
using Synonyms.Infrastructure.Repositories;
using Synonyms.Test.Utils;

namespace Synonyms.Test.Infrastructure.Repositories;

[TestFixture]
public class SynonymRepositoryTest
{
    [Test]
    public async Task TestCreateSynonym()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new SynonymRepository(ctx);
        var word1 = new Word {Value = "A"};
        var word2 = new Word {Value = "B"};
        ctx.AddWord(word1);
        ctx.AddWord(word2);

        var synonym = new Synonym
        {
            Word1 = word1,
            Word2 = word2
        };
        await repository.AddAsync(synonym);
        
        Assert.That(synonym.Id != -1);
        Assert.That(ctx.GetSynonyms().FirstOrDefault() != null);
    }
    
    [Test]
    public async Task TestGetSynonymsForWord()
    {
        var ctx = TestSynonymDbContext.GetTestDbContext();
        var repository = new SynonymRepository(ctx);
        
        var word1 = new Word {Value = "A"};
        var word2 = new Word {Value = "B"};
        ctx.AddWord(word1);
        ctx.AddWord(word2);

        var synonym = new Synonym
        {
            Word1 = word1,
            Word2 = word2
        };
        await repository.AddAsync(synonym);

        var res = await repository.GetSynonymsForWord(word1);
        Assert.That(res.Count == 1, $"Expected number of synonyms to be 1, got {res.Count}.");
        Assert.That(res.First().Value.Equals("B"), $"Expected to get 'B', got {res.First().Value}");
    }
}