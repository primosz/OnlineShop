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
			public string Nonce { get; set; }
		}

		public IEnumerable<Response> Do()
		{
			var stringObject = _session.GetString("cart");
			if (string.IsNullOrEmpty(stringObject))
			{
				return new List<Response>();
			}

			var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

			var response = _context.Stock
				.Include(s => s.Product)
				.AsEnumerable()
				.Where(s => cartList.Any(x => x.StockId == s.Id))
				.Select(s => new Response
				{
					Name = s.Product.Name,
					Value = $"$ {s.Product.Value:N2}",
					StockId = s.Id,
					Quantity = cartList.FirstOrDefault(x => x.StockId == s.Id).Quantity
				})
				.ToList();

			return response;
		}
	}
}
