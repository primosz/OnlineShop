using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Products;
using OnlineShop.Database;

namespace OnlineShop.UI.Pages
{
	public class IndexModel : PageModel
	{
		private ApplicationDbContext _context;


		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }


		public void OnGet()
		{
			Products = new GetProducts(_context).Do();
		}
	}
}