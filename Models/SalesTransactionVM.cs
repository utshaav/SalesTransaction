namespace SalesTransaction.Models;
public class SalesTransactionVM
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? ProductName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public bool IsBilled { get; set; }
}