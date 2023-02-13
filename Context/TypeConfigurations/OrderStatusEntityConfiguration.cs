using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class OrderStatusEntityConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        //Nom de la tabmle
        builder.ToTable("OrderStatus");
        //Primary key de la table
        builder.HasKey(item => item.OrderStatusId);

        builder.HasData(
            new OrderStatus { OrderStatusId = 1, OrderStatusName = "Non payer" },
            new OrderStatus { OrderStatusId = 2, OrderStatusName = "Payment refuser" },
            new OrderStatus { OrderStatusId = 3, OrderStatusName = "Payed" },
            new OrderStatus { OrderStatusId = 4, OrderStatusName = "En attente de Livraison" },
            new OrderStatus { OrderStatusId = 5, OrderStatusName = "Livré" },
            new OrderStatus { OrderStatusId = 6, OrderStatusName = "Annuler" }
        );
    }
    #endregion

}
