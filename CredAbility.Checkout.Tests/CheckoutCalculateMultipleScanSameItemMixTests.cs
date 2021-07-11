using CredAbility.Checkout.Concrete;
using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using CredAbility.Checkout.Repository;
using NUnit.Framework;

namespace CredAbility.Checkout.Tests
{
    public class CheckoutCalculateMultipleScanSameItemMixTests
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

            var skuSpecialPriceRepository = new SKUSpecialPriceRepositoryMock();
            skuSpecialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "A", Quantity = 3, SpecialPrice = 130 });
            skuSpecialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "B", Quantity = 2, SpecialPrice = 45 });

            _checkout = new CheckOut(_skuRepository, skuSpecialPriceRepository);
        }

        [Test]
        public void ScanItemBForDiscountTotal_Calculate_SpecialPriceReturn()
        {
            _checkout.Scan("B");
            _checkout.Scan("A");
            _checkout.Scan("B");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(95, result);
        }

    }
}
