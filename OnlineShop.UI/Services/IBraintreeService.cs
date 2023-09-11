using Braintree;

namespace OnlineShop.UI.Services
{
	public interface IBraintreeService
	{
		IBraintreeGateway CreateGateway();
		IBraintreeGateway GetGateway();
	}
}
