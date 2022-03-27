using SalesTransaction.Data;
using SalesTransaction.Models;
using Microsoft.EntityFrameworkCore;
using SalesTransaction.Interfaces;

namespace SalesTransaction.Services;
public class CustomerServices : ICustomerServices
{
    private readonly TestDBContext context;
    public CustomerServices(TestDBContext context)
    {
        this.context = context;
    }

    public List<Customer> GetAll()
    {
        context.Database.ExecuteSqlRawAsync("");
        return new List<Customer>();
    }

    public async Task CreateAsync(Customer customer)
    {
        try
        {
            string query = StoredProcedureServices.GenerateQueryString(customer, "usp_Customer_Insert", 1);
            await context.Database.ExecuteSqlRawAsync(query);
            await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(Customer customer)
    {
        try
        {
            string query = StoredProcedureServices.GenerateQueryString(customer, "usp_Customer_Insert", 0);
            await context.Database.ExecuteSqlRawAsync(query);
            await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public List<Customer> SelectAll()
    {
        List<Customer> customers = new List<Customer>();
        try
        {
            customers = context.Customers.FromSqlRaw($"usp_Customer_Select @flag = 1").ToList();
            return customers;
        }
        catch (System.Exception )
        {
            return customers;
        }
    }

    public Customer SelectById(int id)
    {
        try
        {
            return context.Customers.FromSqlRaw($"usp_Customer_Select @flag = 1, @Id = {id}").ToList().FirstOrDefault()!;
        }
        catch (System.Exception)
        {
            return new Customer();
        }
    }

    public async Task<string> DeleteById(int id)
    {
        try
        {
            await context.Database.ExecuteSqlRawAsync($"usp_Customer_Select @flag = 0, @Id = {id}");
            context.SaveChanges();
            return "Success";
        }
        catch (System.Exception)
        {
            return "Failed";
        }
    }
}

//usp_Customer_Select
