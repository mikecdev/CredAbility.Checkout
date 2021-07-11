using CredAbility.Checkout.Interfaces;
using System;

namespace CredAbility.Checkout.Concrete
{
    public class CheckOut : ICheckout
    {
        public int GetTotalPrice()
        {
            return 0;
        }

        public void Scan(string item)
        {
            throw new NotImplementedException();
        }
    }
}
