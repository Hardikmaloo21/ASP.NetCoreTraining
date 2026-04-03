import { Link } from "react-router-dom";
import LanguageSwitcher from "./LanguageSwitcher";

function Navbar() {
  return (
    <div className="navbar">
      <h2> Language App</h2>

      <div className="nav-links">
        <Link to="/">Home</Link>
        <Link to="/about">About</Link>
      </div>

      <LanguageSwitcher />
    </div>
  );
}

export default Navbar;