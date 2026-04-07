import { useState } from "react";
import { useAuth } from "../context/AuthContext";

export default function Login() {
  const { login } = useAuth();
  const [name, setName] = useState("");

  return (
    <div>
      <h2>Login</h2>
      <input onChange={e => setName(e.target.value)} />
      <button onClick={() => login({ name })}>Login</button>
    </div>
  );
}