using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.ProductDtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductEndpoints;


[Route("Product/GetAll")]
public class GetProductsEndpoint : EndpointBaseAsync.WithRequest<ProductFilterDto>.WithResult<IActionResult>
{
    private readonly IProductService _productService;

    public GetProductsEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public override async Task<IActionResult> HandleAsync([FromQuery] ProductFilterDto request,CancellationToken cancellationToken = default)
    {

        try
        {
            var products = await _productService.GetProductListByCategoryId(request);

            return Ok(new APIResponse<IEnumerable<ProductDetailsDto>>
            {
                Result = true,
                Data = products
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