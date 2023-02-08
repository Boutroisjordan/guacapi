using GuacAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Comment");
        //Primary key de la table
        builder.HasKey(item => item.CommentId);

        builder.HasOne(x => x.user)
        .WithMany(x => x.Comments)
        .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.offer)
        .WithMany(x => x.Comments)
        .HasForeignKey(x => x.OfferId)
        .OnDelete(DeleteBehavior.ClientSetNull);

    }
}