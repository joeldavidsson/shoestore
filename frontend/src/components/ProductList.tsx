import React from 'react';
import ProductCard from './ProductCard';
import Pagination from './Pagination';
import { useProducts } from '../hooks/useProducts';
import { useState } from 'react';

const ProductList = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 20;

  const { products, loading, error, totalItems } = useProducts({
    page: currentPage,
    pageSize
  });

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  }

  if (loading) return <div>Loading products...</div>;
  if (error) return <div>Error loading products: {error}</div>;
  if (!products?.length) return <div>No products found</div>;

  return (
    <div>
      <div className="product-list">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
      <Pagination
        currentPage={currentPage}
        totalItems={totalItems}
        pageSize={pageSize}
        onPageChange={handlePageChange}
        isLoading={loading}
      />
    </div>
  );
};

export default ProductList;