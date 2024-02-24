using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ProductDtos;

public class ProductBaseDto
{
    public int Id { get; set; }

    [StringLength(250, MinimumLength = 3)]
    [Required]
    public string Name { get; set; }

    [StringLength(500, MinimumLength = 3)]
    [Required]
    public string Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }
}
