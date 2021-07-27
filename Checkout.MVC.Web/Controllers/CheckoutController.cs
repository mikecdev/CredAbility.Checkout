using CredAbility.Checkout.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.MVC.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckout _checkout;

        public CheckoutController(ICheckout checkout) 
        {
            _checkout = checkout;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
