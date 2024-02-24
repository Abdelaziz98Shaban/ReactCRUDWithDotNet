using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Data.Contexts;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {


        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {

            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        #region Global Deleted Filter

        Expression<Func<IModifyable, bool>> predicate = e => !e.Deleted; //non deleted shall only be retrived
        foreach (var modelType in builder.Model.GetEntityTypes())
        {
            if (!typeof(IModifyable).IsAssignableFrom(modelType.ClrType)) // the entity doesn't interface from IModifyable thus shall not adhere to soft deleting
                continue;

            var parameter = Expression.Parameter(modelType.ClrType); //converts the model into a type parameter
            var replacedExpression = ReplacingExpressionVisitor.Replace(predicate.Parameters.First(), parameter, predicate.Body); // replaces the IModifyable with the model implementation
            var lambdaExpression = Expression.Lambda(replacedExpression, parameter); //used for constructing an expression tree when the delegate type isn't known at the runtime. 

            modelType.SetQueryFilter(lambdaExpression);
        }
        #endregion

        base.OnModelCreating(builder);

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

}
