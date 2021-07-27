using CredAbility.Checkout.Constants;
using CredAbility.Checkout.Interfaces;
using CredAbility.Checkout.Repositories;
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

        public CheckOut(ISKURepository skuRepository, ISKUSpecialPriceRepository skuSpecialPriceRepository) : this(skuRepository)
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
            var quantity = group.Count();
            var total = 0;

            if (specialPrice != null && specialPrice.Quantity <= quantity)
            {
                var multiDiscountQuantity = (quantity / specialPrice.Quantity);
                total = specialPrice.SpecialPrice * multiDiscountQuantity;

                quantity %= specialPrice.Quantity;
            }
            
            if (quantity > 0)
            {
                var sku = _skuRepository.Get(group.Key);
                total+= sku?.UnitPrice * quantity ?? 0;
            }

            return total;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(Message.ITEM_CANNOT_BE_NULL_OR_WHITESPACE);

            _basket.Add(item);
        }
    }
}
