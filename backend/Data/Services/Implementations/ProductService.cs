using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Helpers;
using Data.Repositories;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.ProductDtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.Implementations;

public class ProductService : IProductService
{
    private readonly UnitOfWork _UoW;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly UploaderController _uploader;

    public ProductService(UnitOfWork uoW, IMapper mapper, UploaderController uploader, ICategoryService categoryService)
    {
        _UoW = uoW;
        _mapper = mapper;
        _uploader = uploader;
        _categoryService = categoryService;
    }

    public async Task<APIResponse> Add(ProductInputDto dto)
    {
        var categoryExist = await _categoryService.IsCategoryExist(dto.CategoryId);
        if (!categoryExist)
            return new APIResponse {  Result = false , Msg ="Category does not exist"};

        var product = _mapper.Map<Product>(dto);

        if (dto.Picture != null)
            product.Image = await _uploader.Upload(dto.Picture);

        await _UoW.ProductRepository.AddAsync(product);
        return new APIResponse { Result = await _UoW.SaveAsync() > 0 };
    }

    public async Task<bool> Update(ProductInputDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        product.UpdatedOnUtc = DateTime.UtcNow;

        if (dto.Picture != null)
            product.Image = await _uploader.Upload(dto.Picture);    // Upload and update image only if a new image is provided

        else if (dto.DeletePicture)
            product.Image = null;    // If DeletePicture flag is true, remove the existing image

        if (dto.Picture == null && !dto.DeletePicture)// If Picture is null and DeletePicture flag is not set, preserve the existing image in the database
            _UoW.ProductRepository.Update(product, e => e.CreatedOnUtc, e => e.CategoryId, e => e.Image);

        else
            _UoW.ProductRepository.Update(product, e => e.CreatedOnUtc, e => e.CategoryId);

        return await _UoW.SaveAsync() > 0;


    }

    public async Task<IEnumerable<ProductDetailsDto>?> GetProductListByCategoryId(ProductFilterDto filterDto)
    {

        var query = _UoW.ProductRepository.GetQuery();

        return await query.Where(e => e.CategoryId == filterDto.CategoryId)
                            .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
                            .Skip(filterDto.Skip ?? 0)
                            .Take(filterDto.Take ?? 10)
                            .ToListAsync();
    }

    public async Task<ProductDetailsDto?> GetProduct(int id)
    {

        var query = _UoW.ProductRepository.GetQuery();

        return await query.Where(e => e.Id == id).ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var Product = await _UoW.ProductRepository.GetById(e => e.Id == id);
        if (Product == null)
            return false;

        Product.Deleted = true;
        return await _UoW.SaveAsync() > 0;
    }
}
