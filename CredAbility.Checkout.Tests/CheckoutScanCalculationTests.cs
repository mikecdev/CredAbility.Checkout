using CredAbility.Checkout.Concrete;
using CredAbility.Checkout.Interfaces;
using NUnit.Framework;

namespace CredAbility.Checkout.Tests
{
    public class Tests
    {
        private ICheckout _checkout;

        [SetUp]
        public void Setup()
        {
            _checkout = new CheckOut();
        }

        [Test]
        public void ZeroItems_Calculate_ZeroReturn()
        {
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(0, result);
        }
    }
}