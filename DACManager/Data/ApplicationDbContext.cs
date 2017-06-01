using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DACManager.Models;

namespace DACManager.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		#region DbSets
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Inventory> Inventories { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Rental> Rentals { get; set; }
		public DbSet<Staff> Staff { get; set; }
		public DbSet<Store> Stores { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		#endregion

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
			builder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
			builder.Entity<Actor>().ToTable("Actors");
			builder.Entity<Category>().ToTable("Categories");
			builder.Entity<Customer>().ToTable("Customers");
			builder.Entity<Inventory>().ToTable("Inventories");
			builder.Entity<Language>().ToTable("Languages");
			builder.Entity<Movie>().ToTable("Movies");
			builder.Entity<Payment>().ToTable("Payments");
			builder.Entity<Rental>().ToTable("Rentals");
			builder.Entity<Staff>().ToTable("Staff");
			builder.Entity<Store>().ToTable("Stores");
			builder.Entity<Permission>().ToTable("Permissions");

			// Konfiguracja relacji wiele-do-wielu dla Actor i Movie
			builder.Entity<ActorMovie>().HasKey(am => new { am.ActorId, am.MovieId });
			builder.Entity<ActorMovie>().HasOne(am => am.Actor).WithMany(a => a.ActorMovies).HasForeignKey(am => am.ActorId);
			builder.Entity<ActorMovie>().HasOne(am => am.Movie).WithMany(m => m.ActorMovies).HasForeignKey(am => am.MovieId);
			builder.Entity<ActorMovie>().ToTable("ActorMovies");

			// Konfiguracja relacji wiele-do-wielu dla Movie i Category
			builder.Entity<MovieCategory>().HasKey(mc => new { mc.MovieId, mc.CategoryId });
			builder.Entity<MovieCategory>().HasOne(mc => mc.Movie).WithMany(m => m.MovieCategories).HasForeignKey(mc => mc.MovieId);
			builder.Entity<MovieCategory>().HasOne(mc => mc.Movie).WithMany(c => c.MovieCategories).HasForeignKey(mc => mc.CategoryId);
			builder.Entity<MovieCategory>().ToTable("MovieCategories");

			// Konfiguracja relacji jeden-do-jednego dla User i Permission
			builder.Entity<ApplicationUser>()
				.HasOne(u => u.Permission)
				.WithOne(p => p.User)
				.HasForeignKey<Permission>(p => p.UserId);
		}
	}
}
