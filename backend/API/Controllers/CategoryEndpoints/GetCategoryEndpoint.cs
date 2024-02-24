using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoryEndpoints;


[Route("Category/Get")]
public class GetCategoryEndpoint : EndpointBaseAsync.WithRequest<int>.WithResult<IActionResult>
{
    private readonly ICategoryService _categoryService;
    public GetCategoryEndpoint(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public override async Task<IActionResult> HandleAsync([FromQuery] int id, CancellationToken cancellationToken = default)
    {

        try
        {

            var category = await _categoryService.GetCategory(id);

            return Ok(new APIResponse<CategoryDetailsDto>
            {
                Result = true,
                Data = category
            });
        }
        catch (Exception)
        {

        }

        return Ok(new APIResponse
        {
            Result = false,
        });
    }


}