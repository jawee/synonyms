namespace Synonym.Core.Models;

public class Synonym
{
    public long Id { get; set; }
    public long Word1Id { get; set; }
    public virtual Word Word1 { get; set; }
    public long Word2Id { get; set; }
    public virtual Word Word2 { get; set; }
}