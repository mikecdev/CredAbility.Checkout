using CredAbility.Checkout.Constants;
using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CredAbility.Checkout.Repositories
{
    public class SKUSpecialPriceRepositoryMock : ISKUSpecialPriceRepository
    {

        private readonly List<SKUSpecialPrice> _specialPriceList;
        public SKUSpecialPriceRepositoryMock()
        {
            _specialPriceList = new List<SKUSpecialPrice>();
        }

        public void AddItem(SKUSpecialPrice specialPrice)
        {
            if (specialPrice == null)
                throw new ArgumentNullException(nameof(specialPrice));

            if (string.IsNullOrWhiteSpace(specialPrice.SKU))
                throw new ArgumentException($"{nameof(specialPrice)}.{nameof(specialPrice.SKU)} {Message.CANNOT_BE_NULL_OR_WHITESPACE}");

            if (specialPrice.SpecialPrice <= 0)
                throw new ArgumentException($"{nameof(specialPrice)}.{nameof(specialPrice.SpecialPrice)} {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}");

            if (specialPrice.Quantity <= 0)
                throw new ArgumentException($"{nameof(specialPrice)}.{nameof(specialPrice.Quantity)} {Message.CANNOT_BE_LESSTHAN_OR_EQUAL_ZERO}");

            _specialPriceList.Add(specialPrice);
        }

        public SKUSpecialPrice Get(string sku)
        {
            return _specialPriceList.Where(x => x.SKU == sku).FirstOrDefault();
        }
    }
}
