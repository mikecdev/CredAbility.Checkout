using CredAbility.Checkout.Interfaces;
using System;
using System.Collections.Generic;

namespace CredAbility.Checkout.Concrete
{
    public class CheckOut : ICheckout
    {
        private readonly List<string> _basket;
        private readonly ISKURepository _skuRepository;

        public CheckOut(ISKURepository skuRepository)
        {
            _basket = new List<string>();
            _skuRepository = skuRepository;
        }

        public int GetTotalPrice()
        {
            var total = 0;

            foreach (var item in _basket)
            {
                var sku = _skuRepository.Get(item);
                total += sku?.UnitPrice ?? 0;
            }

            return total;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException("item cannot be null or whitespace");

            _basket.Add(item);
        }
    }
}
