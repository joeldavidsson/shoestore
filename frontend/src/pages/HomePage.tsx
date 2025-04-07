import React from 'react';
import ProductList from '../components/ProductList';

const HomePage = () => {
  return (
    <main className="home-page">
      <h2>Welcome to ShoeStore</h2>
      <ProductList />
    </main>
  );
};

export default HomePage;
