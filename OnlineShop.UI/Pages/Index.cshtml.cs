using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Products;
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
        public ProductViewModel Product { get; set; }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_ctx).Do(Product.Name, Product.Description, Product.Value);
            return RedirectToPage("Index");
        }
    }
}