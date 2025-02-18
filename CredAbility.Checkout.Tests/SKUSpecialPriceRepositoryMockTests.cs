﻿using CredAbility.Checkout.Constants;
using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using CredAbility.Checkout.Repositories;
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
            Assert.That(ex.Message, Is.EqualTo($"{Message.VALUE_CANNOT_BE_NULL} (Parameter 'specialPrice')"));
        }

        [Test]
        public void AddSpecialPriceSKUNull_ThrowsArgumentSKUNullException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SpecialPrice = 50 }));
            Assert.That(ex.Message, Is.EqualTo($"specialPrice.SKU {Message.CANNOT_BE_NULL_OR_WHITESPACE}"));
        }

        [Test]
        public void AddSpecialPriceZero_ThrowsArgumentSKUSpecialPriceZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "A", SpecialPrice = 0 }));
            Assert.That(ex.Message, Is.EqualTo($"specialPrice.SpecialPrice {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}"));
        }

        [Test]
        public void AddSpecialPriceNegative_ThrowsArgumentSKUSpecialPriceZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "A", SpecialPrice = -1 }));
            Assert.That(ex.Message, Is.EqualTo($"specialPrice.SpecialPrice {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}"));
        }

        [Test]
        public void AddSpecialPriceQuantityZero_ThrowsArgumentSKUSpecialPriceQuantityZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "A", SpecialPrice = 130, Quantity = 0 }));
            Assert.That(ex.Message, Is.EqualTo($"specialPrice.Quantity {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}"));
        }


        [Test]
        public void AddSpecialPriceQuantityNegative_ThrowsArgumentSKUSpecialPriceQuantityZeroException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _specialPriceRepository.AddItem(new SKUSpecialPrice() { SKU = "A", SpecialPrice = 130, Quantity = -1 }));
            Assert.That(ex.Message, Is.EqualTo($"specialPrice.Quantity {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}"));
        }

        [Test]
        public void AddSpecialPrice_GetSpecialPrice_AreSameReturned()
        {
            var actual = new SKUSpecialPrice() { SKU = "A", Quantity = 3, SpecialPrice = 130 };
            _specialPriceRepository.AddItem(actual);


            Assert.AreSame(_specialPriceRepository.Get("A"), actual);
        }

        [Test]
        public void AddSpecialPrice_GetSpecialPrice_AreNotSameReturned()
        {
            var actual = new SKUSpecialPrice() { SKU = "A", Quantity = 3, SpecialPrice = 130 };
            _specialPriceRepository.AddItem(actual);


            Assert.AreNotSame(_specialPriceRepository.Get("B"), actual);
        }
    }
}
