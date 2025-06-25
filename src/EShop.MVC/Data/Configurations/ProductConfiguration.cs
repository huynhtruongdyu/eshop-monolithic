using EShop.MVC.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.MVC.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        private const string TableName = nameof(Product);

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(p => p.Id);
        }
    }
}