using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineShop.Database;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Cart
{
	public class GetCart
	{
		private ISession _session;
		private ApplicationDbContext _context;
		public GetCart(ISession session, ApplicationDbContext context)
		{
			_session = session;
			_context = context;
		}

		public class Response
		{
			public string Name { get; set; }
			public string Value { get; set; }
			public int StockId { get; set; }
			public int Quantity { get; set; }
		}

		public Response Do()
		{
			//TODO: append to the cart, multiple items
			var stringObject = _session.GetString("cart");
			var cartProduct = JsonConvert.DeserializeObject<CartProduct>(stringObject);

			var response = _context.Stock
				.Include(s => s.Product)
				.Where(s => s.Id == cartProduct.StockId)
				.Select(s => new Response
				{
					Name = s.Product.Name,
					Value = $"$ {s.Product.Value:N2}",
					StockId = s.Id,
					Quantity = cartProduct.Quantity
				})
				.FirstOrDefault();

			return response;
		}
	}
}
