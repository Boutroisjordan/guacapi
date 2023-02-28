using GuacAPI.Entities;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BCrypt;

namespace GuacAPI.Context.TypeConfigurations;

class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var password = BCrypt.Net.BCrypt.HashPassword("pguvepujbvpùejubpisjvpùisjfiophazjefomughazilmoufbgqkouzgfimgfl");

        builder.HasData(
            new User { UserId = 1, Username = "guacadmin", Email = "guacaprocesi@gmail.com", FirstName="admin", LastName= "admin", Phone= "00000000", PasswordHash=password, VerifyToken=null, VerifiedAt=DateTime.Now, RoleId=1, Address="" }
        );
    }
}

class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(item => item.Name).IsRequired();
        builder.Property(item => item.Description).IsRequired();
        builder.HasData(
            new Role { Id = 1, Name = "Admin", Description = "Administrator" },
            new Role { Id = 2, Name = "Client", Description = "Client web" },
            new Role { Id = 3, Name = "Furnisher", Description = "Fournisseur" }
        );
    }
}