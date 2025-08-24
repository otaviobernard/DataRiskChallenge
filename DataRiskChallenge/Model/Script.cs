using System.ComponentModel.DataAnnotations;

namespace DataRiskChallenge.Domain.Model;

public class Script(string identifier, string content)
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public string Identifier { get; set; } = identifier;

    [Required]
    public string Content { get; set; } = content;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}