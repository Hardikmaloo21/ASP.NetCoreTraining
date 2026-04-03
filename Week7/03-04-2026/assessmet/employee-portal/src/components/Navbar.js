import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

const Navbar = () => {
  const { user, logout } = useContext(AuthContext);
  const navigate = useNavigate();

  // Handles logout and redirects
  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <div style={styles.nav}>
      <h2>🚀 Employee Portal</h2>
      {user && (
        <div>
          <span>{user.username} ({user.role})</span>
          <button onClick={handleLogout}>Logout</button>
        </div>
      )}
    </div>
  );
};

const styles = {
  nav: {
    display: "flex",
    justifyContent: "space-between",
    padding: "15px",
    background: "linear-gradient(90deg,#0f2027,#203a43,#2c5364)",
    color: "white"
  }
};

export default Navbar;