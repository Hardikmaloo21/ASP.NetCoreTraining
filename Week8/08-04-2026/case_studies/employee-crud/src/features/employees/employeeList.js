import React from "react";
import { useSelector, useDispatch } from "react-redux";
import { deleteEmployee } from "./employeeSlice";

function EmployeeList({ onEdit }) {
    const employees = useSelector((state) => state.employees.list);
    const dispatch = useDispatch();

    return (
        <div>
            <h2>Employee List</h2>

            {employees.map((emp) => (
                <div key={emp.id}>
                    <span>{emp.name} - {emp.position}</span>

                    <button onClick={() => onEdit(emp)}>Edit</button>

                    <button onClick={() => dispatch(deleteEmployee(emp.id))}>
                        Delete
                    </button>
                </div>
            ))}
        </div>
    );
}

export default EmployeeList;