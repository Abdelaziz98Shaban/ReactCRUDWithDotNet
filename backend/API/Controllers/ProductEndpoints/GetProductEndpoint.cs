using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CategoryEndpoints;


[Route("Product/Get")]
public class GetProductEndpoint : EndpointBaseAsync.WithRequest<int>.WithResult<IActionResult>
{
    private readonly IProductService _productService;
    public GetProductEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public override async Task<IActionResult> HandleAsync([FromQuery] int id, CancellationToken cancellationToken = default)
    {

        try
        {

            var product = await _productService.GetProduct(id);

            return Ok(new APIResponse<ProductDetailsDto>
            {
                Result = true,
                Data = product
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