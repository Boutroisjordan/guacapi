using GuacAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
{
    #region Public Methods

    public void Configure(EntityTypeBuilder<Country> builder)
    {
        // Table
        builder.ToTable("Country");
        
        // Primary Key
        builder.HasKey(item => item.CountryId);
    }
    
    #endregion
}