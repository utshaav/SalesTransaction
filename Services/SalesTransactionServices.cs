using SalesTransaction.Data;

namespace SalesTransaction.Services;
public class SalesTransactionServices
{
    private readonly TestDBContext context;
    public SalesTransactionServices(TestDBContext context)
    {
        this.context = context;
    }
}