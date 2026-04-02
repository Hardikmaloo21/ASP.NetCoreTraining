import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

import { Link } from "react-router-dom";


export default function Login() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [role, setRole] = useState("user");

  const handleLogin = () => {
    login(role);
    navigate("/dashboard");
  };

  return (
    <div className="auth-container">
      <div className="card auth-card">
        <h2>Login</h2>

        <select
          className="input"
          value={role}
          onChange={(e) => setRole(e.target.value)}
        >
          <option value="user">User</option>
          <option value="admin">Admin</option>
        </select>

        <button className="button" onClick={handleLogin}>
          Login
        </button>

        <div>
      
      <br />
      <p>
        Don't have an account? <Link to="/register">Register</Link>
      </p>
    </div>
      </div>
      
    </div>
    
    
  );
}