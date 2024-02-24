using Domain.Dtos;
using Domain.Dtos.CategoryDtos;

namespace Data.Services.Interfaces;

public interface ICategoryService
{
    public Task<IEnumerable<CategoryDetailsDto>?> GetCategoryList(Pagination dto);
    public Task<CategoryDetailsDto?> GetCategory(int id);

    public Task<bool> Add(CategoryInputDto dto);
    public  Task<bool> Update(CategoryInputDto dto);
    public Task<bool> IsCategoryExist(int id);
    public  Task<bool> Delete(int id);
}
