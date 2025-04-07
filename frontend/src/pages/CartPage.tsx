import React from 'react';

const CartPage = () => {
  // Mock cart data
  const cartItems = [
    { id: 1, name: 'Running Shoes', price: 50, quantity: 1 },
    { id: 2, name: 'Casual Sneakers', price: 40, quantity: 2 },
  ];

  const total = cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0);

  return (
    <div className="cart-page">
      <h2>Your Cart</h2>
      {cartItems.map((item) => (
        <div key={item.id}>
          <p>{item.name}</p>
          <p>Price: ${item.price}</p>
          <p>Quantity: {item.quantity}</p>
        </div>
      ))}
      <h3>Total: ${total}</h3>
      <button>Checkout</button>
    </div>
  );
};

export default CartPage;
