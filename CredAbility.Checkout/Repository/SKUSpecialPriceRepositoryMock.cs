using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CredAbility.Checkout.Repository
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

            if (specialPrice.SKU == null)
                throw new ArgumentException($"{nameof(specialPrice)}.{nameof(specialPrice.SKU)} cannot be null or whitespace.");

            if (specialPrice.SpecialPrice == 0)
                throw new ArgumentException($"{nameof(specialPrice)}.{nameof(specialPrice.SpecialPrice)} cannot be 0.");

            if (specialPrice.Quantity == 0)
                throw new ArgumentException($"{nameof(specialPrice)}.{nameof(specialPrice.Quantity)} cannot be 0.");

            _specialPriceList.Add(specialPrice);
        }

        public SKUSpecialPrice Get(string sku)
        {
            return _specialPriceList.Where(x => x.SKU == sku).FirstOrDefault();
        }
    }
}
