using API.Helpers;
using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.CategoryDtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoryEndpoints;


[Route("Category/Update")]
public class UpdateCategoryEndpoint : EndpointBaseAsync.WithRequest<CategoryInputDto>.WithResult<IActionResult>
{
    private readonly ICategoryService _categoryService;
    public UpdateCategoryEndpoint(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPut]
    [CheckModelState]
    public override async Task<IActionResult> HandleAsync([FromForm] CategoryInputDto request, CancellationToken cancellationToken = default)
    {

        try
        {

            var response = await _categoryService.Update(request);

            return Ok(new APIResponse
            {
                Result = response,
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