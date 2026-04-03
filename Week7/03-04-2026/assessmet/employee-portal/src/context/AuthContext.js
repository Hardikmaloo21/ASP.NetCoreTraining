import React, { createContext, useState } from "react";

export const AuthContext = createContext();

const users = [
  { id: 1, username: "admin", password: "123", role: "admin" },
  { id: 2, username: "emp", password: "123", role: "employee" }
];

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  // Login user
  const login = (username, password) => {
    const foundUser = users.find(
      (u) => u.username === username && u.password === password
    );

    if (foundUser) {
      setUser(foundUser);
      return true;
    }
    return false;
  };

  // Logout user
  const logout = () => setUser(null);

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};