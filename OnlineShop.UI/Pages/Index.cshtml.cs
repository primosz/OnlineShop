using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.CreateProducts;
using OnlineShop.Application.GetProducts;
using OnlineShop.Database;

namespace OnlineShop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _ctx;


        public IndexModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public Application.CreateProducts.ProductViewModel Product { get; set; }
        public IEnumerable<Application.GetProducts.ProductViewModel> Products { get; set; }


        public void OnGet()
        {
            Products = new GetProducts(_ctx).Do();
        }

        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_ctx).Do(Product);
            return RedirectToPage("Index");
        }
    }
}