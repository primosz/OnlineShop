using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.ProductsAdmin;
using OnlineShop.Application.StockAdmin;
using OnlineShop.Database;

namespace OnlineShop.UI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AdminController : Controller
	{
		private ApplicationDbContext _context;
		public AdminController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("products")]
		public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());

		[HttpGet("products/{id}")]
		public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));

		[HttpPost("products")]
		public async Task<IActionResult> CreateProduct(CreateProduct.Request request) => Ok(await new CreateProduct(_context).Do(request));

		[HttpDelete("products/{id}")]
		public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_context).Do(id));

		[HttpPut("products")]
		public async Task<IActionResult> UpdateProduct(UpdateProduct.Request request) => Ok(await new UpdateProduct(_context).Do(request));


		[HttpGet("stock")]
		public IActionResult GetStock() => Ok(new GetStock(_context).Do());

		[HttpPost("stock")]
		public async Task<IActionResult> CreateStock(CreateStock.Request request) => Ok(await new CreateStock(_context).Do(request));

		[HttpDelete("stock/{id}")]
		public async Task<IActionResult> DeleteStock(int id) => Ok(await new DeleteStock(_context).Do(id));

		[HttpPut("stock")]
		public async Task<IActionResult> UpdateStock(UpdateStock.Request request) => Ok(await new UpdateStock(_context).Do(request));
	}
}
