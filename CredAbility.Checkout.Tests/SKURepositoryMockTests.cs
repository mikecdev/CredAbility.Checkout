using CredAbility.Checkout.Constants;
using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using CredAbility.Checkout.Repositories;
using NUnit.Framework;
using System;


namespace CredAbility.Checkout.Tests
{
    public class SKURepositoryMockTests
    {
        private ISKURepository _skuRepository;

        [SetUp]
        public void Setup()
        {
            _skuRepository = new SKURepositoryMock();
        }

        [Test]
        public void AddNullItem_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _skuRepository.AddItem(null));
            Assert.That(ex.Message, Is.EqualTo($"{Message.VALUE_CANNOT_BE_NULL} (Parameter 'skuItem')"));
        }

        [Test]
        public void AddItemSKUNull_ThrowsArgumentSKUNullException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _skuRepository.AddItem(new SKUItem() { UnitPrice = 50 }));
            Assert.That(ex.Message, Is.EqualTo($"skuItem.SKU {Message.CANNOT_BE_NULL_OR_WHITESPACE}"));
        }

        [Test]
        public void AddItemPriceZero_ThrowsArgumentSKUUnitPriceZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _skuRepository.AddItem(new SKUItem() { SKU = "A", UnitPrice = 0 }));
            Assert.That(ex.Message, Is.EqualTo($"skuItem.UnitPrice {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}"));
        }

        [Test]
        public void AddItemPriceNegative_ThrowsArgumentSKUUnitPriceZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _skuRepository.AddItem(new SKUItem() { SKU = "A", UnitPrice = -1 }));
            Assert.That(ex.Message, Is.EqualTo($"skuItem.UnitPrice {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}"));
        }

        [Test]
        public void AddItem_GetItem_AreSameReturned()
        {
            var actual = new SKUItem() { SKU = "A", UnitPrice = 50 };
            _skuRepository.AddItem(actual);

            Assert.AreSame(_skuRepository.Get("A"), actual);
        }

        [Test]
        public void AddItem_GetItem_AreNotSameReturned()
        {
            var actual = new SKUItem() { SKU = "A", UnitPrice = 50 };
            _skuRepository.AddItem(actual);


            Assert.AreNotSame(_skuRepository.Get("B"), actual);
        }
    }
}
