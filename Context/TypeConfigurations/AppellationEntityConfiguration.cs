using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class AppellationEntityConfiguration : IEntityTypeConfiguration<Appellation>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Appellation> builder)
    {
        //Nom de la tabmle
        builder.ToTable("appellation");
        //Primary key de la table
        builder.HasKey(item => item.AppellationId);

        builder.HasData(
            new Appellation {AppellationId = 1, Name ="montrachet"},
            new Appellation {AppellationId = 2, Name ="meursault"},
            new Appellation {AppellationId = 3, Name ="corton-charlemagne"},
            new Appellation {AppellationId = 4, Name ="chardonnay"},
            new Appellation {AppellationId = 5, Name ="other"},
            new Appellation {AppellationId = 6, Name ="blanc"},
            new Appellation {AppellationId = 7, Name ="riesling"},
            new Appellation {AppellationId = 8, Name ="corton"},
            new Appellation {AppellationId = 9, Name ="chablis"},
            new Appellation {AppellationId = 10, Name ="puligny-montrachet"},
            new Appellation {AppellationId = 11, Name ="bordeaux"},
            new Appellation {AppellationId = 12, Name ="albariño"},
            new Appellation {AppellationId = 13, Name ="chassagne-montrachet"},
            new Appellation {AppellationId = 14, Name ="châteauneuf-du-pape"},
            new Appellation {AppellationId = 15, Name ="cuvée"},
            new Appellation {AppellationId = 16, Name ="alsace"},
            new Appellation {AppellationId = 17, Name ="rouge"},
            new Appellation {AppellationId = 18, Name ="viognier"},
            new Appellation {AppellationId = 19, Name ="hermitage"},
            new Appellation {AppellationId = 20, Name ="bonnezeaux"},
            new Appellation {AppellationId = 21, Name ="sancerre"},
            new Appellation {AppellationId = 22, Name ="rioja"},
            new Appellation {AppellationId = 23, Name ="roussanne"}

        );
    }
    #endregion

}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video