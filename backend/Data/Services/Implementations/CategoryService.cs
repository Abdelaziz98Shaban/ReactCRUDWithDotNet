using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Helpers;
using Data.Repositories;
using Data.Services.Interfaces;
using Domain.Dtos;
using Domain.Dtos.CategoryDtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly UnitOfWork _UoW;
    private readonly IMapper _mapper;
    private readonly UploaderController _uploader;

    public CategoryService(UnitOfWork uoW, IMapper mapper, UploaderController uploader)
    {
        _UoW = uoW;
        _mapper = mapper;
        _uploader = uploader;
    }

    public async Task<bool> Add(CategoryInputDto dto)
    {

        var category = _mapper.Map<Category>(dto);

        if (dto.Picture != null)
            category.Image = await _uploader.Upload(dto.Picture);

        await _UoW.CategoryRepository.AddAsync(category);
        return await _UoW.SaveAsync() > 0;
    }

    public async Task<bool> Update(CategoryInputDto dto)
    {

        var category = _mapper.Map<Category>(dto);
        category.UpdatedOnUtc = DateTime.UtcNow;

        if (dto.Picture != null)
            category.Image = await _uploader.Upload(dto.Picture);    // Upload and update image only if a new image is provided

        else if (dto.DeletePicture)
            category.Image = null;    // If DeletePicture flag is true, remove the existing image

        if (dto.Picture == null && !dto.DeletePicture)// If Picture is null and DeletePicture flag is not set, preserve the existing image in the database
            _UoW.CategoryRepository.Update(category, e => e.CreatedOnUtc, e => e.Image);

        else
            _UoW.CategoryRepository.Update(category, e => e.CreatedOnUtc);


        return await _UoW.SaveAsync() > 0;


    }

    public async Task<IEnumerable<CategoryDetailsDto>?> GetCategoryList(Pagination dto)
    {

        var query = _UoW.CategoryRepository.GetQuery();

        return await query.ProjectTo<CategoryDetailsDto>(_mapper.ConfigurationProvider)
                            .Skip(dto.Skip ?? 0)
                            .Take(dto.Take ?? 10)
                            .ToListAsync();
    }

    public async Task<CategoryDetailsDto?> GetCategory(int id)
    {

        var query = _UoW.CategoryRepository.GetQuery();

        return await query.Where(e => e.Id == id).ProjectTo<CategoryDetailsDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();
    }
    public Task<bool> IsCategoryExist(int id)
    {
        return _UoW.CategoryRepository.AnyAsync(cat => cat.Id == id);
    }

    public async Task<bool> Delete(int id)
    {
        var category = await _UoW.CategoryRepository.GetById(e => e.Id == id);
        if (category == null)
            return false;

        category.Deleted = true;
        return await _UoW.SaveAsync() > 0;
    }
}
