import { NavLink } from "react-router-dom";
function NavBar() {
  return (
    <nav style={styles.nav}>
        <h2 style={styles.logo}>
            MyApp
        </h2>
        <div>
                
        <NavLink to="/" style={styles.link} end>
            Home
        </NavLink>
        <NavLink to="/about" style={styles.link}>
            About
        </NavLink>
        <NavLink to="/contact" style={styles.link}>
            Contact
        </NavLink>
        </div>
    </nav>
  );
}

const styles = {
    nav: {
        background: "#1e293b",
        gap: '20px',
        padding: "15px 40px",
        display: "flex",
        justifyContent: "space-between",
    },
    link: ({ isActive }) => ({
        color: isActive ? "#ffffff" : "#bbb",
        fontWeight: isActive ? "bold" : "normal",
        textDecoration: "none",
    })
};

export default NavBar