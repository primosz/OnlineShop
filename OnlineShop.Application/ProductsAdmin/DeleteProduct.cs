using OnlineShop.Database;

namespace OnlineShop.Application.ProductsAdmin
{
	public class DeleteProduct
	{
		private ApplicationDbContext _context;

		public DeleteProduct(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Do(int id)
		{
			var product = _context.Products.FirstOrDefault(p => p.Id == id);
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
