import { Outlet, Link } from "react-router-dom";

export default function MainLayout() {
  return (
    <div>
      <header className="header">E-Commerce App</header>

      <div className="container">
        <aside className="sidebar">
          <Link to="/">Home</Link>
          <Link to="/about">About</Link>
          <Link to="/contact">Contact</Link>
          <Link to="/products">Products</Link>
          <Link to="/login">Login</Link>
          <Link to="/register">Register</Link>
        </aside>

        <main className="content">
          <Outlet />
        </main>
      </div>

      <footer className="footer">© 2026 E-Commerce</footer>
    </div>
  );
}