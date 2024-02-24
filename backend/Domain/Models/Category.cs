using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Category : BaseEntity
{

    [StringLength(250, MinimumLength = 3)]
    [Required]
    public string Name { get; set; }

    public string? Image { get; set; }
}
