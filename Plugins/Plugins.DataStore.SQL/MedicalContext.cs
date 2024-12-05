using Microsoft.EntityFrameworkCore;
using CoreBusiness;

namespace Plugins.DataStore.SQL
{
	public class MedicalContext : DbContext
	{
		public MedicalContext(DbContextOptions<MedicalContext> options): base(options) 
		{
			
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		//defining the relationship
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>()
				.HasMany(c => c.Products)
				.WithOne(p => p.Category)
				.HasForeignKey(p => p.CategoryId);

			// seeding data
			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId = 1, Name = "Tablets", Description = "Tablets" },
				new Category { CategoryId = 2, Name = "Syrups", Description = "Syrups" },
				new Category { CategoryId = 3, Name = "Injections", Description = "Injections" }
			);

			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = 1, CategoryId = 1, Name = "Rzoler DSR", Quantity = 100, UnitPrice = 11.9, Price = 119 },
				new Product { ProductId = 2, CategoryId = 1, Name = "Cefomil-O", Quantity = 200, UnitPrice = 20, Price = 200 },
				new Product { ProductId = 3, CategoryId = 2, Name = "Sucram-O Syp", Quantity = 300, UnitPrice = 252, Price = 252 },
				new Product { ProductId = 4, CategoryId = 2, Name = "Laxiforce Syp", Quantity = 300, UnitPrice = 193, Price = 193 },
				new Product { ProductId = 5, CategoryId = 3, Name = "Razo", Quantity = 300, UnitPrice = 100, Price = 100 }
			);
		}
	}
}
