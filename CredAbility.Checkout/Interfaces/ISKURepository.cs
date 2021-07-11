using CredAbility.Checkout.Models;

namespace CredAbility.Checkout.Interfaces
{
    public interface ISKURepository
    {
        void AddItem(SKUItem skuItem);
        SKUItem Get(string sku);
    }
}
