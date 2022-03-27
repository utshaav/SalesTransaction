using SalesTransaction.Models;

namespace SalesTransaction.Interfaces;

public interface ICustomerServices
{
    Task CreateAsync(Customer customer);
    Task<string> DeleteById(int id);
    List<Customer> GetAll();
    List<Customer> SelectAll();
    Customer SelectById(int id);
    Task UpdateAsync(Customer customer);
}