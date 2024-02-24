using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.ProductDtos;

public class ProductInputDto : ProductBaseDto
{
    public IFormFile? Picture { get; set; }
    public bool DeletePicture { get; set; }
}
