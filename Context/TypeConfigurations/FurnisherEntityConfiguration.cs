using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class FurnisherEntityConfiguration : IEntityTypeConfiguration<Furnisher>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Furnisher> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Furnisher");
        //Primary key de la table
        builder.HasKey(item => item.FurnisherId);

        builder.HasMany(f => f.Invoices)
            .WithOne(f => f.Furnisher)
            .HasForeignKey(f => f.FurnisherId);
    }

    // protected override void OnModelCreating()
    #endregion

}

