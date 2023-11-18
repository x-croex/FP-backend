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
    [Migration("20231117233047_WithdrawAdded")]
    partial class WithdrawAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FP.Core.Database.Models.Pack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("DealSum")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PackTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PackTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("FP.Core.Database.Models.PackType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PackTypes");
                });

            modelBuilder.Entity("FP.Core.Database.Models.User", b =>
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

                    b.Property<int>("BalanceWalletId")
                        .HasColumnType("integer");

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

                    b.Property<int>("TopUpWalletId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BalanceWalletId");

                    b.HasIndex("TopUpWalletId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FP.Core.Database.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("WalletAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WalletSecretKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("FP.Core.Database.Models.Pack", b =>
                {
                    b.HasOne("FP.Core.Database.Models.PackType", "PackType")
                        .WithMany()
                        .HasForeignKey("PackTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FP.Core.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PackType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FP.Core.Database.Models.User", b =>
                {
                    b.HasOne("FP.Core.Database.Models.Wallet", "BalanceWallet")
                        .WithMany()
                        .HasForeignKey("BalanceWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FP.Core.Database.Models.Wallet", "TopUpWallet")
                        .WithMany()
                        .HasForeignKey("TopUpWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BalanceWallet");

                    b.Navigation("TopUpWallet");
                });
#pragma warning restore 612, 618
        }
    }
}