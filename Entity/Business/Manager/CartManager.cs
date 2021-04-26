using Entity.Business.Abstract;
using Entity.Domain.Entity;
using Entity.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Business.Manager
{
    public class CartManager : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartLine != null)
            {
                cartLine.Quantity++;
                return;
            }
            else
            {
                cart.CartLines.Add(new CartLine { Product = product, Quantity = 1 });
            }
        }

        
        public List<CartLine> List(Cart cart)
        {
            return cart.CartLines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            var product=cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            if (product.Quantity == 1)
            {
                cart.CartLines.Remove(product);
            }
            else
            {
                product.Quantity--;
            }




        }
    }
}
