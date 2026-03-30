import React from "react";

function ProductItem({ product, addToCart }) {
  return (
    <div style={{ marginBottom: "10px" }}>
      <p>{product.name} - ${product.price}</p>
      <button onClick={() => addToCart(product)}>Add</button>
    </div>
  );
}

export default ProductItem;