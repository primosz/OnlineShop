using Braintree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Cart;
using OnlineShop.UI.Services;

namespace OnlineShop.UI.Pages.Checkout
{
	public class PaymentModel : PageModel
	{
		private readonly IBraintreeService _braintreeService;
		public string ClientToken;
		public GetCart.Response Cart;

		public PaymentModel(IBraintreeService braintreeService)
		{
			_braintreeService = braintreeService;
		}

		public IActionResult OnGet()
		{
			var information = new GetCustomerInformation(HttpContext.Session).Do();

			if (information == null)
			{
				return RedirectToPage("/Checkout/CustomerInformation");
			}

			var gateway = _braintreeService.GetGateway();
			ClientToken = gateway.ClientToken.Generate();  //Genarate a token
														   //ViewBag.ClientToken = clientToken;

			//for debug
			Cart = new GetCart.Response()
			{
				Name = "TestProductName",
				Value = $"$ {12:N2}",
				StockId = 123,
				Quantity = 12,
				Nonce = ""
			};


			return Page();
		}

		//param to be changed, for debug
		public IActionResult OnPost(GetCart.Response response)
		{
			var gateway = _braintreeService.GetGateway();
			var request = new TransactionRequest
			{
				Amount = Convert.ToDecimal("1"),
				PaymentMethodNonce = response.Nonce,
				Options = new TransactionOptionsRequest
				{
					SubmitForSettlement = true
				}
			};

			Result<Transaction> result = gateway.Transaction.Sale(request);

			if (result.IsSuccess())
			{
				//to be change
				return RedirectToPage("Index");
			}
			else
			{
				return RedirectToPage("Index");

			}
		}
	}
}
