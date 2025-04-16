import { useState, useEffect } from 'react';
import { Product } from '../types/product';
import { productService } from '../services/productService';
import { PaginationParams } from '../types/pagination';

export const useProducts = (params: PaginationParams) => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [totalItems, setTotalItems] = useState(0);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        setLoading(true);

        const response = await productService.getAllProducts({
          page: params.page,
          pageSize: params.pageSize,
        });

        if (response.items && Array.isArray(response.items)) {
          setProducts(response.items);
          setTotalItems(response.totalCount ?? 0);
        }
      } catch (err) {
        setError(err instanceof Error ? err.message : 'An error occurred');
        setProducts([]);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, [params.page, params.pageSize]);

  return { products, loading, error, totalItems };
};
