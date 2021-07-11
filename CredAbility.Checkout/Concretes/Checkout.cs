using CredAbility.Checkout.Interfaces;
using System;
using System.Collections.Generic;

namespace CredAbility.Checkout.Concrete
{
    public class CheckOut : ICheckout
    {
        private readonly List<string> _basket;

        public CheckOut()
        {
            _basket = new List<string>();
        }

        public int GetTotalPrice()
        {
            return 0;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException("item cannot be null or whitespace");

            _basket.Add(item);
        }
    }
}
