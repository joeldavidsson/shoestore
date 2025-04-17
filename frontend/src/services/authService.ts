import {
  User,
  AuthResponse,
  LoginCredentials,
  RegisterFormData,
} from '../types/auth';

const API_URL = 'http://localhost:5236/api/auth';

export const authService = {
  async login({ email, password }: LoginCredentials): Promise<AuthResponse> {
    try {
      console.log('Attempting login with:', { email }); // Don't log password

      const response = await fetch(`${API_URL}/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
        },
        body: JSON.stringify({ email, password }),
      });

      const data = await response.json();
      console.log('Login response:', {
        status: response.status,
        ok: response.ok,
        data,
      });

      if (!response.ok) {
        throw new Error(data.message || data.title || 'Login failed');
      }

      if (!data.token || !data.user) {
        console.error('Invalid response format:', data);
        throw new Error('Invalid response format from server');
      }

      // Store auth data
      localStorage.setItem('token', data.token);
      localStorage.setItem('user', JSON.stringify(data.user));

      return data;
    } catch (error) {
      console.error('Login error details:', error);
      throw error;
    }
  },

  async register(credentials: RegisterFormData): Promise<void> {
    const response = await fetch(`${API_URL}/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email: credentials.email,
        password: credentials.password,
        confirmPassword: credentials.confirmPassword,
        firstName: credentials.firstName,
        lastName: credentials.lastName,
        phoneNumber: credentials.phoneNumber,
        address: credentials.address,
        zipCode: credentials.zipCode,
        city: credentials.city,
        country: credentials.country,
      }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || 'Registration failed');
    }
  },

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  },

  getToken(): string | null {
    return localStorage.getItem('token');
  },

  getCurrentUser(): User | null {
    const userStr = localStorage.getItem('user');
    return userStr ? JSON.parse(userStr) : null;
  },

  isAuthenticated(): boolean {
    return !!this.getToken();
  },
};
