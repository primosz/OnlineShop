﻿using OnlineShop.Database;

namespace OnlineShop.Application.ProductsAdmin
{
	public class GetProducts
	{
		private ApplicationDbContext _context;
		public GetProducts(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<ProductViewModel> Do() =>
			_context.Products.Select(p => new ProductViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = p.Value
			}).ToList();

		public class ProductViewModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
		}
	}


}
