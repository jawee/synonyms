using Synonym.Core.Models;

namespace Synonym.Infrastructure.Context;

public class InMemoryDbContext
{
   private List<Word> _words;
   private List<Core.Models.Synonym> _synonyms;
   private long _wordId = 1;
   private long _synonymId = 1;
   
   public InMemoryDbContext()
   {
      _words = new List<Word>();
      _synonyms = new List<Core.Models.Synonym>();
   }

   public void AddWord(Word entity)
   {
      entity.Id = _wordId++;
      _words.Add(entity);
   }

   public List<Word> GetWords()
   {
      return _words;
   }

   public void AddSynonym(Core.Models.Synonym entity)
   {
      entity.Id = _synonymId++;
      entity.Word1Id = entity.Word1.Id;
      entity.Word2Id = entity.Word2.Id;
      _synonyms.Add(entity);
   }

   public List<Core.Models.Synonym> GetSynonyms()
   {
      return _synonyms;
   }
}