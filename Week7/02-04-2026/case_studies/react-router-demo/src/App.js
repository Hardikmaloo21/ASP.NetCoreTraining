import {BrowserRouter , Routes , Route , NavLink} from "react-router-dom";
import Home from "./components/HomePage";
import About from "./components/About";
import Contact from "./components/Contact";

function App() {
  return (
    <BrowserRouter>
      <nav style={styles.nav}>
        <NavLink to="/" style={styles.link} end>
          Home
        </NavLink>
        <NavLink to="/about" style={styles.link}>
          About
        </NavLink>
        <NavLink to="/contact" style={styles.link}>
          Contact
        </NavLink>
      </nav>

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/about" element={<About />} />
        <Route path="/contact" element={<Contact />} />
      </Routes>
    </BrowserRouter>
  );
}

const isActive = true; 

const styles = {
  nav: {
    background: "#333",
    gap: '20px',
    padding: "10px",
    display: "flex",
    justifyContent: "center",
  },
  link: ({ isActive }) => ({
    color: isActive ? "#ffffff" : "#bbb",
    fontWeight: isActive ? "bold" : "normal",
    textDecoration: "none",
  }),
};

export default App;