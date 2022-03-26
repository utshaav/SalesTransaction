using SalesTransaction.Data;

namespace SalesTransaction.Services;
public class ProductServices
{
    private readonly TestDBContext context;
    public ProductServices(TestDBContext context)
    {
        this.context = context;

    }
}
