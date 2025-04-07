import React from 'react';
import { useParams } from 'react-router-dom';

const ProductDetailsPage = () => {
  const { id } = useParams();

  // Mock product data
  const product = { id, name: 'Running Shoes', price: 50, description: 'High-quality running shoes.' };

  return (
    <div className="product-details">
      <h2>{product.name}</h2>
      <p>Price: ${product.price}</p>
      <p>{product.description}</p>
      <button>Add to Cart</button>
    </div>
  );
};

export default ProductDetailsPage;
