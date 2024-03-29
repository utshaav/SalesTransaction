using Microsoft.EntityFrameworkCore;
using SalesTransaction.Data;
using SalesTransaction.Interfaces;
using SalesTransaction.Models;

namespace SalesTransaction.Services;


public class SalesTransactionServices : ISalesTransactionServices
{
    private readonly TestDBContext context;
    public SalesTransactionServices(TestDBContext context)
    {
        this.context = context;
    }

    public List<Sale> GetAll()
    {
        context.Database.ExecuteSqlRawAsync("");
        return new List<Sale>();
    }

    public async Task CreateAsync(Sale sale)
    {
        try
        {
            sale.IsBilled = false;
            string query = StoredProcedureServices.GenerateQueryString(sale, "usp_Sale_Insert", 1);
            await context.Database.ExecuteSqlRawAsync(query);
            await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task UpdateAsync(Sale sale)
    {
        try
        {
            string query = StoredProcedureServices.GenerateQueryString(sale, "usp_Sale_Insert", 0);
            await context.Database.ExecuteSqlRawAsync(query);
            await context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public List<SalesTransactionVM> SelectAll()
    {
        List<SalesTransactionVM> sales = new List<SalesTransactionVM>();
        try
        {
            sales = context.Set<SalesTransactionVM>().FromSqlRaw($"usp_Sale_Select @flag = 1").ToList();
            return sales;
        }
        catch (System.Exception)
        {

            return sales;
        }
    }

    public Sale SelectById(int id)
    {
        try
        {
            return context.Sales.FromSqlRaw($"usp_Sale_Select @flag = 1, @Id = {id}").ToList().FirstOrDefault()!;
        }
        catch (System.Exception)
        {
            return new Sale();
        }
    }

    public async Task<string> DeleteById(int id)
    {
        try
        {
            await context.Database.ExecuteSqlRawAsync($"usp_Sale_Select @flag = 0, @Id = {id}");
            context.SaveChanges();
            return "Success";
        }
        catch (System.Exception)
        {
            return "Failed";
        }
    }

    public List<SalesTransactionVM> SelectAllByCustomerId(int customerId, bool fetchAll = false, bool isBilled = false)
    {
         List<SalesTransactionVM> sales = new List<SalesTransactionVM>();
        int i = 1;
        i = fetchAll ? 0 : i;
        i = isBilled ? 2 : i;
        try
        {
            sales = context.Set<SalesTransactionVM>().FromSqlRaw($"usp_Sale_Select_ByCustomerId @flag = {i}, @Id = {customerId}").ToList();
            return sales;
        }
        catch (System.Exception)
        {

            return sales;
        }
    }

    public List<SalesTransactionVM> GenerateInvoice(String salesIds, int customerId)
    {
        List<SalesTransactionVM> sales = new List<SalesTransactionVM>();
        try
        {
            string query = $"usp_Sale_GenerateInvoice @Id = {customerId}, @SalesIds= '{salesIds}'";
            sales = context.Set<SalesTransactionVM>()
                           .FromSqlRaw(query)
                           .ToList();
            return sales;
        }
        catch (System.Exception)
        {

            return sales;
        }
    }
}