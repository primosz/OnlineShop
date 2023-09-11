using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Cart;
using OnlineShop.Database;

namespace OnlineShop.UI.Pages
{
	public class CartModel : PageModel
	{
		public IEnumerable<GetCart.Response> CartList { get; set; }
		public ApplicationDbContext _context { get; set; }

		public CartModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			CartList = new GetCart(HttpContext.Session, _context).Do();
			return Page();
		}
	}
}
