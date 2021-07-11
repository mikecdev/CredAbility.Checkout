using CredAbility.Checkout.Concrete;
using CredAbility.Checkout.Interfaces;
using NUnit.Framework;
using System;

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

        [Test]
        public void AddNullItem_Calculate_ThrowsArgumentNullException()
        {

            var ex = Assert.Throws<ArgumentNullException>(() => _checkout.Scan(null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'item cannot be null or whitespace')"));
        }

    }
}