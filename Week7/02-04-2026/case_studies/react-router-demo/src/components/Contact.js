import React from "react";

function Contact() {
  return (
    <div style={styles.container}>
      <h1>Contact Page</h1>
      <p>you can reach at us.</p>
      <p>Email: support@example.com</p>
      <p>phone : +91 98765 43210</p>
    </div>
  );
}

const styles = {
  container: {
    background: "#d4edda",
    padding: "40px",
    textAlign: "center",
  },
};

export default Contact;