namespace SalesTransaction.Services;
public static class StoredProcedureServices
{
    public static string GenerateQueryString(object pObject, string spName, byte flag)
    {
        string queryString = $"EXEC {spName} @flag = {flag}";
        if (pObject != null)
        {
            foreach (var prop in pObject.GetType().GetProperties())
            {
                queryString += ",";
                
                if (prop.PropertyType.Name == "int")
                {
                    queryString += $" @{prop.Name} = {prop.GetValue(pObject)}";
                }
                else
                {
                    queryString += $" @{prop.Name} = '{prop.GetValue(pObject)}'";
                }
            }
        }
        return queryString;
    }
}