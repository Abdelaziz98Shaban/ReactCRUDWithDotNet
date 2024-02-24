using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.CategoryDtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoryEndpoints;


[Route("Category/GetAll")]
public class GetCategoriesEndpoint : EndpointBaseAsync.WithRequest<Pagination>.WithResult<IActionResult>
{
    private readonly ICategoryService _categoryService;
    public GetCategoriesEndpoint(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public override async Task<IActionResult> HandleAsync([FromQuery] Pagination request, CancellationToken cancellationToken = default)
    {

        try
        {
        
            var categories = await _categoryService.GetCategoryList(request);

            return Ok(new APIResponse<IEnumerable<CategoryDetailsDto>>
            {
                Result = true,
                Data = categories
            });
        }
        catch (Exception)
        {

        }

        return Ok(new APIResponse
        {
            Result =false, 
        });
    }


}