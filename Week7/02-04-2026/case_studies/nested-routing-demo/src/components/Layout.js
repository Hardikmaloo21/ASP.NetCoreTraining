import NavBar from "./NavBar";
import { Outlet } from "react-router-dom";

function Layout() {
  return (
    <>
      <Navbar />

      <div style={styles.container}>
        <Outlet />
      </div>
    </>
  );
}

const styles = {
  container: {
    padding: "40px",
    textAlign: "center",
  },
};

export default Layout;