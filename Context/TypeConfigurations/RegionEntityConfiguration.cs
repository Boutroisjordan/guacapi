using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class RegionEntityConfiguration : IEntityTypeConfiguration<Region>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Region");
        //Primary key de la table
        builder.HasKey(item => item.RegionID);

        builder.HasData(
            new Region { RegionID = 1, Name = "region 1" }
        );

    }
    #endregion

}
