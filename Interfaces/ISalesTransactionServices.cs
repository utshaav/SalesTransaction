using SalesTransaction.Models;

namespace SalesTransaction.Interfaces;
public interface ISalesTransactionServices
{
    Task CreateAsync(Sale sale);
    Task<string> DeleteById(int id);
    List<Sale> GetAll();
    List<SalesTransactionVM> SelectAll();
    List<SalesTransactionVM> SelectAllByCustomerId(int customerId, bool fetchAll = false, bool isBilled = false);
    List<SalesTransactionVM> GenerateInvoice(String salesIds, int customerId);
    Sale SelectById(int id);
    Task UpdateAsync(Sale sale);
}

