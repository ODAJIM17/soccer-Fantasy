using SFS.Domain.Resources;
using System.ComponentModel.DataAnnotations;

namespace SFS.Domain.Entities;

public class Team
{
    public int Id { get; set; }

    [Display(Name = "Country", ResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    public string? Image { get; set; } = null;
    public Country Country { get; set; } = null!;
    public int CountryId { get; set; }
    public string ImageFull => string.IsNullOrEmpty(Image) ? "/images/NoImage.png" : Image;
}