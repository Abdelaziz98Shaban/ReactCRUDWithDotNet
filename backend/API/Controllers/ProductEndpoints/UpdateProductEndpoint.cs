using API.Helpers;
using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.productEndpoints;


[Route("Product/Update")]
public class UpdateProductEndpoint : EndpointBaseAsync.WithRequest<ProductInputDto>.WithResult<IActionResult>
{
    private readonly IProductService _productService;
    public UpdateProductEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPut]
    [CheckModelState]
    public override async Task<IActionResult> HandleAsync([FromForm] ProductInputDto request, CancellationToken cancellationToken = default)
    {

        try
        {

            var response = await _productService.Update(request);

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