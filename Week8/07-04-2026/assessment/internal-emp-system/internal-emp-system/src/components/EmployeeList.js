import { useEmployees } from "../context/EmployeeContext";

export default function EmployeeList() {
  const { employees, remove } = useEmployees();

  return (
    <ul>
      {employees.map(emp => (
        <li key={emp.id}>
          {emp.name}
          <button onClick={() => remove(emp.id)}>Delete</button>
        </li>
      ))}
    </ul>
  );
}