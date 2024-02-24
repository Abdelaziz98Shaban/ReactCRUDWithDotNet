using API.Helpers;
using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoryEndpoints;


[Route("Product/Add")]
public class CreateProductEndpoint : EndpointBaseAsync.WithRequest<ProductInputDto>.WithResult<IActionResult>
{
    private readonly IProductService _productService;
    public CreateProductEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [CheckModelState]
    public override async Task<IActionResult> HandleAsync([FromForm] ProductInputDto request, CancellationToken cancellationToken = default)
    {

        try
        {
        
            var response = await _productService.Add(request);

            return Ok(response);
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