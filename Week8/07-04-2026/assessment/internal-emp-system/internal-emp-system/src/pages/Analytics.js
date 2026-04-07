import { useEmployees } from "../context/EmployeeContext";

export default function Analytics() {
  const { employees } = useEmployees();

  return (
    <div className="card">
      <h2>Analytics</h2>
      <p>Total Employees: {employees.length}</p>
    </div>
  );
}