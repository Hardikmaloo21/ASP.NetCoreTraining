import { Link } from "react-router-dom";
export default function Register() {
  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h2>Register Page</h2>
        <p>Registration UI here</p>
        <p>
          Already have an account? <Link to="/login">Login</Link>
        </p>
      </div>
    </div>
  );
}


const styles = {
  container: {
    height: "100vh",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    background: "#f0f2f5",
  },
  card: {
    padding: "30px",
    background: "white",
    borderRadius: "10px",
  },
};