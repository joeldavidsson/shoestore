import React from 'react';
import { Link } from 'react-router-dom';

const ProductCard = ({ product }: { product: { id: number; name: string; price: number } }) => {
  return (
    <div className="product-card">
      <h3>{product.name}</h3>
      <p>Price: ${product.price}</p>
      <Link to={`/product/${product.id}`}>View Details</Link>
      <button>Add to Cart</button>
    </div>
  );
};

export default ProductCard;
