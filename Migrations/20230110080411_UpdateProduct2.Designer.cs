﻿// <auto-generated />
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuacAPI.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20230110080411_UpdateProduct2")]
    partial class UpdateProduct2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GuacAPI.Models.AlcoholType", b =>
                {
                    b.Property<int>("AlcoholTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlcoholTypeId"));

                    b.Property<string>("code_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("label")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlcoholTypeId");

                    b.ToTable("AlcoholTypes");
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

                    b.ToTable("Appellations");
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

                    b.ToTable("Domains");
                });

            modelBuilder.Entity("GuacAPI.Models.Furnisher", b =>
                {
                    b.Property<int>("FurnisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FurnisherId"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Siret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FurnisherId");

                    b.ToTable("Furnishers");
                });

            modelBuilder.Entity("GuacAPI.Models.Millesime", b =>
                {
                    b.Property<int>("MillesimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MillesimeId"));

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("MillesimeId");

                    b.ToTable("Millesimes");
                });

            modelBuilder.Entity("GuacAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<decimal>("AlcoholDegree")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AlcoholTypeId")
                        .HasColumnType("int");

                    b.Property<int>("AppellationId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<int>("FurnisherId")
                        .HasColumnType("int");

                    b.Property<int>("MillesimeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Reference")
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

                    b.HasIndex("MillesimeId");

                    b.HasIndex("RegionId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("GuacAPI.Models.Region", b =>
                {
                    b.Property<int>("RegionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegionID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionID");

                    b.ToTable("Regions");
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

                    b.HasOne("GuacAPI.Models.Millesime", "millesime")
                        .WithMany("Products")
                        .HasForeignKey("MillesimeId")
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

                    b.Navigation("millesime");

                    b.Navigation("region");
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

            modelBuilder.Entity("GuacAPI.Models.Millesime", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GuacAPI.Models.Region", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
