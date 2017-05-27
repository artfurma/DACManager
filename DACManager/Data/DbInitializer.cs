using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DACManager.Models;

namespace DACManager.Data
{
	public static class DbInitializer
	{
		public static void Initialize(ApplicationDbContext context)
		{
			context.Database.Migrate();

			// Znajdź jakiekolwiek filmy
			if (context.Movies.Any())
			{
				return; // Baza już została zapełniona przykładowymi danymi
			}

			var movies = new[]
			{
				new Movie {Title = "Ojciec Chrzestny", ReleaseDate = DateTime.Parse("1972-12-31")},
				new Movie {Title = "Gwiezdne Wojny IV: Nowa Nadzieja", ReleaseDate = DateTime.Parse("1977-07-19")},
				new Movie {Title = "Avengers", ReleaseDate = DateTime.Parse("2012-05-12")},
				new Movie {Title = "Straight Outta Compton", ReleaseDate = DateTime.Parse("2015-09-04")},
				new Movie {Title = "Titanic", ReleaseDate = DateTime.Parse("1997-11-18")}
			};

			foreach (var m in movies)
			{
				context.Movies.Add(m);
			}
			context.SaveChanges();

			var actors = new[]
			{
				new Actor {FirstName = "Leonardo", LastName = "DiCaprio", BirthDate = DateTime.Parse("1974-11-11")},
				new Actor {FirstName = "Robert", LastName = "De Niro", BirthDate = DateTime.Parse("1943-08-17")},
				new Actor {FirstName = "Mark", LastName = "Hamill", BirthDate = DateTime.Parse("1951-09-25")},
				new Actor {FirstName = "Robert", LastName = "Downey Jr", BirthDate = DateTime.Parse("1965-04-04")},
				new Actor {FirstName = "Al", LastName = "Pacino", BirthDate = DateTime.Parse("1940-04-25")},
				new Actor {FirstName = "Corey", LastName = "Hawkings", BirthDate = DateTime.Parse("1988-10-22")}
			};

			foreach (var a in actors)
			{
				context.Actors.Add(a);
			}
			context.SaveChanges();

			var stores = new[]
			{
				new Store {Name = "U Eda", Address = "Kwiatowa 32"},
				new Store {Name = "Ekskluzywna Wypozyczalnia Filmow", Address = "Startowa 10E/3232"}
			};

			foreach (var store in stores)
			{
				context.Stores.Add(store);
			}
			context.SaveChanges();

			var customers = new[]
			{
				new Customer
				{
					FirstName = "Jan",
					LastName = "Kowalski",
					Store = context.Stores.FirstOrDefault(),
					Address = "Kopernika 15",
					Email = "jan.nowak@gmail.com"
				}
			};

			foreach (var customer in customers)
			{
				context.Customers.Add(customer);
			}
			context.SaveChanges();

			var staff = new[]
			{
				new Staff
				{
					FirstName = "Adam",
					LastName = "Kowalczyk",
					Email = "a.kowal@czyk.pl",
					Address = "Kowalska 999",
					Store = context.Stores.LastOrDefault()
				}
			};

			foreach (var s in staff)
			{
				context.Staff.Add(s);
			}
			context.SaveChanges();
		}
	}
}
