import React from 'react';
import { Link } from 'react-router-dom';
import { Product } from '../types/product';

const ProductCard = ({ product }: { product: Product }) => {
  return (
    <div className='product-card'>
      {product.image && <img src={product.image} alt={product.brand} />}
      <h3>{product.brand}</h3>
      <p>{product.description}</p>
      <p>Price: ${product.price}</p>
      <Link to={`/product/${product.id}`}>View Details</Link>
      <button>Add to Cart</button>
    </div>
  );
};

export default ProductCard;
