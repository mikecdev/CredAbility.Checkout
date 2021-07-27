using CredAbility.Checkout.Constants;
using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CredAbility.Checkout.Repositories
{
    public class SKURepositoryMock : ISKURepository
    {
        private readonly List<SKUItem> _skuList;

        public SKURepositoryMock()
        {
            _skuList = new List<SKUItem>();
        }

        public void AddItem(SKUItem skuItem)
        {
            if (skuItem == null)
                throw new ArgumentNullException(nameof(skuItem));

            if (string.IsNullOrWhiteSpace(skuItem.SKU))
                throw new ArgumentException($"{nameof(skuItem)}.{nameof(skuItem.SKU)} {Message.CANNOT_BE_NULL_OR_WHITESPACE}");

            if (skuItem.UnitPrice <= 0)
                throw new ArgumentException($"{nameof(skuItem)}.{nameof(skuItem.UnitPrice)} {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}");

            _skuList.Add(skuItem);
        }

        public SKUItem Get(string sku)
        {
            return _skuList.Where(x => x.SKU == sku).FirstOrDefault();
        }
    }
}
