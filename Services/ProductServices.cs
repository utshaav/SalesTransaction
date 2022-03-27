using Microsoft.EntityFrameworkCore;
using SalesTransaction.Data;
using SalesTransaction.Interfaces;
using SalesTransaction.Models;

namespace SalesTransaction.Services;

public class ProductServices : IProductServices
{
    private readonly TestDBContext context;
    public ProductServices(TestDBContext context)
    {
        this.context = context;
    }

    public List<Product> GetAll()
    {
        context.Database.ExecuteSqlRawAsync("");
        return new List<Product>();
    }

    public async Task CreateAsync(Product product)
    {
        try
        {
            string query = StoredProcedureServices.GenerateQueryString(product, "usp_Product_Insert", 1);
            await context.Database.ExecuteSqlRawAsync(query);
            await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(Product product)
    {
        try
        {
            string query = StoredProcedureServices.GenerateQueryString(product, "usp_Product_Insert", 0);
            await context.Database.ExecuteSqlRawAsync(query);
            await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public List<Product> SelectAll()
    {
        List<Product> customers = new List<Product>();
        try
        {
            customers = context.Products.FromSqlRaw($"usp_Product_Select @flag = 1").ToList();
            return customers;
        }
        catch (System.Exception)
        {

            return customers;
        }
    }

    public Product SelectById(int id)
    {
        try
        {
            return context.Products.FromSqlRaw($"usp_Product_Select @flag = 1, @Id = {id}").ToList().FirstOrDefault()!;
        }
        catch (System.Exception)
        {
            return new Product();
        }
    }

    public async Task<string> DeleteById(int id)
    {
        try
        {
            await context.Database.ExecuteSqlRawAsync($"usp_Product_Select @flag = 0, @Id = {id}");
            context.SaveChanges();
            return "Success";
        }
        catch (System.Exception)
        {
            return "Failed";
        }
    }
}
