﻿// <auto-generated />
using CarService.DbAccess.EF;
using CarService.DbAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace CarService.Api.Migrations
{
    [DbContext(typeof(CarServiceDbContext))]
    partial class CarServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarService.DbAccess.Entities.Auto", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AutoRiaId");

                    b.Property<string>("Info");

                    b.HasKey("EntityId");

                    b.ToTable("Autos");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Comment", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("OrderId");

                    b.Property<int>("Rate");

                    b.Property<string>("Text");

                    b.HasKey("EntityId");

                    b.HasIndex("OrderId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.CustomerAuto", b =>
                {
                    b.Property<string>("CustomerId");

                    b.Property<int?>("AutoId");

                    b.HasKey("CustomerId", "AutoId");

                    b.HasIndex("AutoId");

                    b.ToTable("CustomerAuto");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Dialog", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerId");

                    b.Property<string>("MechanicId");

                    b.Property<string>("MechanicId1");

                    b.Property<int?>("OrderId");

                    b.HasKey("EntityId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MechanicId1");

                    b.HasIndex("OrderId");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Invitation", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("MechanicId");

                    b.Property<int?>("OrderId");

                    b.Property<string>("Text");

                    b.HasKey("EntityId");

                    b.HasIndex("MechanicId");

                    b.HasIndex("OrderId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Message", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("DialogId");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("EntityId");

                    b.HasIndex("DialogId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Order", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AutoId");

                    b.Property<string>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("MechanicId");

                    b.Property<int?>("ReviewId");

                    b.Property<int>("Status");

                    b.HasKey("EntityId");

                    b.HasIndex("AutoId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MechanicId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Review", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AutoRate");

                    b.Property<string>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("MechanicId");

                    b.Property<int?>("OrderId");

                    b.Property<string>("Photos");

                    b.Property<string>("Videos");

                    b.HasKey("EntityId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MechanicId");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasFilter("[OrderId] IS NOT NULL");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.ReviewProposition", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<string>("MechanicId");

                    b.Property<int?>("OrderId");

                    b.Property<int>("Price");

                    b.HasKey("EntityId");

                    b.HasIndex("MechanicId");

                    b.HasIndex("OrderId");

                    b.ToTable("ReviewPropositions");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Transaction", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("ReceiverId");

                    b.Property<string>("SenderId");

                    b.Property<int>("Status");

                    b.HasKey("EntityId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("LastLoginDate");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasAlternateKey("EntityId")
                        .HasName("AlternateKey_Entityid");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Customer", b =>
                {
                    b.HasBaseType("CarService.DbAccess.Entities.User");

                    b.Property<string>("CardNumber");

                    b.Property<string>("City");

                    b.ToTable("Customer");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Mechanic", b =>
                {
                    b.HasBaseType("CarService.DbAccess.Entities.Customer");

                    b.Property<int>("MechanicRate");

                    b.Property<string>("Specialization");

                    b.Property<int>("WorkExperience");

                    b.ToTable("Mechanic");

                    b.HasDiscriminator().HasValue("Mechanic");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Comment", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Order", "Order")
                        .WithMany("Comments")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.CustomerAuto", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Auto", "Auto")
                        .WithMany("CustomerAutoes")
                        .HasForeignKey("AutoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarService.DbAccess.Entities.Customer", "Customer")
                        .WithMany("CustomerAutoes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Dialog", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Customer", "Customer")
                        .WithMany("Dialogs")
                        .HasForeignKey("CustomerId");

                    b.HasOne("CarService.DbAccess.Entities.Mechanic", "Mechanic")
                        .WithMany()
                        .HasForeignKey("MechanicId1");

                    b.HasOne("CarService.DbAccess.Entities.Order", "Order")
                        .WithMany("Dialogs")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Invitation", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Mechanic", "Mechanic")
                        .WithMany("Invitations")
                        .HasForeignKey("MechanicId");

                    b.HasOne("CarService.DbAccess.Entities.Order", "Order")
                        .WithMany("Invitations")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Message", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Dialog", "Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Order", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Auto", "Auto")
                        .WithMany("Orders")
                        .HasForeignKey("AutoId");

                    b.HasOne("CarService.DbAccess.Entities.Customer", "Customer")
                        .WithMany("OrdersMade")
                        .HasForeignKey("CustomerId");

                    b.HasOne("CarService.DbAccess.Entities.Mechanic", "Mechanic")
                        .WithMany("OrdersTaken")
                        .HasForeignKey("MechanicId");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Review", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId");

                    b.HasOne("CarService.DbAccess.Entities.Mechanic", "Mechanic")
                        .WithMany("MadeReviews")
                        .HasForeignKey("MechanicId");

                    b.HasOne("CarService.DbAccess.Entities.Order", "Order")
                        .WithOne("Review")
                        .HasForeignKey("CarService.DbAccess.Entities.Review", "OrderId");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.ReviewProposition", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.Mechanic", "Mechanic")
                        .WithMany("ReviewPropositions")
                        .HasForeignKey("MechanicId");

                    b.HasOne("CarService.DbAccess.Entities.Order", "Order")
                        .WithMany("ReviewPropositions")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("CarService.DbAccess.Entities.Transaction", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.User", "Receiver")
                        .WithMany("ReceiversTransactions")
                        .HasForeignKey("ReceiverId");

                    b.HasOne("CarService.DbAccess.Entities.User", "Sender")
                        .WithMany("SendersTransactions")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CarService.DbAccess.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarService.DbAccess.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
