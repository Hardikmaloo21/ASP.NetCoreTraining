import React from "react";
import ProductItem from "./ProductItem";

function ProductList({ products, addToCart }) {
  return (
    <div>
      <h2>Products</h2>
      {products.map((product) => (
        <ProductItem key={product.id} product={product} addToCart={addToCart} />
      ))}
    </div>
  );
}

export default ProductList;