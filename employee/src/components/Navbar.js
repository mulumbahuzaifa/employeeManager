import React from 'react';
import { Link } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { logout } from '../redux/authSlice';
import logo from "./luwa-logo.png";

export default function Navbar() {
    const dispatch = useDispatch();
    const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  
    return (
    <nav className="relative container mx-auto p-6">
        <div className="flex items-center justify-between">
            <div className="flex items-center space-x-20">
            <Link to="/">
                <img src={logo} alt="" />
            </Link>
            {/* <div className="hidden font-bold lg:flex">
                <Link to="/search" className="text-black hover:text-darkBlue">
                Search
                </Link>
            </div> */}
            </div>
            {isAuthenticated ? (
            <div className="hidden lg:flex items-center space-x-6 text-back">
                {/* <div className="hover:text-darkBlue">Welcome, {user?.userName}</div> */}
                <a
                onClick={() => dispatch(logout())}
                className="px-8 py-3 font-bold rounded text-black bg-lightGreen hover:opacity-70"
                >
                Logout
                </a>
                <Link to="/employees" className="hover:text-darkBlue">
                Employees
                </Link>
            </div>
            ) : (
            <div className="hidden lg:flex items-center space-x-6 text-back">
              
                <Link to="/login" className="hover:text-darkBlue px-8 py-3">
                Login
                </Link>
                <Link
                to="/register"
                className="px-8 py-3 font-bold rounded text-black bg-lightGreen hover:opacity-70"
                >
                Signup
                </Link>
            </div>
            )}
        </div>
        </nav>
      
    );
}
