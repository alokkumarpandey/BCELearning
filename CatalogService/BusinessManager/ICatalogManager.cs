using CommonEnitity.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.BusinessManager;

public interface ICatalogManager
{
    Task<IEnumerable<CatalogItem>> GetCatalogItemListAsyc();
    Task<CatalogItem> GetCatalogItemByIDAsync(Guid itemID);

    Task CatalogItemAddAsync(CatalogItem objCatalogItem);
    Task CatalogItemUpdateAsync(CatalogItem objCatalogItem);

    Task CatalogDelteItemByIDAsync(Guid CatalogItemID);
}

