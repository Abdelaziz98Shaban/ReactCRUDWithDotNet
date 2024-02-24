using Data.AutoMapper;
using Data.Helpers;
using Data.Repositories;
using Data.Services.Implementations;
using Data.Services.Interfaces;

namespace API.Helpers;

public class RegisterServices
{
    public static void Register(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        AutoMapperConfiguration.RegisterMapping();


        //Model services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddTransient<UnitOfWork>();
        services.AddTransient<UploaderController>();
    }

}
