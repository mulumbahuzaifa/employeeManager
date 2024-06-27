import React, { createContext, useState, useEffect, useContext,useMemo } from 'react';
import axios from 'axios';

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
//   const [auth, setAuth] = useState({
//     token: localStorage.getItem('token'),
//     isAuthenticated: false,
//   });

//   useEffect(() => {
//     if (auth.token) {
//       setAuth({ ...auth, isAuthenticated: true });
//     }
//   }, [auth.token]);

//   const login = async (username, password) => {
//     const response = await axios.post('/api/account/login', { username, password });
//     localStorage.setItem('token', response.data.token);
//     setAuth({ token: response.data.token, isAuthenticated: true });
//   };

//   const logout = () => {
//     localStorage.removeItem('token');
//     setAuth({ token: null, isAuthenticated: false });
//   };

// State to hold the authentication token
const [token, setToken_] = useState(localStorage.getItem("token"));

// Function to set the authentication token
const setToken = (newToken) => {
  setToken_(newToken);
};

useEffect(() => {
  if (token) {
    axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    localStorage.setItem('token',token);
  } else {
    delete axios.defaults.headers.common["Authorization"];
    localStorage.removeItem('token')
  }
}, [token]);

// Memoized value of the authentication context
const contextValue = useMemo(
  () => ({
    token,
    setToken,
  }),
  [token]
);

  return (
    // <AuthContext.Provider value={{ auth, login, logout }}>
    //   {children}
    // </AuthContext.Provider>
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

export const useAuth = () => {
    return useContext(AuthContext);
};

export { AuthContext, AuthProvider };