﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineMarket.DataAccess;

namespace OnlineMarket.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210521113157_sodfaihdoifj")]
    partial class sodfaihdoifj
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OnlineMarket.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OnlineMarket.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Area")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExtraDetails")
                        .HasColumnType("TEXT");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("OnlineMarket.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProductReviewId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductReviewId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("OnlineMarket.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineMarket.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AverageRating")
                        .HasColumnType("REAL");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModeratedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("SellerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WarehouseLocation")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ModeratedById");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OnlineMarket.Models.ProductReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Rating")
                        .HasColumnType("REAL");

                    b.Property<string>("Review")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReviewerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("OnlineMarket.Models.RefreshToken", b =>
                {
                    b.Property<string>("JwtId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Used")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("JwtId");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("OnlineMarket.Models.SystemUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Area")
                        .HasColumnType("TEXT");

                    b.Property<string>("BankName")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExtraDetails")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("IBAN")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OnlineMarket.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeliveryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("REAL");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("DeliveryId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("OnlineMarket.Models.WishListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SystemUserId");

                    b.ToTable("WishList");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OnlineMarket.Models.SystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnlineMarket.Models.SystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineMarket.Models.SystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnlineMarket.Models.SystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineMarket.Models.Image", b =>
                {
                    b.HasOne("OnlineMarket.Models.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineMarket.Models.ProductReview", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductReviewId");
                });

            modelBuilder.Entity("OnlineMarket.Models.Order", b =>
                {
                    b.HasOne("OnlineMarket.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineMarket.Models.Transaction", null)
                        .WithMany("Orders")
                        .HasForeignKey("TransactionId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineMarket.Models.Product", b =>
                {
                    b.HasOne("OnlineMarket.Models.SystemUser", "ModeratedBy")
                        .WithMany()
                        .HasForeignKey("ModeratedById");

                    b.HasOne("OnlineMarket.Models.SystemUser", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.Navigation("ModeratedBy");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("OnlineMarket.Models.ProductReview", b =>
                {
                    b.HasOne("OnlineMarket.Models.Product", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineMarket.Models.SystemUser", "Reviewer")
                        .WithMany()
                        .HasForeignKey("ReviewerId");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("OnlineMarket.Models.RefreshToken", b =>
                {
                    b.HasOne("OnlineMarket.Models.SystemUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineMarket.Models.Transaction", b =>
                {
                    b.HasOne("OnlineMarket.Models.SystemUser", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("OnlineMarket.Models.Delivery", "Delivery")
                        .WithMany()
                        .HasForeignKey("DeliveryId");

                    b.Navigation("Buyer");

                    b.Navigation("Delivery");
                });

            modelBuilder.Entity("OnlineMarket.Models.WishListItem", b =>
                {
                    b.HasOne("OnlineMarket.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineMarket.Models.SystemUser", null)
                        .WithMany("WishList")
                        .HasForeignKey("SystemUserId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineMarket.Models.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("OnlineMarket.Models.ProductReview", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("OnlineMarket.Models.SystemUser", b =>
                {
                    b.Navigation("WishList");
                });

            modelBuilder.Entity("OnlineMarket.Models.Transaction", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
