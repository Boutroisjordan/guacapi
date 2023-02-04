using Microsoft.EntityFrameworkCore;
using GuacAPI.Context.TypeConfigurations;
using GuacAPI.Models;

namespace GuacAPI.Context;

public class DataContext : DbContext
{
    #region Constructor

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AlcoholTypeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppellationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DomainEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FurnisherEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RegionEntityConfiguration());
         modelBuilder.ApplyConfiguration(new ProductOfferEntityConfiguration());
         modelBuilder.ApplyConfiguration(new InvoiceFurnisherEntityConfiguration());
         modelBuilder.ApplyConfiguration(new InvoiceFurnisherProductEntityConfiguration());



        modelBuilder.Entity<ProductOffer>()
        .HasKey(po => new { po.ProductId, po.OfferId });

        modelBuilder.Entity<ProductOffer>()
            .HasOne(po => po.Product)
            .WithMany(p => p.ProductOffers)
            .HasForeignKey(po => po.ProductId);

        modelBuilder.Entity<ProductOffer>()
            .HasOne(po => po.Offer)
            .WithMany(o => o.ProductOffers)
            .HasForeignKey(po => po.OfferId);

        modelBuilder.Entity<InvoiceFurnisherProduct>()
        .HasKey(po => new { po.InvoiceFurnisherId, po.ProductId });

        modelBuilder.Entity<InvoiceFurnisherProduct>()
            .HasOne(cfp => cfp.InvoiceFurnisher)
            .WithMany(cf => cf.InvoicesFurnisherProduct)
            .HasForeignKey(cfp => cfp.InvoiceFurnisherId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<InvoiceFurnisherProduct>()
            .HasOne(cfp => cfp.Product)
            .WithMany(p => p.InvoicesFurnisherProduct)
            .HasForeignKey(ifp => ifp.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        // modelBuilder.Entity<InvoiceFurnisherProduct>()
        //     .HasOne(ifp => ifp.Product);
        //  modelBuilder.Entity<Post>()
        //     .HasMany(p => p.Tags)
        //     .WithMany(p => p.Posts)
        //     .UsingEntity<PostTag>(
        //         j => j
        //             .HasOne(pt => pt.Tag)
        //             .WithMany(t => t.PostTags)
        //             .HasForeignKey(pt => pt.TagId),
        //         j => j
        //             .HasOne(pt => pt.Post)
        //             .WithMany(p => p.PostTags)
        //             .HasForeignKey(pt => pt.PostId),
        //         j =>
        //         {
        //             j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //             j.HasKey(t => new { t.PostId, t.TagId });
        //         });

        //  modelBuilder.Entity<InvoiceFurnisher>()
        //     .HasMany(p => p.Products)
        //     .WithMany()
        //     .UsingEntity<InvoiceFurnisherProduct>(
        //         j => j
        //             .HasOne(pt => pt.Product)
        //             .WithMany()
        //             .HasForeignKey(pt => pt.ProductId),
        //         j => j
        //             .HasOne(pt => pt.InvoiceFurnisher)
        //             .WithMany(p => p.InvoicesFurnisherProduct)
        //             .HasForeignKey(pt => pt.InvoiceFurnisherId),
        //         j =>
        //         {
        //             j.Property(pt => pt.Date).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //             j.HasKey(t => new { t.InvoiceFurnisherId, t.ProductId });
        //         });

//t'as pas besoin de la fk sur le produit go voir la doc microsoft

    }

    

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Furnisher> Furnishers { get; set; } = null!;
    public DbSet<Domain> Domains { get; set; } = null!;
    public DbSet<Region> Regions { get; set; } = null!;
    public DbSet<AlcoholType> AlcoholTypes { get; set; } = null!;
    public DbSet<Appellation> Appellations { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Offer> Offers { get; set; } = null!;
    public DbSet<ProductOffer> ProductOffers {get; set;} = null!;
    public DbSet<InvoiceFurnisher> InvoicesFurnisher {get; set;} = null!;
    public DbSet<InvoiceFurnisherProduct> InvoicesFurnisherProduct {get; set;} = null!;

}
