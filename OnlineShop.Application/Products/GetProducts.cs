using OnlineShop.Database;

namespace OnlineShop.Application.Products
{
	public class GetProducts
	{
		private ApplicationDbContext _ctx;
		public GetProducts(ApplicationDbContext ctx)
		{
			_ctx = ctx;
		}

		public IEnumerable<ProductViewModel> Do() =>
			_ctx.Products.Select(x => new ProductViewModel
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description,
				Value = x.Value
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
