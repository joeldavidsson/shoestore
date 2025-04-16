import React, { createContext, useContext, useState, useEffect } from 'react';
import { authService } from '../services/authService';
import { AuthContextType, AuthProviderProps, User } from '../types/auth';

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const token = authService.getToken();
    if (token) {
      setIsAuthenticated(true);
      setUser(authService.getCurrentUser());
    }
  }, []);

  const login = async (email: string, password: string) => {
    const data = await authService.login({ email, password });
    setIsAuthenticated(true);
    setUser(data.user);
  };

  const logout = () => {
    authService.logout();
    setIsAuthenticated(false);
    setUser(null);
  };

  const register = async (email: string, password: string) => {
    await authService.register({
      email, password,
      confirmPassword: ''
    });
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, user, login, logout, register }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};