using Synonyms.Core.Models;

namespace Synonyms.Infrastructure.Context;

public class InMemoryDbContext
{
   private readonly List<Word> _words;
   private readonly List<Synonym> _synonyms;
   private long _wordId = 1;
   private long _synonymId = 1;
   
   public InMemoryDbContext()
   {
      _words = new List<Word>();
      _synonyms = new List<Synonym>();
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

   public void AddSynonym(Synonym entity)
   {
      entity.Id = _synonymId++;
      entity.Word1Id = entity.Word1.Id;
      entity.Word2Id = entity.Word2.Id;
      _synonyms.Add(entity);
   }

   public List<Synonym> GetSynonyms()
   {
      return _synonyms;
   }
}