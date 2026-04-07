import { useEmployees } from "../context/EmployeeContext";
import { useState } from "react";

export default function Dashboard() {
  const { employees, add, remove } = useEmployees();

  const [name, setName] = useState("");
  const [role, setRole] = useState("");

  const handleAdd = () => {
    if (!name || !role) return;

    add({
      id: Date.now(),
      name,
      role,
    });

    setName("");
    setRole("");
  };

  return (
    <>
      {/* STATS */}
      <div className="card">
        <h2>Dashboard</h2>
        <p>Total Employees: {employees.length}</p>
      </div>

      {/* ADD FORM */}
      <div className="card">
        <h3>Add Employee</h3>

        <div className="form-row">
          <input
            placeholder="Employee Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          <input
            placeholder="Role"
            value={role}
            onChange={(e) => setRole(e.target.value)}
          />

          <button onClick={handleAdd}>Add</button>
        </div>
      </div>

      {/* TABLE */}
      <div className="card">
        <h3>Employee List</h3>

        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Role</th>
              <th>Action</th>
            </tr>
          </thead>

          <tbody>
            {employees.map((emp) => (
              <tr key={emp.id}>
                <td>{emp.name}</td>
                <td>{emp.role}</td>
                <td>
                  <button onClick={() => remove(emp.id)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}