import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const validatePassword = (password: string): boolean => {
  const minLength = 6;
  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  const hasNumber = /\d/.test(password);
  const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);

  return (
    password.length >= minLength &&
    hasUpperCase &&
    hasLowerCase &&
    hasNumber &&
    hasSpecialChar
  );
};

const RegisterPage = () => {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: '',
    zipCode: '',
    city: '',
    country: ''
  });
  const [error, setError] = useState<string | string[]>('');
  const navigate = useNavigate();
  const { register } = useAuth();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!validatePassword(formData.password)) {
      setError([
        'Password must:',
        '- Be at least 6 characters long',
        '- Contain at least one uppercase letter',
        '- Contain at least one lowercase letter',
        '- Contain at least one number',
        '- Contain at least one special character'
      ]);
      return;
    }

    if (formData.password !== formData.confirmPassword) {
      setError('Passwords do not match');
      return;
    }

    try {
      await register(formData);
      navigate('/login');
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError('Registration failed');
      }
    }
  };

  return (
    <div className="h-full w-full flex flex-col items-center justify-center bg-gray-50 py-12 px-4">
      <div className="max-w-md w-full flex flex-col space-y-8 p-8 bg-white rounded-lg shadow-md">
        <h2 className="text-center text-5xl font-bold">Create Account</h2>
        {error && (
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
            {Array.isArray(error) ? (
              <ul className="list-disc pl-4">
                {error.map((err, index) => (
                  <li key={index}>{err}</li>
                ))}
              </ul>
            ) : (
              error
            )}
          </div>
        )}
        <form onSubmit={handleSubmit} className="space-y-4 flex flex-col">
          <div className="grid grid-cols-2 gap-4">
            <input
              name="firstName"
              type="text"
              required
              className="px-3 py-2 border rounded-md"
              placeholder="First Name"
              value={formData.firstName}
              onChange={handleChange}
            />
            <input
              name="lastName"
              type="text"
              required
              className="px-3 py-2 border rounded-md"
              placeholder="Last Name"
              value={formData.lastName}
              onChange={handleChange}
            />
          </div>
          <input
            name="email"
            type="email"
            required
            className="w-full px-3 py-2 border rounded-md"
            placeholder="Email Address"
            value={formData.email}
            onChange={handleChange}
          />
          <input
            name="phoneNumber"
            type="tel"
            required
            className="w-full px-3 py-2 border rounded-md"
            placeholder="Phone Number"
            value={formData.phoneNumber}
            onChange={handleChange}
          />
          <input
            name="address"
            type="text"
            required
            className="w-full px-3 py-2 border rounded-md"
            placeholder="Address"
            value={formData.address}
            onChange={handleChange}
          />
          <div className="grid grid-cols-2 gap-4">
            <input
              name="zipCode"
              type="text"
              required
              className="px-3 py-2 border rounded-md"
              placeholder="ZIP Code"
              value={formData.zipCode}
              onChange={handleChange}
            />
            <input
              name="city"
              type="text"
              required
              className="px-3 py-2 border rounded-md"
              placeholder="City"
              value={formData.city}
              onChange={handleChange}
            />
          </div>
          <input
            name="country"
            type="text"
            required
            className="w-full px-3 py-2 border rounded-md"
            placeholder="Country"
            value={formData.country}
            onChange={handleChange}
          />
          <input
            name="password"
            type="password"
            required
            className="w-full px-3 py-2 border rounded-md"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
          />
          <input
            name="confirmPassword"
            type="password"
            required
            className="w-full px-3 py-2 border rounded-md"
            placeholder="Confirm Password"
            value={formData.confirmPassword}
            onChange={handleChange}
          />
          <button
            type="submit"
            className="w-full py-2 px-4 bg-blue-600 text-white rounded-md hover:bg-blue-700"
          >
            Register
          </button>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;