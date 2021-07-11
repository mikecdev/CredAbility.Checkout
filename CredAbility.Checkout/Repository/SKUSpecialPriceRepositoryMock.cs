using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Models;
using System;

namespace CredAbility.Checkout.Repository
{
    public class SKUSpecialPriceRepositoryMock : ISKUSpecialPriceRepository
    {
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
        }
    }
}
