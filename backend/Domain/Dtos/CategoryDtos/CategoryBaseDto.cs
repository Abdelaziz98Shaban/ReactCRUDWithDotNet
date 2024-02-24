using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.CategoryDtos;

public class CategoryBaseDto
{
    public int Id { get; set; }

    [StringLength(250, MinimumLength = 3)]
    [Required]
    public string Name { get; set; }
}
