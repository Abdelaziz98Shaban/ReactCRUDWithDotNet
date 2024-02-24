using Ardalis.ApiEndpoints;
using Azure.Core;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoryEndpoints;

[Route("Category/Delete")]
public class DeleteCategoryEndpoint : EndpointBaseAsync.WithRequest<int>.WithResult<IActionResult>
{

    private readonly ICategoryService _categoryService;
    public DeleteCategoryEndpoint(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpDelete]
    public override async Task<IActionResult> HandleAsync([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        try
        {

            var result = await _categoryService.Delete(id);
            return Ok(new APIResponse
            {
                Result = result,
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