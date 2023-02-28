using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class InvoiceFurnisherEntityConfiguration : IEntityTypeConfiguration<InvoiceFurnisher>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<InvoiceFurnisher> builder)
    {
        //Nom de la tabmle
        builder.ToTable("InvoiceFurnisher");
        //Primary key de la table
        builder.HasKey(item => item.InvoiceFurnisherId);


        // builder.HasOne(f => f.Furnisher)
        //     .WithMany(f => f.Invoices)
        //     .HasForeignKey(f => f.FurnisherId);
        // builder.HasMany(f => f.InvoicesFurnisherProduct)
        //     .WithOne(fp => fp.InvoiceFurnisher)
        //     .HasForeignKey(fp => fp.InvoiceFurnisherId);

    }
    #endregion

}
