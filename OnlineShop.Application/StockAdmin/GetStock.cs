using Microsoft.EntityFrameworkCore;
using OnlineShop.Database;

namespace OnlineShop.Application.StockAdmin
{
	public class GetStock
	{
		private ApplicationDbContext _context;

		public GetStock(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<ProductViewModel> Do()
		{
			var stock = _context.Products
				.Include(p => p.Stock)
				.Select(p => new ProductViewModel
				{
					Id = p.Id,
					Name = p.Name,
					Stock = p.Stock.Select(s => new StockViewModel
					{
						Id = s.Id,
						Description = s.Description,
						Quantity = s.Quantity
					})
				})
				.ToList();

			return stock;
		}

		public class StockViewModel
		{
			public int Id { get; set; }

			public string Description { get; set; }
			public int Quantity { get; set; }
		}

		public class ProductViewModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public IEnumerable<StockViewModel> Stock { get; set; }
		}

	}
}

