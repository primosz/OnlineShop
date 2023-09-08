using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Cart
{
	public class AddToCart
	{
		private ISession _session;
		public AddToCart(ISession session)
		{
			_session = session;
		}

		public class Request
		{
			public int StockId { get; set; }
			public int Quantity { get; set; }
		}

		public void Do(Request request)
		{
			var cartList = new List<CartProduct>();
			var stringObject = _session.GetString("cart");

			if (!string.IsNullOrEmpty(stringObject))
			{
				cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
			}

			var cartProduct = new CartProduct
			{
				StockId = request.StockId,
				Quantity = request.Quantity
			};

			//var stringObject = JsonConvert.SerializeObject(cartProduct);
			//TODO: append to the cart

			_session.SetString("cart", stringObject);
		}
	}
}
