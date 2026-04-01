import logo from './logo.svg';
import './App.css';
//import Counter from "./components/Counter";
// import StateVsPropsDemo from './components/StateVsPropsDemo';
import TemperatureConverter from './components/TemperatureConverter';
import React, { useState } from 'react';

// function App() {
//   return (
//     <div className="App">
//       <Counter />
//     </div>
//   );
// }

// function App() {
//   return (
//     <div className="App">
//       <StateVsPropsDemo />
//     </div>
//   );
// }
function App() {
  return (
    <div className="App">
      <TemperatureConverter />
    </div>
  );
}

export default App;