using Microsoft.EntityFrameworkCore;
using OnlineShop.Database;

namespace OnlineShop.Application.Products
{
	public class GetProduct
	{
		private ApplicationDbContext _ctx;
		public GetProduct(ApplicationDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductViewModel Do(string name) =>
			_ctx.Products
			.Include(p => p.Stock)
			.Where(p => p.Name == name)
			.Select(p => new ProductViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = $"€ {p.Value:N2}",
				Stock = p.Stock.Select(s => new StockViewModel
				{
					Id = s.Id,
					Description = s.Description,
					InStock = s.Quantity > 0
				})
			})
			.FirstOrDefault();

		public class ProductViewModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public string Value { get; set; }
			public IEnumerable<StockViewModel> Stock { get; set; }
		}

		public class StockViewModel
		{
			public int Id { get; set; }
			public string Description { get; set; }
			public bool InStock { get; set; }
		}
	}
}
