import React, { useState } from "react";
import ProductList from "./components/ProductList";
import Cart from "./components/Cart";

function App() {
  const [cart, setCart] = useState([]);

  const products = [
    { id: 1, name: "React T-Shirt", price: 25 },
    { id: 2, name: "JS Hoodie", price: 40 },
    { id: 3, name: "Sneakers", price: 60 }
  ];

  // Add to cart
  const addToCart = (product) => {
    const exist = cart.find((item) => item.id === product.id);

    if (exist) {
      setCart(
        cart.map((item) =>
          item.id === product.id
            ? { ...item, qty: item.qty + 1 }
            : item
        )
      );
    } else {
      setCart([...cart, { ...product, qty: 1 }]);
    }
  };

  // Remove item
  const removeFromCart = (id) => {
    setCart(cart.filter((item) => item.id !== id));
  };

  // Update quantity
  const updateQty = (id, change) => {
    setCart(
      cart.map((item) =>
        item.id === id
          ? { ...item, qty: item.qty + change }
          : item
      ).filter(item => item.qty > 0)
    );
  };

  return (
    <div style={{ display: "flex", justifyContent: "space-around", padding: "20px" }}>
      <ProductList products={products} addToCart={addToCart} />
      <Cart cart={cart} removeFromCart={removeFromCart} updateQty={updateQty} />
    </div>
  );
}

export default App;