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
                if (prop.Name.ToLower() == "isbilled")
                {
                    bool check = prop.GetValue(pObject) != null && prop.GetValue(pObject).ToString() == "True";
                    var val = check ? 1 : 0;
                    queryString += $" @{prop.Name} = '{val}'";
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