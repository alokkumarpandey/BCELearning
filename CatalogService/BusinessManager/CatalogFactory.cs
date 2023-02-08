using CatalogService.SQLDataProvider;

namespace CatalogService.BusinessManager;

public class CatalogFactory
{
    private static string _providerType = "SQLDataProvider";

    private static Dictionary<string, ICatalogDataProvider> objProviderDic
            = new Dictionary<string, ICatalogDataProvider>();

    public CatalogFactory(string providerType)
    {
        _providerType = providerType;
    }

    public static ICatalogDataProvider Create()
    {
        //Lazy Loading Pattern Or Eager Loading
        if (objProviderDic.Count == 0)
        {
            objProviderDic.Add("SQLDataProvider", new CatalogDataProvider());
            objProviderDic.Add("MySQLDataProvider", new CatalogDataProvider());
        }
        //Use RIP pattern to get rid of IF
        //Replace IF with Ploy
        return objProviderDic[_providerType];
        //if (custType == "SQLDataProvider")
        //{
        //    return new CatalogDataProvider();
        //}
        //else
        //{
        //    return new MySQLDataProvider();
        //}
    }
}

