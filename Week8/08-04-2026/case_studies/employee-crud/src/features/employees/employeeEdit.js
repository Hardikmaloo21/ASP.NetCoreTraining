import React, { useState, useEffect } from "react";
import { useDispatch } from "react-redux";
import { updateEmployee } from "./employeeSlice";

const EmployeeEdit = ({ selectedEmployee, clearEdit }) => {
    const dispatch = useDispatch();

    const [name, setName] = useState("");
    const [position, setPosition] = useState("");

    useEffect(() => {
        if (selectedEmployee) {
            setName(selectedEmployee.name || "");
            setPosition(selectedEmployee.position || "");
        }
    }, [selectedEmployee]);

    if (!selectedEmployee) return null;

    const handleSubmit = (e) => {
        e.preventDefault();

        dispatch(
            updateEmployee({
                id: selectedEmployee.id,
                name,
                position,
            })
        );

        clearEdit();
    };

    return (
        <div>
            <h2>Edit Employee</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />

                <input
                    type="text"
                    value={position}
                    onChange={(e) => setPosition(e.target.value)}
                />

                <button type="submit">Update</button>
                <button type="button" onClick={clearEdit}>
                    Cancel
                </button>
            </form>
        </div>
    );
};

export default EmployeeEdit;