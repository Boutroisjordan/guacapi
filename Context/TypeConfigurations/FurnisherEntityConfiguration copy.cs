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
        
    }
    #endregion

}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video