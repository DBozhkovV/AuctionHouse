﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AuctionHouse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuctionHouse.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AuctionHouse.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorUserId")
                        .HasColumnType("uuid");

                    b.Property<float>("Bid")
                        .HasColumnType("real");

                    b.Property<float>("BoughtFor")
                        .HasColumnType("real");

                    b.Property<float>("BuyPrice")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndBidDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("ImagesNames")
                        .HasColumnType("text[]");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("MainImageName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartingBidDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("StartingPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AuthorUserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("AuctionHouse.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOrdered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsOrderActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOrderCompleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AuctionHouse.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PasswordResetToken")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("PasswordResetTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VerificationToken")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5ed6000b-5653-486b-abfd-f6cfd1ec7087"),
                            Balance = 0f,
                            Email = "admin@gmail.com",
                            FirstName = "Admin",
                            IsVerified = true,
                            LastName = "Admin",
                            Password = "$2a$11$Mi21L9FGHUGMxecuhCnyw.Rj6J1CCsoFuvnpMGjziOd7pIsfyrUHi",
                            PhoneNumber = "admin",
                            Role = 1,
                            Username = "admin",
                            VerificationToken = new Guid("f1f8a63d-49eb-439e-ab0d-aec080e6b834")
                        });
                });

            modelBuilder.Entity("AuctionHouse.Models.Item", b =>
                {
                    b.HasOne("AuctionHouse.Models.User", "Author")
                        .WithMany("AuthoredItems")
                        .HasForeignKey("AuthorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("AuctionHouse.Models.Order", b =>
                {
                    b.HasOne("AuctionHouse.Models.Item", "Item")
                        .WithOne("Order")
                        .HasForeignKey("AuctionHouse.Models.Order", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionHouse.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuctionHouse.Models.Item", b =>
                {
                    b.Navigation("Order")
                        .IsRequired();
                });

            modelBuilder.Entity("AuctionHouse.Models.User", b =>
                {
                    b.Navigation("AuthoredItems");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
