using SalesTransaction.Models;

namespace SalesTransaction.Interfaces;
public interface IProductServices
{
    Task CreateAsync(Product product);
    Task<string> DeleteById(int id);
    List<Product> GetAll();
    List<Product> SelectAll();
    Product SelectById(int id);
    Task UpdateAsync(Product product);
}