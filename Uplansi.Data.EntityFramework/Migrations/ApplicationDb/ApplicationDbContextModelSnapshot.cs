﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Uplansi.Data.EntityFramework;

#nullable disable

namespace Uplansi.Data.EntityFramework.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Uplansi.Core.Entities.Common.Country", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "us",
                            Name = "United States"
                        },
                        new
                        {
                            Id = "co",
                            Name = "Colombia"
                        });
                });

            modelBuilder.Entity("Uplansi.Core.Entities.Common.Language", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Languages", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "en",
                            Name = "English"
                        },
                        new
                        {
                            Id = "es",
                            Name = "Spanish"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}