import { useContext, useState } from "react";
import { EmployeeContext } from "../context/EmployeeContext";
import EmployeeForm from "../components/EmployeeForm";

const AdminDashboard = () => {
  const { employees, addEmployee, deleteEmployee, updateEmployee } = useContext(EmployeeContext);
  const [editingEmployee, setEditingEmployee] = useState(null);

  return (
    <div style={{ padding: "20px" }}>
      <h2>Admin Dashboard</h2>

      <EmployeeForm
        addEmployee={addEmployee}
        updateEmployee={updateEmployee}
        editingEmployee={editingEmployee}
        setEditingEmployee={setEditingEmployee}
      />

      {employees.map(emp => (
        <div key={emp.id} style={styles.card}>
          <span>{emp.name} - {emp.role}</span>
          <div>
            <button onClick={() => setEditingEmployee(emp)}>Edit</button>
            <button onClick={() => deleteEmployee(emp.id)}>Delete</button>
          </div>
        </div>
      ))}
    </div>
  );
};

const styles = {
  card: {
    display: "flex",
    justifyContent: "space-between",
    background: "#0f172a",
    padding: "10px",
    marginTop: "10px",
    borderRadius: "8px",
    color: "white"
  }
};

export default AdminDashboard;