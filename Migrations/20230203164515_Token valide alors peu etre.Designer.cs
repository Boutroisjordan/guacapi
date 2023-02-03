// <auto-generated />
using System;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuacAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230203154542_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GuacAPI.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("GuacAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GuacAPI.Models.AlcoholType", b =>
                {
                    b.Property<int>("AlcoholTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlcoholTypeId"));

                    b.Property<string>("label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlcoholTypeId");

                    b.ToTable("alcohol_type", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Appellation", b =>
                {
                    b.Property<int>("AppellationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppellationId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppellationId");

                    b.ToTable("appellation", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Domain", b =>
                {
                    b.Property<int>("DomainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DomainId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DomainId");

                    b.ToTable("Domain", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Furnisher", b =>
                {
                    b.Property<int>("FurnisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FurnisherId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Siret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FurnisherId");

                    b.ToTable("Furnisher", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Offer", b =>
                {
                    b.Property<int>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfferId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("OfferId");

                    b.ToTable("Offer", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<float>("AlcoholDegree")
                        .HasColumnType("real");

                    b.Property<int>("AlcoholTypeId")
                        .HasColumnType("int");

                    b.Property<int>("AppellationId")
                        .HasColumnType("int");

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<int>("FurnisherId")
                        .HasColumnType("int");

                    b.Property<int>("Millesime")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("AlcoholTypeId");

                    b.HasIndex("AppellationId");

                    b.HasIndex("DomainId");

                    b.HasIndex("FurnisherId");

                    b.HasIndex("RegionId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.ProductOffer", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityProduct")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "OfferId");

                    b.HasIndex("OfferId");

                    b.ToTable("ProductOffer", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Region", b =>
                {
                    b.Property<int>("RegionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegionID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionID");

                    b.ToTable("Region", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Entities.RefreshToken", b =>
                {
                    b.HasOne("GuacAPI.Entities.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GuacAPI.Models.Product", b =>
                {
                    b.HasOne("GuacAPI.Models.AlcoholType", "alcoholType")
                        .WithMany("Products")
                        .HasForeignKey("AlcoholTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuacAPI.Models.Appellation", "appellation")
                        .WithMany("Products")
                        .HasForeignKey("AppellationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuacAPI.Models.Domain", "domain")
                        .WithMany("Products")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuacAPI.Models.Furnisher", "furnisher")
                        .WithMany("Products")
                        .HasForeignKey("FurnisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuacAPI.Models.Region", "region")
                        .WithMany("Products")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("alcoholType");

                    b.Navigation("appellation");

                    b.Navigation("domain");

                    b.Navigation("furnisher");

                    b.Navigation("region");
                });

            modelBuilder.Entity("GuacAPI.Models.ProductOffer", b =>
                {
                    b.HasOne("GuacAPI.Models.Offer", "Offer")
                        .WithMany("ProductOffers")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuacAPI.Models.Product", "Product")
                        .WithMany("ProductOffers")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("GuacAPI.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("GuacAPI.Models.AlcoholType", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GuacAPI.Models.Appellation", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GuacAPI.Models.Domain", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GuacAPI.Models.Furnisher", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GuacAPI.Models.Offer", b =>
                {
                    b.Navigation("ProductOffers");
                });

            modelBuilder.Entity("GuacAPI.Models.Product", b =>
                {
                    b.Navigation("ProductOffers");
                });

            modelBuilder.Entity("GuacAPI.Models.Region", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}