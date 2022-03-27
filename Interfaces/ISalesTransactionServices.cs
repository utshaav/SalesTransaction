using SalesTransaction.Models;

namespace SalesTransaction.Interfaces;
public interface ISalesTransactionServices
{
    Task CreateAsync(Sale sale);
    Task<string> DeleteById(int id);
    List<Sale> GetAll();
    List<SalesTransactionVM> SelectAll();
    Sale SelectById(int id);
    Task UpdateAsync(Sale sale);
}
