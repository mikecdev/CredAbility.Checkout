using CredAbility.Checkout.Models;

namespace CredAbility.Checkout.Interfaces
{
    public interface ISKUSpecialPriceRepository
    {
        void AddItem(SKUSpecialPrice specialPrice);
        SKUSpecialPrice Get(string sku);
    }
}
