using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataRiskChallenge.Domain.Model;

public class Execution(string identifier, Guid scriptId)
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid(); 
    
    [ForeignKey("Script")]
    public Guid ScriptId { get; init; } = scriptId;
    public string Identifier { get; init; } = identifier;
    public int Status { get; set; } = 0;
    public string Result { get; set; } = string.Empty;
    public DateTime ExecutedAt { get; init; } = DateTime.UtcNow;
    public DateTime CompletedAt { get; set; }
}