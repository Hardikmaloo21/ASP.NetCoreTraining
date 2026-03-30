import React, { useState } from "react";

function ThemeToggle() {
  const [darkMode, setDarkMode] = useState(false);

  const toggleTheme = () => {
    setDarkMode(!darkMode);
  };

  const containerStyle = {
    height: "100vh",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: darkMode ? "#222" : "#ffffff",
    color: darkMode ? "#ffffff" : "#000000",
    transition: "0.3s"
  };

  const buttonStyle = {
    padding: "10px 20px",
    fontSize: "16px",
    cursor: "pointer",
    border: "none",
    borderRadius: "5px",
    backgroundColor: darkMode ? "#ffffff" : "#222",
    color: darkMode ? "#000" : "#ffffff"
  };

  return (
    <div style={containerStyle}>
      <h1>Mode: {darkMode ? "Dark" : "Light"}</h1>

      <button onClick={toggleTheme} style={buttonStyle}>
        Switch to {darkMode ? "Light" : "Dark"} Mode
      </button>
    </div>
  );
}

export default ThemeToggle;