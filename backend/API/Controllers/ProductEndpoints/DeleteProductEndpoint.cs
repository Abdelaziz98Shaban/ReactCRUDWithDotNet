using Ardalis.ApiEndpoints;
using Data.Services.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductEndpoints;

[Route("Product/Delete")]
public class DeleteProductEndpoint : EndpointBaseAsync.WithRequest<int>.WithResult<IActionResult>
{

    private readonly IProductService _productService;
    public DeleteProductEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    [HttpDelete]
    public override async Task<IActionResult> HandleAsync([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        try
        {

            var result = await _productService.Delete(id);
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