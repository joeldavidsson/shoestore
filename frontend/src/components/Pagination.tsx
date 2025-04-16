import React from 'react';
import { PaginationComponentProps } from '../types/pagination';

const Pagination = ({ currentPage, totalItems = 0, pageSize, onPageChange, isLoading }: PaginationComponentProps) => {
  const totalPages = Math.ceil(totalItems / pageSize);

  const handlePageChange = (page: number) => {
    onPageChange(page);
  };

  return (
    <div className="pagination">
      <button
        onClick={() => handlePageChange(currentPage - 1)}
        disabled={currentPage === 1 || isLoading}
      >
        Previous
      </button>
      <span>
        Page {currentPage} of {totalPages} (Total Items: {totalItems})
      </span>
      <button
        onClick={() => handlePageChange(currentPage + 1)}
        disabled={currentPage === totalPages || isLoading}
      >
        Next
      </button>
    </div>
  );
};

export default Pagination;