using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineShop.Database;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Cart
{
	public class GetOrder
	{
		private ISession _session;
		private ApplicationDbContext _context;
		public GetOrder(ISession session, ApplicationDbContext context)
		{
			_session = session;
			_context = context;
		}

		public class Response
		{
			public IEnumerable<Product> Products { get; set; }
			public CustomerInformation CustomerInformation { get; set; }

			public decimal GetTotalValue() => Products.Sum(p => p.Value * p.Quantity);
		}

		public class CustomerInformation
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Email { get; set; }
			public string PhoneNumber { get; set; }
			public string Adress1 { get; set; }
			public string Adress2 { get; set; }
			public string City { get; set; }
			public string PostCode { get; set; }
		}

		public class Product
		{
			public int ProductId { get; set; }
			public int StockId { get; set; }
			public int Quantity { get; set; }
			public decimal Value { get; set; }
		}

		public Response Do()
		{
			var cart = _session.GetString("cart");
			var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);
			var listofProducts = _context.Stock
				.Include(s => s.Product)
				.AsEnumerable()
				.Where(s => cartList.Any(p => p.StockId == s.Id))
				.Select(s => new Product
				{
					ProductId = s.ProductId,
					StockId = s.Id,
					Value = s.Product.Value,
					Quantity = cartList.FirstOrDefault(p => p.StockId == s.Id).Quantity
				}).ToList();

			var customerInfoString = _session.GetString("customer-info");
			var customerInformation = JsonConvert.DeserializeObject<Domain.Models.CustomerInformation>(customerInfoString);

			return new Response
			{
				Products = listofProducts,
				CustomerInformation = new CustomerInformation
				{
					FirstName = customerInformation.FirstName,
					LastName = customerInformation.LastName,
					Email = customerInformation.Email,
					PhoneNumber = customerInformation.PhoneNumber,
					Adress1 = customerInformation.Adress1,
					Adress2 = customerInformation.Adress2,
					City = customerInformation.City,
					PostCode = customerInformation.PostCode
				}
			};
		}
	}
}
