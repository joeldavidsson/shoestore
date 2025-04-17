import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Product } from '../types/product';
import { cartService } from '../services/cartService';
import { CartItem } from '../types/cart';

interface ProductCardProps {
  product: Product;
}

const ProductCard = ({ product }: ProductCardProps) => {
  const [isLoading, setIsLoading] = useState(false);

  const handleAddToCart = async () => {
    try {
      setIsLoading(true);
      const cartItem: CartItem = {
        productId: product.id,
        name: product.brand,
        description: product.description,
        brand: product.brand,
        price: product.price.toString(),
        imageUrl: product.image,
        quantity: 1,
      };

      await cartService.addToCart(cartItem);
    } catch (error) {
      console.error('Failed to add item to cart:', error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className='product-card'>
      {product.image && <img src={product.image} alt={product.brand} />}
      <h3>{product.brand}</h3>
      <p>{product.description}</p>
      <p>Price: ${product.price}</p>
      <button><Link to={`/product/${product.id}`}>View Details</Link></button>
      <button
        onClick={handleAddToCart}
        disabled={isLoading}
      >
        {isLoading ? 'Adding...' : 'Add to Cart'}
      </button>
    </div>
  );
};

export default ProductCard;
