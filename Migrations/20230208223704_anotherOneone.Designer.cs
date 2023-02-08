﻿// <auto-generated />
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
    [Migration("20230208223704_anotherOneone")]
    partial class anotherOneone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GuacAPI.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

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

                    b.Property<DateTime>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

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

                    b.HasData(
                        new
                        {
                            AlcoholTypeId = 1,
                            label = "Rouge"
                        });
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

                    b.HasData(
                        new
                        {
                            AppellationId = 1,
                            Name = "IGP"
                        });
                });

            modelBuilder.Entity("GuacAPI.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("PreviousCommentId")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("OfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment", (string)null);
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

                    b.HasData(
                        new
                        {
                            DomainId = 1,
                            Name = "Domaine 1"
                        });
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

                    b.HasData(
                        new
                        {
                            FurnisherId = 1,
                            City = "budapest",
                            Name = "fournisseur 1",
                            PostalCode = "27000",
                            Siret = "29239393",
                            Street = "155 rue des vins"
                        });
                });

            modelBuilder.Entity("GuacAPI.Models.InvoiceFurnisherProduct", b =>
                {
                    b.Property<int>("InvoiceFurnisherId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityProduct")
                        .HasColumnType("int");

                    b.HasKey("InvoiceFurnisherId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("InvoiceFurnisherProduct", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Offer", b =>
                {
                    b.Property<int>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfferId"));

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("isB2B")
                        .HasColumnType("bit");

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

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            AlcoholDegree = 2f,
                            AlcoholTypeId = 1,
                            AppellationId = 1,
                            DomainId = 1,
                            FurnisherId = 1,
                            Millesime = 2010,
                            Name = "product 1",
                            Price = 12,
                            Reference = "jndijfndjn",
                            RegionId = 1,
                            Stock = 155
                        });
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

                    b.HasData(
                        new
                        {
                            RegionID = 1,
                            Name = "region 1"
                        });
                });

            modelBuilder.Entity("InvoiceFurnisher", b =>
                {
                    b.Property<int>("InvoiceFurnisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceFurnisherId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FurnisherId")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvoiceFurnisherId");

                    b.HasIndex("FurnisherId");

                    b.ToTable("InvoiceFurnisher", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Entities.User", b =>
                {
                    b.OwnsMany("GuacAPI.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ReasonRevoked")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("Revoked")
                                .HasColumnType("datetime2");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Token")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("GuacAPI.Models.Comment", b =>
                {
                    b.HasOne("GuacAPI.Models.Offer", "offer")
                        .WithMany("Comments")
                        .HasForeignKey("OfferId")
                        .IsRequired();

                    b.HasOne("GuacAPI.Entities.User", "user")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("offer");

                    b.Navigation("user");
                });

            modelBuilder.Entity("GuacAPI.Models.InvoiceFurnisherProduct", b =>
                {
                    b.HasOne("InvoiceFurnisher", "InvoiceFurnisher")
                        .WithMany("InvoicesFurnisherProduct")
                        .HasForeignKey("InvoiceFurnisherId")
                        .IsRequired();

                    b.HasOne("GuacAPI.Models.Product", "Product")
                        .WithMany("InvoicesFurnisherProduct")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("InvoiceFurnisher");

                    b.Navigation("Product");
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

            modelBuilder.Entity("InvoiceFurnisher", b =>
                {
                    b.HasOne("GuacAPI.Models.Furnisher", "Furnisher")
                        .WithMany("Invoices")
                        .HasForeignKey("FurnisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Furnisher");
                });

            modelBuilder.Entity("GuacAPI.Entities.User", b =>
                {
                    b.Navigation("Comments");
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
                    b.Navigation("Invoices");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("GuacAPI.Models.Offer", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ProductOffers");
                });

            modelBuilder.Entity("GuacAPI.Models.Product", b =>
                {
                    b.Navigation("InvoicesFurnisherProduct");

                    b.Navigation("ProductOffers");
                });

            modelBuilder.Entity("GuacAPI.Models.Region", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("InvoiceFurnisher", b =>
                {
                    b.Navigation("InvoicesFurnisherProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
