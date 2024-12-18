/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        primary: {
          800: '#4A4459',
          900: '#1D1B20',
        }
      }
    },
  },
  plugins: [],
}

