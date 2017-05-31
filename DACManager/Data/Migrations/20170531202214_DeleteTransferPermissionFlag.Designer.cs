using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DACManager.Data;
using DACManager.Models;

namespace DACManager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170531202214_DeleteTransferPermissionFlag")]
    partial class DeleteTransferPermissionFlag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DACManager.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("DACManager.Models.ActorMovie", b =>
                {
                    b.Property<int>("ActorId");

                    b.Property<int>("MovieId");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("ActorMovies");
                });

            modelBuilder.Entity("DACManager.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DACManager.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DACManager.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DACManager.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MovieId");

                    b.Property<int>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("DACManager.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("DACManager.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LanguageId");

                    b.Property<int>("Length");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("DACManager.Models.MovieCategory", b =>
                {
                    b.Property<int>("MovieId");

                    b.Property<int>("CategoryId");

                    b.HasKey("MovieId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("MovieCategories");
                });

            modelBuilder.Entity("DACManager.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<int?>("RentalId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RentalId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("DACManager.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Actors");

                    b.Property<int>("Categories");

                    b.Property<int>("Customers");

                    b.Property<int>("Inventories");

                    b.Property<int>("Languages");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("Movies");

                    b.Property<int>("Payments");

                    b.Property<int>("Permissions");

                    b.Property<bool>("Receive");

                    b.Property<int>("Rentals");

                    b.Property<int>("Staff");

                    b.Property<int>("Stores");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("DACManager.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<int>("InventoryId");

                    b.Property<DateTime>("RentalDate");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<int?>("StaffId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("InventoryId")
                        .IsUnique();

                    b.HasIndex("StaffId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("DACManager.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("PaymentId");

                    b.Property<int>("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.HasIndex("StoreId")
                        .IsUnique();

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("DACManager.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
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
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DACManager.Models.ActorMovie", b =>
                {
                    b.HasOne("DACManager.Models.Actor", "Actor")
                        .WithMany("ActorMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DACManager.Models.Movie", "Movie")
                        .WithMany("ActorMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DACManager.Models.Customer", b =>
                {
                    b.HasOne("DACManager.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DACManager.Models.Inventory", b =>
                {
                    b.HasOne("DACManager.Models.Movie", "Movie")
                        .WithMany("Inventories")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("DACManager.Models.Movie", b =>
                {
                    b.HasOne("DACManager.Models.Language", "Language")
                        .WithMany("Movies")
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("DACManager.Models.MovieCategory", b =>
                {
                    b.HasOne("DACManager.Models.Category", "Category")
                        .WithMany("MovieCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DACManager.Models.Movie", "Movie")
                        .WithMany("MovieCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DACManager.Models.Payment", b =>
                {
                    b.HasOne("DACManager.Models.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DACManager.Models.Rental", "Rental")
                        .WithMany("Payments")
                        .HasForeignKey("RentalId");
                });

            modelBuilder.Entity("DACManager.Models.Permission", b =>
                {
                    b.HasOne("DACManager.Models.ApplicationUser", "User")
                        .WithOne("Permission")
                        .HasForeignKey("DACManager.Models.Permission", "UserId");
                });

            modelBuilder.Entity("DACManager.Models.Rental", b =>
                {
                    b.HasOne("DACManager.Models.Customer", "Customer")
                        .WithOne("Rental")
                        .HasForeignKey("DACManager.Models.Rental", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DACManager.Models.Inventory", "Inventory")
                        .WithOne("Rental")
                        .HasForeignKey("DACManager.Models.Rental", "InventoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DACManager.Models.Staff", "Staff")
                        .WithMany("Rentals")
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("DACManager.Models.Staff", b =>
                {
                    b.HasOne("DACManager.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("DACManager.Models.Store", "Store")
                        .WithOne("Staff")
                        .HasForeignKey("DACManager.Models.Staff", "StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DACManager.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DACManager.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DACManager.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
