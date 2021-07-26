using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThinkMoney.Checkout.Repositories
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

            if (skuItem.SKU == null)
                throw new ArgumentException($"{nameof(skuItem)}.{nameof(skuItem.SKU)} cannot be null or whitespace.");

            if (skuItem.UnitPrice == 0)
                throw new ArgumentException($"{nameof(skuItem)}.{nameof(skuItem.UnitPrice)} cannot be 0.");

            _skuList.Add(skuItem);
        }

        public SKUItem Get(string sku)
        {
            return _skuList.Where(x => x.SKU == sku).FirstOrDefault();
        }
    }
}
