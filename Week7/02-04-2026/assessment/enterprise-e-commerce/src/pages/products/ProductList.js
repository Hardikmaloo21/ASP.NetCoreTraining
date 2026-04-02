import { Link } from "react-router-dom";

export default function ProductList() {
  return (
    <div>
      <h2>Products</h2>

      <div className="product-grid">
        {[1, 2, 3].map((id) => (
          <div key={id} className="card product-card">
            <h3>Product {id}</h3>
            <Link to={`/products/${id}`}>View Details</Link>
          </div>
        ))}
      </div>
    </div>
  );
}