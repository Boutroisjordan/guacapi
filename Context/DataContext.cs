using Microsoft.EntityFrameworkCore;
using GuacAPI.Context.TypeConfigurations;
using GuacAPI.Entities;
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
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
         modelBuilder.ApplyConfiguration(new InvoiceFurnisherEntityConfiguration());
         modelBuilder.ApplyConfiguration(new InvoiceFurnisherProductEntityConfiguration());
         modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
         modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
         modelBuilder.ApplyConfiguration(new OrderStatusEntityConfiguration());

         //Product offer many to many

        modelBuilder.Entity<ProductOffer>()
            .HasKey(po => new { po.ProductId, po.OfferId });

        modelBuilder.Entity<ProductOffer>()
            .HasOne(po => po.Product)
            .WithMany(p => p.ProductOffers)
            .HasForeignKey(po => po.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductOffer>()
            .HasOne(po => po.Offer)
            .WithMany(o => o.ProductOffers)
            .HasForeignKey(po => po.OfferId)
            .OnDelete(DeleteBehavior.Cascade);


//Invoices furnisher many to many
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


        modelBuilder.Entity<OrderOffer>()
        .HasKey(po => new { po.OrderId, po.OfferId });

        modelBuilder.Entity<OrderOffer>()
            .HasOne(cfp => cfp.order)
            .WithMany(cf => cf.OrderOffers)
            .HasForeignKey(cfp => cfp.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderOffer>()
            .HasOne(cfp => cfp.offer)
            .WithMany()
            .HasForeignKey(ifp => ifp.OfferId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>().HasOne(u => u.RefreshToken).WithOne(rt => rt.User).HasForeignKey<RefreshToken>(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<Furnisher> Furnishers { get; set; }
    public DbSet<Domain> Domains { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<AlcoholType> AlcoholTypes { get; set; }
    public DbSet<Appellation> Appellations { get; set; }
    public DbSet<User> Users { get; set; }


    public DbSet<Offer> Offers { get; set; }
    public DbSet<ProductOffer> ProductOffers {get; set;}
    public DbSet<InvoiceFurnisher> InvoicesFurnisher {get; set;}
    public DbSet<InvoiceFurnisherProduct> InvoicesFurnisherProduct {get; set;}
    public DbSet<Comment> Comments {get; set;}
    public DbSet<Order> Orders {get; set;}
    public DbSet<OrderStatus> OrderStatus {get; set;}
    public DbSet<OrderOffer> OrderOffers {get; set;}
    public DbSet<RefreshToken> RefreshToken { get;  set; }

}

