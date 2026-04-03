import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const [data, setData] = useState({ username: "", password: "" });

  // Handle login
  const handleLogin = () => {
    const success = login(data.username, data.password);

    if (success) {
      if (data.username === "admin") navigate("/admin");
      else navigate("/employee");
    } else {
      alert("Invalid credentials");
    }
  };

  return (
    <div style={{
      display: "flex",
      height: "100vh",
      justifyContent: "center",
      alignItems: "center",
      background: "linear-gradient(120deg,#1d2671,#c33764)"
    }}>
      <div style={{
        padding: "30px",
        background: "white",
        borderRadius: "10px",
        display: "flex",
        flexDirection: "column",
        gap: "10px"
      }}>
        <h2>Login</h2>
        <input placeholder="Username" onChange={e => setData({ ...data, username: e.target.value })} />
        <input type="password" placeholder="Password" onChange={e => setData({ ...data, password: e.target.value })} />
        <button onClick={handleLogin}>Login</button>
      </div>
    </div>
  );
};

export default Login;