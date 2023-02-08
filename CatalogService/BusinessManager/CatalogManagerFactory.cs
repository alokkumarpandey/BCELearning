using CatalogService.SQLDataProvider;

namespace CatalogService.BusinessManager;

public class CatalogManagerFactory
{
    public static string _catalogManagerType = "";
    public static ICatalogManager Create()
    {
        ICatalogManager objManager;
        if (string.IsNullOrEmpty(_catalogManagerType))
        {
             objManager = new CatalogManager();            
        }
        else
        {
            //we can create  new manger object as per need
            objManager = new CatalogManager();
        }
        return objManager;
    }
}

