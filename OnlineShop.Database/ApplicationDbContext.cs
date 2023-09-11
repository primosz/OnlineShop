﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;

namespace OnlineShop.Database
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Stock> Stock { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderStock> OrderStocks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<OrderStock>()
				.HasKey(p => new { p.StockId, p.OrderId });
		}
	}
}
