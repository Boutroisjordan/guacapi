using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class OfferEntityConfiguration : IEntityTypeConfiguration<Offer>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Offer");
        //Primary key de la table
        builder.HasKey(item => item.OfferId);

        // builder.HasMany(x => x.Comments)
        // .WithOne(item => item.OfferId);

        builder.Property(x => x.Deadline)
        .HasColumnType("date");

    }
    #endregion

}
