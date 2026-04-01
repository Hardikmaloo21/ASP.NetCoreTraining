import React from "react";

function CartItem({ item, removeFromCart, updateQty }) {
  return (
    <div style={{ marginBottom: "10px" }}>
      <p>
        {item.name} x{item.qty} = ${item.price * item.qty}
      </p>

      <button onClick={() => updateQty(item.id, 1)}>+</button>
      <button onClick={() => updateQty(item.id, -1)}>-</button>
      <button onClick={() => removeFromCart(item.id)}>Remove</button>
    </div>
  );
}

export default CartItem;