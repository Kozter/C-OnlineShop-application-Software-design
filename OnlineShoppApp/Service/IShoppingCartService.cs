using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppApp.Service
{
    public interface IShoppingCartService
    {
        public void AddToCart(Dictionary<int, List<CartItem>> cartItems, CartItem cartItem);
        public void ShowCartItems(List<CartItem> cartItems);
        public Task Checkout(List<CartItem> cartItems);
    }
}
