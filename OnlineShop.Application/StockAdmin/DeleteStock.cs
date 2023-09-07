using OnlineShop.Database;

namespace OnlineShop.Application.StockAdmin
{
	public class DeleteStock
	{
		private ApplicationDbContext _context;

		public DeleteStock(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Do(int id)
		{
			var stock = _context.Stock.FirstOrDefault(s => s.Id == id);
			_context.Stock.Remove(stock);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}
