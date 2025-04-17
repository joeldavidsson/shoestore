import { defineConfig } from 'vite';
import eslint from 'vite-plugin-eslint';
import tailwindcss from '@tailwindcss/vite'
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react(), eslint(), tailwindcss()],
});
