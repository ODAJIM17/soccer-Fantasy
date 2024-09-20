using System.ComponentModel.DataAnnotations;

namespace SFS.Domain.Entities;

public class Team
{
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;

    public string? Image { get; set; } = null;
    public Country Country { get; set; } = null!;
    public int CountryId { get; set; }
}