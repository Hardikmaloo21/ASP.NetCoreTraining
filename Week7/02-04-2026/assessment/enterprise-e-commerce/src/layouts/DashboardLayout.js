import { Outlet, Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function DashboardLayout() {
  const { logout, user } = useAuth();

  return (
    <div>
      <header style={styles.header}>
        <h2>Dashboard ({user?.role})</h2>
        <button onClick={logout}>Logout</button>
      </header>

      <nav style={styles.nav}>
        <Link to="/dashboard">Home</Link>
        <Link to="/dashboard/analytics">Analytics</Link>
        <Link to="/dashboard/settings">Settings</Link>
      </nav>

      <div style={styles.content}>
        <Outlet />
      </div>
    </div>
  );
}

const styles = {
  header: {
    background: "#222",
    color: "white",
    padding: "10px",
    display: "flex",
    justifyContent: "space-between",
  },
  nav: {
    background: "#ddd",
    padding: "10px",
    display: "flex",
    gap: "10px",
  },
  content: {
    padding: "20px",
  },
};