using Domain.Dtos;
using Domain.Dtos.ProductDtos;

namespace Data.Services.Interfaces;

public interface IProductService
{
    public Task<IEnumerable<ProductDetailsDto>?> GetProductListByCategoryId(ProductFilterDto filterDto);
    public Task<ProductDetailsDto?> GetProduct(int id);
    public Task<APIResponse> Add(ProductInputDto dto);
    public Task<bool> Update(ProductInputDto dto);
    public Task<bool> Delete(int id);
}
