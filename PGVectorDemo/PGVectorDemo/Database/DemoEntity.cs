using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pgvector;

namespace PGVectorDemo.Database;

public class DemoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    [Column(TypeName = "vector(3)")]
    public Vector? Embedding { get; set; }
}