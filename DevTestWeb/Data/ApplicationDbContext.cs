using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CommonEnitity.Catalog;

namespace DevTestWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CommonEnitity.Catalog.CatalogItem> CatalogItem { get; set; } = default!;
        public DbSet<CommonEnitity.Catalog.CatalogBrand> CatalogBrand { get; set; } = default!;
        public DbSet<CommonEnitity.Catalog.CatalogType> CatalogType { get; set; } = default!;
    }
}