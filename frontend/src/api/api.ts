import { authService } from '../services/authService';

export const API_BASE_URL = 'http://localhost:5236/';

export const fetchApi = async (endpoint: string, options?: RequestInit) => {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...options?.headers,
    },
  });

  if (!response.ok) {
    throw new Error(`API error: ${response.statusText}`);
  }

  return response.json();
};

export const fetchAuthApi = async (endpoint: string, options?: RequestInit) => {
  const token = authService.getToken();

  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...(token && { Authorization: `Bearer ${token}` }),
      ...options?.headers,
    },
  });

  if (!response.ok) {
    if (response.status === 401) {
      authService.logout();
      window.location.href = '/login';
      throw new Error('Session expired');
    }
    throw new Error(`API error: ${response.statusText}`);
  }

  return response.json();
};