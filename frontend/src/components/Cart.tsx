import React from 'react';
import { useState, useEffect } from 'react';
import { cartService } from '../services/cartService';
import { Cart as CartType } from '../types/cart';

const Cart = () => {
  const [cart, setCart] = useState<CartType | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchCart();
  }, []);

  const fetchCart = async () => {
    try {
      setIsLoading(true);
      const data = await cartService.getCart();
      setCart(data);
    } catch (err) {
      setError('Failed to load cart');
      console.error(err);
    } finally {
      setIsLoading(false);
    }
  };

  const handleUpdateQuantity = async (productId: number, quantity: number) => {
    try {
      const updatedCart = await cartService.updateQuantity(productId, quantity);
      setCart(updatedCart);
    } catch (err) {
      setError('Failed to update quantity');
      console.error(err);
    }
  };

  const handleRemoveItem = async (productId: number) => {
    try {
      await cartService.removeItem(productId);
      fetchCart();
    } catch (err) {
      setError('Failed to remove item');
      console.error(err);
    }
  };

  if (isLoading) return <div>Loading cart...</div>;
  if (error) return <div className="error">{error}</div>;
  if (!cart || cart.items.length === 0) return <div>Your cart is empty</div>;

  return (
    <div className="cart flex flex-col items-center justify-center p-4">
      <h2>Your Cart</h2>
      <div className="cart-items">
        {cart.items.map((item) => (
          <div key={item.productId} className="cart-item h-36 w-full gap-3 flex flex-row items-center justify-between p-4 border-b border-gray-300">
            {item.imageUrl && (
              <img src={item.imageUrl} alt={item.name} className="h-28 w-28 object-cover" />
            )}
            <div className="cart-item-details flex items-center justify-center w-full">
              <h3>{item.name}</h3>
              <p>{item.description}</p>
              <p>Price: ${item.price}</p>
              <div className="quantity-controls flex flex-row items-center">
                <button
                  onClick={() => handleUpdateQuantity(item.productId, item.quantity - 1)}
                  disabled={item.quantity <= 1}
                >
                  -
                </button>
                <span>{item.quantity}</span>
                <button
                  onClick={() => handleUpdateQuantity(item.productId, item.quantity + 1)}
                >
                  +
                </button>
              </div>
              <button
                onClick={() => handleRemoveItem(item.productId)}
                className="remove-item"
              >
                Remove
              </button>
            </div>
          </div>
        ))}
      </div>
      <div className="cart-summary">
        <h3>Total: ${cart.totalAmount}</h3>
        <button className="checkout-button">Proceed to Checkout</button>
      </div>
    </div>
  );
};

export default Cart;