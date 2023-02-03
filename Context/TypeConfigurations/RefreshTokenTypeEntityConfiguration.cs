using GuacAPI.Entities;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class RefreshTokenTypeEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    #region Public methods

    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
    }

    #endregion
}