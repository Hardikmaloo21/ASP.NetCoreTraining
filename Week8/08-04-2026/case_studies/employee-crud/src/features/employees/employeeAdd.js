import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { addEmployee } from "./employeeSlice";

function EmployeeAdd() {
    const dispatch = useDispatch();

    const [name, setName] = useState("");
    const [position, setPosition] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!name || !position) return;

        dispatch(
            addEmployee({
                id: Date.now(),
                name,
                position,
            })
        );

        setName("");
        setPosition("");
    };

    return (
        <div>
            <h2>Add Employee</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />

                <input
                    type="text"
                    placeholder="Position"
                    value={position}
                    onChange={(e) => setPosition(e.target.value)}
                />

                <button type="submit">Add</button>
            </form>
        </div>
    );
}

export default EmployeeAdd;