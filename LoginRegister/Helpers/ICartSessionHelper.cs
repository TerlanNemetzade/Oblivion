using Entity.DomainModel;


namespace Oblivion.Helpers
{
    public interface ICartSessionHelper
    {
        Cart GetCart(string key);
        void SetCart(string key,Cart cart);
        void Clear();
    }
}
