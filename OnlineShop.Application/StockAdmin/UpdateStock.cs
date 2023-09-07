﻿using OnlineShop.Database;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.StockAdmin
{
	public class UpdateStock
	{
		private ApplicationDbContext _context;

		public UpdateStock(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Response> Do(Request request)
		{
			var stocks = new List<Stock>();
			foreach (var stock in request.Stock)
			{
				stocks.Add(new Stock
				{
					Id = stock.Id,
					Description = stock.Description,
					Quantity = stock.Quantity,
					ProductId = stock.ProductId
				});
			};

			_context.Stock.UpdateRange(stocks);
			await _context.SaveChangesAsync();

			return new Response
			{
				Stock = request.Stock
			};
		}

		public class StockViewModel
		{
			public int Id { get; set; }
			public int ProductId { get; set; }

			public string Description { get; set; }
			public int Quantity { get; set; }
		}

		public class Request
		{
			public IEnumerable<StockViewModel> Stock { get; set; }
		}

		public class Response
		{
			public IEnumerable<StockViewModel> Stock { get; set; }
		}
	}
}
