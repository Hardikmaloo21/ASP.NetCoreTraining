import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { EmployeeContext } from "../context/EmployeeContext";

const EmployeeDashboard = () => {
  const { user } = useContext(AuthContext);
  const { employees } = useContext(EmployeeContext);

  // FIX: Match using ID
  const myData = employees.find(emp => emp.id === user.id);

  return (
    <div style={{ padding: "20px" }}>
      <h2>My Profile</h2>
      {myData ? (
        <div style={{
          padding: "20px",
          background: "#1e293b",
          borderRadius: "10px",
          color: "white",
          width: "300px"
        }}>
          <h3>{myData.name}</h3>
          <p>{myData.role}</p>
        </div>
      ) : (
        <p>No data found</p>
      )}
    </div>
  );
};

export default EmployeeDashboard;