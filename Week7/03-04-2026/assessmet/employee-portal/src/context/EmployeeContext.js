import React, { createContext, useState } from "react";

export const EmployeeContext = createContext();

export const EmployeeProvider = ({ children }) => {
  const [employees, setEmployees] = useState([
    { id: 1, name: "Admin", role: "Manager" },
    { id: 2, name: "emp", role: "Developer" }
  ]);

  // Add employee
  const addEmployee = (emp) => {
    setEmployees([...employees, { ...emp, id: Date.now() }]);
  };

  // Delete employee
  const deleteEmployee = (id) => {
    setEmployees(employees.filter((emp) => emp.id !== id));
  };

  // Update employee
  const updateEmployee = (updatedEmp) => {
    setEmployees(
      employees.map((emp) =>
        emp.id === updatedEmp.id ? updatedEmp : emp
      )
    );
  };

  return (
    <EmployeeContext.Provider
      value={{ employees, addEmployee, deleteEmployee, updateEmployee }}
    >
      {children}
    </EmployeeContext.Provider>
  );
};