using CredAbility.Checkout.Concrete;
using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using CredAbility.Checkout.Repository;
using NUnit.Framework;
using System;

namespace CredAbility.Checkout.Tests
{
    public class CheckoutScanCalculationTests
    {
        private ICheckout _checkout;
        private ISKURepository _skuRepository;

        [SetUp]
        public void Setup()
        {
            _skuRepository = new SKURepositoryMock();
            _skuRepository.AddItem(new SKUItem() { SKU = "A", UnitPrice = 50 });
            _skuRepository.AddItem(new SKUItem() { SKU = "B", UnitPrice = 30 });
            _skuRepository.AddItem(new SKUItem() { SKU = "C", UnitPrice = 20 });
            _skuRepository.AddItem(new SKUItem() { SKU = "D", UnitPrice = 15 });

            _checkout = new CheckOut(_skuRepository);
        }

        [Test]
        public void ZeroItems_Calculate_ZeroReturn()
        {
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void ScanNullItem_Calculate_ThrowsArgumentNullException()
        {

            var ex = Assert.Throws<ArgumentNullException>(() => _checkout.Scan(null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'item cannot be null or whitespace')"));
        }

        [Test]
        public void ScanNoneExistingItem_Calculate_ZeroReturn()
        {
            _checkout.Scan("Z");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void ScanExistingItem_Calculate_50Return()
        {
            _checkout.Scan("A");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(50, result);
        }

        [Test]
        public void ScanDifferentPricingItems_Calculate_80Return()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(80, result);
        }

        [Test]
        public void ScanDifferentPricingItems_Calculate_100Return()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(100, result);
        }

        [Test]
        public void ScanDifferentPricingItems_Calculate_115Return()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("D");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(115, result);
        }
    }
}