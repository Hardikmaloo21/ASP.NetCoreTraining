import { useState } from "react";
import { useEmployees } from "../context/EmployeeContext";

export default function EmployeeForm() {
  const { add } = useEmployees();
  const [name, setName] = useState("");

  return (
    <div>
      <input value={name} onChange={e => setName(e.target.value)} />
      <button onClick={() => add({ id: Date.now(), name })}>
        Add
      </button>
    </div>
  );
}