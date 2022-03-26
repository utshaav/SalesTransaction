namespace SalesTransaction.Data;
public class DataAccessLayer
{
    private readonly string connectionString;

    public DataAccessLayer(IConfiguration config)
    {
        connectionString = config.GetConnectionString("Default");
    }

    
}