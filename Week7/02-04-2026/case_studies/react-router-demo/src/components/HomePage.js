import React from "react";

function Home(){
    return(
        <div style={styles.container}>
            <h1>Home Page</h1>
            <p>Welcome to our React Router demo application!</p>
            <p>This is the Homepage where users land first.</p>
        </div>
    )
}

const styles = {
    container: {
        background: "#f0f8ff",   
        padding: "40px",
        textAlign: "center",    
    },
};

export default Home