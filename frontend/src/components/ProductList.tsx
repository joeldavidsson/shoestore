import React from 'react';
import ProductCard from './ProductCard';

const ProductList = () => {
  const products = [
    { id: 1, name: 'Running Shoes', price: 50 },
    { id: 2, name: 'Casual Sneakers', price: 40 },
    { id: 3, name: 'Formal Shoes', price: 70 },
  ];

  return (
    <div className="product-list">
      {products.map((product) => (
        <ProductCard key={product.id} product={product} />
      ))}
    </div>
  );
};

export default ProductList;
