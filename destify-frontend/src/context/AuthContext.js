import React, { createContext, useState } from "react";
import {  useNavigate } from "react-router-dom";


export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [token, setToken] = useState(localStorage.getItem("token") || "");
  const navigate = useNavigate();

  const login = (newToken) => {
    setToken(newToken);
    setIsAuthenticated(true);
    localStorage.setItem("token", newToken);
  };

  const logout = () => {
    setToken("");
    setIsAuthenticated(false);
    localStorage.removeItem("token");
    navigate("/");
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
