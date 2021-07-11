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

        [Test]
        public void ScanItemAQuantityLessThanSpecialQuantity_Calculate_FullPriceTimesQuantityReturn()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(100, result);
        }

        [Test]
        public void ScanItemCMultipleNotQuantitySpecial_Calculate_FullPriceTimesQuantityReturn()
        {
            _checkout.Scan("C");
            _checkout.Scan("C");
            _checkout.Scan("C");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(60, result);
        }

        [Test]
        public void ScanMultiItemsAAnyOrderForDiscountTotal_Calculate_SpecialPriceReturn()
        {
            _checkout.Scan("A");
            _checkout.Scan("C");
            _checkout.Scan("A");
            _checkout.Scan("D");
            _checkout.Scan("A");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(165, result);
        }

        [Test]
        public void ScanMultiItemsABAnyOrderForMultiDiscountTotal_Calculate_MultiSpecialPriceReturn()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("A");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(175, result);
        }

        [Test]
        public void ScanSameItemBMultipleTimes_Calculate_MultiBSpecialPriceReturn()
        {
            _checkout.Scan("B");
            _checkout.Scan("B");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("B");
            var result = _checkout.GetTotalPrice();

            Assert.AreEqual(110, result);
        }
    }
}
