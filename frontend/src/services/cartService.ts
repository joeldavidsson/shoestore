import { fetchAuthApi } from '../api/api';
import { Cart, CartItem } from '../types/cart';

export const cartService = {
  async getCart(): Promise<Cart> {
    return await fetchAuthApi('api/cart');
  },

  async addToCart(item: CartItem): Promise<Cart> {
    return await fetchAuthApi('api/cart/items', {
      method: 'POST',
      body: JSON.stringify(item),
    });
  },

  async updateQuantity(productId: number, quantity: number): Promise<Cart> {
    return await fetchAuthApi(`api/cart/items/${productId}`, {
      method: 'PUT',
      body: JSON.stringify(quantity),
    });
  },

  async removeItem(productId: number): Promise<void> {
    await fetchAuthApi(`api/cart/items/${productId}`, {
      method: 'DELETE',
    });
  },

  async clearCart(): Promise<void> {
    await fetchAuthApi('api/cart/clear', {
      method: 'DELETE',
    });
  },
};
