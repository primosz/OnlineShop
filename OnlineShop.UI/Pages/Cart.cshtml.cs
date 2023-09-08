using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Cart;
using OnlineShop.Database;

namespace OnlineShop.UI.Pages
{
	public class CartModel : PageModel
	{
		public GetCart.Response Cart { get; set; }
		public ApplicationDbContext _context { get; set; }

		public CartModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			Cart = new GetCart(HttpContext.Session, _context).Do();
			return Page();
		}
	}
}
