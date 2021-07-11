﻿using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using CredAbility.Checkout.Repository;
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
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'skuItem')"));
        }

        [Test]
        public void AddItemSKUNull_ThrowsArgumentSKUNullException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _skuRepository.AddItem(new SKUItem() { UnitPrice = 50 }));
            Assert.That(ex.Message, Is.EqualTo("skuItem.SKU cannot be null or whitespace."));
        }

        [Test]
        public void AddItemPriceZero_ThrowsArgumentSKUUnitPriceZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _skuRepository.AddItem(new SKUItem() { SKU = "A", UnitPrice = 0 }));
            Assert.That(ex.Message, Is.EqualTo("skuItem.UnitPrice cannot be 0."));
        }
    }
}
