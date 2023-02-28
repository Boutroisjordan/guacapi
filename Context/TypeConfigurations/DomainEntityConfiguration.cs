using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class DomainEntityConfiguration : IEntityTypeConfiguration<Domain>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Domain> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Domain");
        //Primary key de la table
        builder.HasKey(item => item.DomainId);

        builder.HasData(
            new Domain { DomainId = 1, Name = "Domaine 1"}
        );

    }
    #endregion

}
