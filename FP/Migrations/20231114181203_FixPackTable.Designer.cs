﻿// <auto-generated />
using System;
using FP.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FP.Migrations
{
    [DbContext(typeof(FpDbContext))]
    [Migration("20231114181203_FixPackTable")]
    partial class FixPackTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FP.Core.Database.Models.FpUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("BalanceCrypto")
                        .HasColumnType("real");

                    b.Property<float>("BalanceFiat")
                        .HasColumnType("real");

                    b.Property<float>("BalanceInternal")
                        .HasColumnType("real");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Passwordhash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rang")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FP.Core.Database.Models.Pack", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<decimal>("DealSum")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("FP.Core.Database.Models.Pack", b =>
                {
                    b.HasOne("FP.Core.Database.Models.FpUser", "User")
                        .WithMany("Pack")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FP.Core.Database.Models.FpUser", b =>
                {
                    b.Navigation("Pack");
                });
#pragma warning restore 612, 618
        }
    }
}
