using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class AlcoholTypeEntityConfiguration : IEntityTypeConfiguration<AlcoholType>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<AlcoholType> builder)
    {
        //Nom de la tabmle
        builder.ToTable("alcohol_type");
        //Primary key de la table
        builder.HasKey(item => item.AlcoholTypeId);

        builder.HasData(
            new AlcoholType { AlcoholTypeId = 1, label = "red"},
            new AlcoholType { AlcoholTypeId = 2, label = "grand cru"},
            new AlcoholType { AlcoholTypeId = 3, label = "white"},
            new AlcoholType { AlcoholTypeId = 4, label = "sweet"},
            new AlcoholType { AlcoholTypeId = 5, label = "sparkling"}
        );

    }
    #endregion

}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video