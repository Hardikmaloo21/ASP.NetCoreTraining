import { Outlet, Link, useParams } from "react-router-dom";

export default function ProductDetails() {
  const { productId } = useParams();

  return (
    <div>
      <h1>Product {productId}</h1>

      <nav>
        <Link to="reviews">Reviews</Link> | 
        <Link to="specs">Specs</Link>
      </nav>

      <Outlet />
    </div>
  );
}