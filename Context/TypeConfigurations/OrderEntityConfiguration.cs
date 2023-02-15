using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Order");
        //Primary key de la table
        builder.HasKey(item => item.OrderId);

        builder.HasOne(x => x.user)
        .WithMany(y => y.Orders)
        .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.OrderStatus)
        .WithMany(y => y.Orders)
        .HasForeignKey(x => x.OrderStatusId);

        // builder.HasData(
        //     new Region { RegionID = 1, Name = "region 1" }
        // );

    }
    #endregion

}
