using Data.Contexts;
using Domain.IRepositories;
using Domain.Models;
namespace Data.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
    }


    #region Product
    private IBaseRepository<Product> _productRepository;

    public IBaseRepository<Product> ProductRepository
    {

        get
        {
            if (_productRepository == null)
                _productRepository = new BaseRepository<Product>(_db);
            return _productRepository;
        }
    }
    #endregion

    #region Product
    private IBaseRepository<Category> _categoryRepository;

    public IBaseRepository<Category> CategoryRepository
    {

        get
        {
            if (_categoryRepository == null)
                _categoryRepository = new BaseRepository<Category>(_db);
            return _categoryRepository;
        }
    }
    #endregion


    public async Task<int> SaveAsync()
    {
     return await  _db.SaveChangesAsync();
    }


}

