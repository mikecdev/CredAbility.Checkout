using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CredAbility.Checkout.Concrete
{
    public class CheckOut : ICheckout
    {
        private readonly List<string> _basket;
        private readonly ISKURepository _skuRepository;
        private readonly ISKUSpecialPriceRepository _skuSpecialPriceRepository;

        public CheckOut(ISKURepository skuRepository)
        {
            _basket = new List<string>();
            _skuRepository = skuRepository;
        }

        public CheckOut(ISKURepository skuRepository, SKUSpecialPriceRepositoryMock skuSpecialPriceRepository) : this(skuRepository)
        {
            _skuSpecialPriceRepository = skuSpecialPriceRepository;
        }

        public int GetTotalPrice()
        {
            var total = 0;

            var itemGroups = _basket.GroupBy(_ => _);

            foreach (var group in itemGroups)
            {
                total += CalculatePrice(group);
            }

            return total;
        }

        private int CalculatePrice(IGrouping<string, string> group)
        {
            var specialPrice = _skuSpecialPriceRepository?.Get(group.Key);

            if (specialPrice != null && specialPrice.Quantity <= group.Count())
            {
                return specialPrice.SpecialPrice;
            }
            else
            {
                var sku = _skuRepository.Get(group.Key);
                return sku?.UnitPrice ?? 0;
            }


        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException("item cannot be null or whitespace");

            _basket.Add(item);
        }
    }
}
