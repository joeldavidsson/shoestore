import { fetchApi } from '../api/api';
import { Product } from '../types/product';
import { PaginationParams, PaginatedResponse } from '../types/pagination';

export const productService = {
  async getAllProducts(
    params: PaginationParams,
  ): Promise<PaginatedResponse<Product>> {
    const { page = 1, pageSize = 20 } = params;
    return await fetchApi(`api/products?page=${page}&pageSize=${pageSize}`);
  },

  async getProductById(id: number): Promise<Product> {
    return await fetchApi(`api/products/${id}`);
  },

  async getProductsByBrand(
    brand: string,
    params: PaginationParams,
  ): Promise<PaginatedResponse<Product>> {
    const { page = 1, pageSize = 20 } = params;
    return await fetchApi(
      `api/products/brand/${brand}?page=${page}&pageSize=${pageSize}`,
    );
  },

  async searchProducts(
    searchTerm: string,
    params: PaginationParams,
  ): Promise<PaginatedResponse<Product>> {
    const { page = 1, pageSize = 20 } = params;
    return await fetchApi(
      `api/products/search?term=${encodeURIComponent(searchTerm)}&page=${page}&pageSize=${pageSize}`,
    );
  },
};
