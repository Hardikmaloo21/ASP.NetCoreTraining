import React from "react";

function About() {
  return (
    <div style={styles.container}>
        <h1>About Page</h1>
        <p>This application demonstrates React Router concepts.</p>
        <p>It includes navigation, routing, and component rendering.</p>
    </div>
  );
}

const styles = {
  container: {
    background: "#fff3cd",
    padding: "40px",
    textAlign: "center",
  },
};

export default About;