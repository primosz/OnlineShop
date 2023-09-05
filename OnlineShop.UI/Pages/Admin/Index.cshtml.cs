using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineShop.UI.Pages.Admin
{
	public class IndexModel : PageModel
	{
		public void OnGet()
		{
			Assembly asm = Assembly.GetExecutingAssembly();

			var cont = asm.GetTypes()
				.Where(type => typeof(Controller).IsAssignableFrom(type)) //filter controllers
				.SelectMany(type => type.GetMethods())
				.Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute))).ToList();
		}
	}
}
