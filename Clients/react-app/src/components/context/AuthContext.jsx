import React, { createContext, useContext, useState } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [userRole, setUserRole] = useState(null);
  const [userDetails, setUserDetails] = useState(null); // not sure if this userDetails state is a security threat

  return (
    <AuthContext.Provider
      value={{ userRole, setUserRole, userDetails, setUserDetails }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  return useContext(AuthContext);
};
