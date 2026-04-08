import React , {useState} from "react";
import EmployeeList from "./features/employees/employeeList";
import EmployeeAdd from "./features/employees/employeeAdd";
import EmployeeEdit from "./features/employees/employeeEdit";

function App() {
    const [selectedEmployee, setSelectedEmployee] = React.useState(null);
    const handleEdit = (employee) => {
        setSelectedEmployee(employee);
    };
    const handleCloseEdit = () => {
        setSelectedEmployee(null);
    };
    return (
        <div>
            <h1>Employee Management</h1>
            <EmployeeAdd />
            <EmployeeList onEdit={handleEdit} />
            {selectedEmployee && (
                <EmployeeEdit 
                selectedEmployee={selectedEmployee} 
                clearEdit={() => setSelectedEmployee(null)} />
            )}
        </div>
    );
}

export default App;