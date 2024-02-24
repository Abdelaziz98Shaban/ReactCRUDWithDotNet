using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.CategoryDtos;

public class CategoryInputDto : CategoryBaseDto
{
    public IFormFile? Picture { get; set; }
    public bool DeletePicture { get; set; }
}
