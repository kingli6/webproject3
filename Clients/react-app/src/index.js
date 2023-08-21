import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { AuthProvider } from './components/context/AuthContext'; // Import AuthProvider

ReactDOM.createRoot(document.querySelector('#root')).render(
  <AuthProvider>
    <App />
  </AuthProvider>
);
