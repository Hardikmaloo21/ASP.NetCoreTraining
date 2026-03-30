import React from "react";
import CartItem from "./CartItem";

function Cart({ cart, removeFromCart, updateQty }) {

  const total = cart.reduce(
    (acc, item) => acc + item.price * item.qty,
    0
  );

  return (
    <div>
      <h2>Cart</h2>

      {cart.length === 0 ? (
        <p>Cart is empty</p>
      ) : (
        cart.map((item) => (
          <CartItem
            key={item.id}
            item={item}
            removeFromCart={removeFromCart}
            updateQty={updateQty}
          />
        ))
      )}

      <h3>Total: ${total}</h3>
    </div>
  );
}

export default Cart;