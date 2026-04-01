// import React , { useState } from "react";

// function App() {
//   const [count, setCount] = useState(0);
//   return (
//     <div style={styles.container}>
//       <h1>Counter app</h1>
//       <button style={styles.btn} onClick={() => setCount(count + 1)}>Increment</button>
//       <button style={styles.btn} onClick={() => setCount(count - 1)}>Decrement</button>
//     </div>
//   );
// }

// const styles = {
//   container: {
//     textAlign: "center",
//     marginTop: "50px",
//   },
//   btn: {
//     margin: "10px",
//     padding: "10px 20px",
//     fontSize: "16px",
//   },
// };

// export default App;

// import React , {useState} from "react";

// function App(){
//   const [count, setCount] = useState(0);

//   const increment = () => {
//     setCount(prev => prev + 1);
//   };

//   const incrementByTwo = () => {
//     setCount(prev => prev + 2);
//   };

//   const resetCount = () => {
//     setCount(0);
//   }
  
//   return (
//     <div style={styles.contanier}>
//       <h1>Funcional update Demo</h1>
//       <h2>{count}</h2>
//       <button style={styles.btn} onClick={increment}>Increment</button>
//       <button style={styles.btn} onClick={incrementByTwo}>Increment by 2</button>
//       <button style={styles.btn} onClick={resetCount}>Reset</button>
//     </div>
//   );
  
// };

// const styles ={
//   contanier: {
//     textAlign: "center",
//     marginTop: "50px"
//   },
//   btn: {
//     margin: "10px",
//     padding: "10px 20px",
//     fontSize: "16px"
//   }
// };

// export default App;

// import React, { useState } from "react";

// function App() {
//   const [data, setData] = useState(0);

//   console.log("Expensive computation running...");

//   let result = 0;
//   for (let i = 0; i < 1000000; i++) {
//     result += i;
//   }

//   const recalculateData = () => {
//     setData(prev => {
//       console.log("Recalculating data...");
//       return prev + 100;
//     });
//   };

//   return (
//     <div style={styles.container}>
//       <h1>Lazy Initialization Demo</h1>

//       <h2>Computed Value: {data}</h2>
//       <h3>Result: {result % 1000}</h3>

//       <button style={styles.btn} onClick={recalculateData}>
//         Recalculate (+100)
//       </button>
//     </div>
//   );
// }

// const styles = {
//   container: {   // fixed typo
//     textAlign: "center",
//     marginTop: "50px"
//   },
//   btn: {
//     margin: "10px",
//     padding: "10px 20px",
//     fontSize: "16px"
//   }
// };

// export default App;

// import React, { useState } from "react";

// function App() {

//   const [items, setItems] = useState([]);

//   // Add Single Item
//   const addItem = () => {
//     const newItem = {
//       id: Date.now(),
//       name: "Item " + (items.length + 1),
//       created: new Date().toLocaleTimeString()
//     };

//     setItems(prev => [...prev, newItem]);
//   };

//   // Add Multiple Items
//   const addMultipleItems = () => {
//     const newItems = [
//       { id: Date.now(), name: "Batch Item 1", created: new Date().toLocaleTimeString() },
//       { id: Date.now() + 1, name: "Batch Item 2", created: new Date().toLocaleTimeString() },
//       { id: Date.now() + 2, name: "Batch Item 3", created: new Date().toLocaleTimeString() }
//     ];

//     setItems(prev => [...prev, ...newItems]);
//   };

//   // Update Item
//   const updateItem = (id) => {
//     setItems(prev =>
//       prev.map(item =>
//         item.id === id
//           ? { ...item, name: "Updated Item", updated: new Date().toLocaleTimeString() }
//           : item
//       )
//     );
//   };

//   // Delete Item
//   const deleteItem = (id) => {
//     setItems(prev => prev.filter(item => item.id !== id));
//   };

//   // Delete All
//   const deleteAllItems = () => {
//     setItems([]);
//   };

//   return (
//     <div style={styles.container}>   {/* ✅ FIXED: single parent */}

//       <h2>Items List</h2>

//       <button style={styles.btn} onClick={addItem}>Add Item</button>
//       <button style={styles.btn} onClick={addMultipleItems}>Add Multiple</button>
//       <button style={styles.deleteAllBtn} onClick={deleteAllItems}>Delete All</button>

//       <h3>Total Items: {items.length}</h3>

//       <ul style={styles.list}>
//         {items.map(item => (
//           <li key={item.id} style={styles.card}>
//             <p><b>{item.name}</b></p>
//             <p>Created: {item.created}</p>
//             {item.updated && <p>Updated: {item.updated}</p>}

//             <button
//               style={styles.updateBtn}
//               onClick={() => updateItem(item.id)}
//             >
//               Update
//             </button>

//             <button
//               style={styles.deleteBtn}
//               onClick={() => deleteItem(item.id)}
//             >
//               Delete
//             </button>
//           </li>
//         ))}
//       </ul>

//       <p style={styles.info}>
//         ⚡ Always use <b>map, filter, spread</b> in React
//       </p>

//     </div>
//   );
// }

// // Styles
// const styles = {
//   container: {
//     padding: "20px",
//     fontFamily: "Arial"
//   },
//   btn: {
//     margin: "5px",
//     padding: "8px 12px",
//     backgroundColor: "#4CAF50",
//     color: "white",
//     border: "none",
//     cursor: "pointer"
//   },
//   deleteAllBtn: {
//     margin: "5px",
//     padding: "8px 12px",
//     backgroundColor: "red",
//     color: "white",
//     border: "none",
//     cursor: "pointer"
//   },
//   list: {
//     listStyle: "none",
//     padding: 0
//   },
//   card: {
//     border: "1px solid #ccc",
//     margin: "10px 0",
//     padding: "10px",
//     borderRadius: "5px"
//   },
//   updateBtn: {
//     marginRight: "10px",
//     backgroundColor: "orange",
//     color: "white",
//     border: "none",
//     padding: "5px 10px",
//     cursor: "pointer"
//   },
//   deleteBtn: {
//     backgroundColor: "red",
//     color: "white",
//     border: "none",
//     padding: "5px 10px",
//     cursor: "pointer"
//   },
//   info: {
//     marginTop: "20px",
//     color: "#555"
//   }
// };

// export default App;

// import React, { useReducer, useState } from "react";

// function App() {

//   // 🔹 Initial State
//   const initialCounterState = {
//     count: 0,
//     history: []
//   };

//   // 🔹 Reducer Function
//   function counterReducer(state, action) {
//     switch (action.type) {
//       case "increment":
//         return {
//           count: state.count + 1,
//           history: [
//             ...state.history,
//             { type: "increment", value: state.count + 1, time: new Date().toLocaleTimeString() }
//           ]
//         };

//       case "decrement":
//         return {
//           count: state.count - 1,
//           history: [
//             ...state.history,
//             { type: "decrement", value: state.count - 1, time: new Date().toLocaleTimeString() }
//           ]
//         };

//       case "reset":
//         return {
//           count: 0,
//           history: [
//             ...state.history,
//             { type: "reset", value: 0, time: new Date().toLocaleTimeString() }
//           ]
//         };

//       case "set":
//         return {
//           count: action.payload,
//           history: [
//             ...state.history,
//             { type: "set", value: action.payload, time: new Date().toLocaleTimeString() }
//           ]
//         };

//       default:
//         return state;
//     }
//   }

//   // 🔹 useReducer Hook
//   const [counterState, dispatch] = useReducer(counterReducer, initialCounterState);

//   // 🔹 Input State for SET
//   const [inputValue, setInputValue] = useState("");

//   return (
//     <div style={styles.container}>
//       <h1>useReducer Counter (Advanced)</h1>

//       <h2>Count: {counterState.count}</h2>

//       {/* 🔹 Actions */}
//       <div>
//         <button style={styles.btn} onClick={() => dispatch({ type: "increment" })}>
//           +1
//         </button>

//         <button style={styles.btn} onClick={() => dispatch({ type: "decrement" })}>
//           -1
//         </button>

//         <button style={styles.resetBtn} onClick={() => dispatch({ type: "reset" })}>
//           Reset
//         </button>
//       </div>

//       {/* 🔹 Set Value */}
//       <div style={{ marginTop: "20px" }}>
//         <input
//           type="number"
//           placeholder="Enter value"
//           value={inputValue}
//           onChange={(e) => setInputValue(e.target.value)}
//           style={styles.input}
//         />

//         <button
//           style={styles.btn}
//           onClick={() =>
//             dispatch({ type: "set", payload: Number(inputValue) })
//           }
//         >
//           Set Value
//         </button>
//       </div>

//       {/* 🔹 History */}
//       <h3 style={{ marginTop: "30px" }}>History</h3>

//       <ul style={styles.list}>
//         {counterState.history.map((item, index) => (
//           <li key={index} style={styles.card}>
//             <b>{item.type.toUpperCase()}</b> → {item.value}
//             <br />
//             <small>{item.time}</small>
//           </li>
//         ))}
//       </ul>

//       <p style={styles.info}>
//         👉 useReducer is best for <b>complex state logic & history tracking</b>
//       </p>
//     </div>
//   );
// }

// // 🎨 Styling
// const styles = {
//   container: {
//     textAlign: "center",
//     marginTop: "40px",
//     fontFamily: "Arial"
//   },
//   btn: {
//     margin: "10px",
//     padding: "10px 15px",
//     cursor: "pointer"
//   },
//   resetBtn: {
//     margin: "10px",
//     padding: "10px 15px",
//     backgroundColor: "red",
//     color: "white",
//     border: "none",
//     cursor: "pointer"
//   },
//   input: {
//     padding: "10px",
//     marginRight: "10px"
//   },
//   list: {
//     listStyle: "none",
//     padding: 0
//   },
//   card: {
//     border: "1px solid #ccc",
//     margin: "10px auto",
//     padding: "10px",
//     width: "250px"
//   },
//   info: {
//     marginTop: "20px",
//     color: "green"
//   }
// };

// export default App;

