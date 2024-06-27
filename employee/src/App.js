import React from 'react';
import { BrowserRouter as Router, Route, Routes, Outlet } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import LoginPage from './components/LoginPage';
import Dashboard from './pages/Dashboard';
import PrivateRoute from './utils/PrivateRoute';
import NotFound from './pages/NotFound';
// import Routes from './routes';
import { Provider } from 'react-redux';
import Navbar from './components/Navbar';
import EmployeeList from './components/EmployeeList';
import EmployeeForm from './components/EmployeeForm';
import { store } from './redux/store';
import { ToastContainer } from 'react-toastify';
import Registration from './components/Registration';

function App() {
  return (
    // <AuthProvider>
    //   <Routes />
    // </AuthProvider>
    <Provider store={store}>
      <Router>
        <Navbar />
        <Outlet />
        <ToastContainer />
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<Registration />} />
          <Route path="/" element={<PrivateRoute><Dashboard /></PrivateRoute>} />
          <Route path="/employees" element={<PrivateRoute><EmployeeList /></PrivateRoute>} />
          <Route path="/employee/add" element={<PrivateRoute><EmployeeForm /></PrivateRoute>} />
          <Route path="/employee/edit/:id" element={<PrivateRoute><EmployeeForm /></PrivateRoute>} />
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;
