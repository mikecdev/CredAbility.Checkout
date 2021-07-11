using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using CredAbility.Checkout.Repository;
using NUnit.Framework;
using System;

namespace CredAbility.Checkout.Tests
{
    public class SKUSpecialPriceRepositoryMockTests
    {
        private ISKUSpecialPriceRepository _specialPriceRepository;

        [SetUp]
        public void Setup()
        {
            _specialPriceRepository = new SKUSpecialPriceRepositoryMock();
        }

        [Test]
        public void AddNullItem_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _specialPriceRepository.AddItem(null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'specialPrice')"));
        }

        [Test]
        public void AddSpecialPriceSKUNull_ThrowsArgumentSKUNullException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SpecialPrice = 50 }));
            Assert.That(ex.Message, Is.EqualTo("specialPrice.SKU cannot be null or whitespace."));
        }

        [Test]
        public void AddSpecialPriceZero_ThrowsArgumentSKUSpecialPriceZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "A", SpecialPrice = 0 }));
            Assert.That(ex.Message, Is.EqualTo("specialPrice.SpecialPrice cannot be 0."));
        }

        [Test]
        public void AddSpecialPriceQuantityZero_ThrowsArgumentSKUSpecialPriceQuantityZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "A", SpecialPrice = 130, Quantity = 0 }));
            Assert.That(ex.Message, Is.EqualTo("specialPrice.Quantity cannot be 0."));
        }
    }
}
