// import React, { useContext } from 'react';
// import { Navigate, Outlet } from "react-router-dom";
// import { useAuth } from '../context/AuthContext';

// export const ProtectedRoute = () => {
//     const { token } = useAuth();
  
//     // Check if the user is authenticated
//     if (!token) {
//       // If not authenticated, redirect to the login page
//       return <Navigate to="/login" />;
//     }
  
//     // If authenticated, render the child routes
//     return <Outlet />;
//   };
import React from 'react';
import { useSelector } from 'react-redux';
import { Navigate } from 'react-router-dom';

const PrivateRoute = ({ children }) => {
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  return isAuthenticated ? children : <Navigate to="/login" />;
};

export default PrivateRoute;