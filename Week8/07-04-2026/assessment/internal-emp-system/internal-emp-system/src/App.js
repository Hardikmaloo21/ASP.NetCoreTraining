import { AuthProvider, useAuth } from "./context/AuthContext";
import { ThemeProvider } from "./context/ThemeContext";
import { EmployeeProvider } from "./context/EmployeeContext";

import Dashboard from "./pages/Dashboard";
import Analytics from "./pages/Analytics";
import Settings from "./pages/Settings";
import Login from "./pages/Login";

import { useState } from "react";
import { useTheme } from "./context/ThemeContext";

function Layout() {
  const [page, setPage] = useState("dashboard");
  const { logout, user } = useAuth();
  const { toggleTheme } = useTheme();

  return (
    <div className="app">
      {/* SIDEBAR */}
      <div className="sidebar">
        <h2>EMS</h2>

        <button onClick={() => setPage("dashboard")}>Dashboard</button>
        <button onClick={() => setPage("analytics")}>Analytics</button>
        <button onClick={() => setPage("settings")}>Settings</button>
      </div>

      {/* MAIN */}
      <div className="main">
        {/* TOPBAR */}
        <div className="topbar">
          <span>{user?.name}</span>
          <button onClick={toggleTheme}>Theme</button>
          <button onClick={logout}>Logout</button>
        </div>

        {/* CONTENT */}
        <div className="content">
          {page === "dashboard" && <Dashboard />}
          {page === "analytics" && <Analytics />}
          {page === "settings" && <Settings />}
        </div>
      </div>
    </div>
  );
}

function Main() {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? <Layout /> : <Login />;
}

export default function App() {
  return (
    <AuthProvider>
      <ThemeProvider>
        <EmployeeProvider>
          <Main />
        </EmployeeProvider>
      </ThemeProvider>
    </AuthProvider>
  );
}