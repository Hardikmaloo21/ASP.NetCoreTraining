
import { useState, useEffect } from "react";

const EmployeeForm = ({ addEmployee, updateEmployee, editingEmployee, setEditingEmployee }) => {
  const [formData, setFormData] = useState({ name: "", role: "" });

  // Prefills form when editing
  useEffect(() => {
    if (editingEmployee) setFormData(editingEmployee);
  }, [editingEmployee]);

  // Handles input changes
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  // Handles form submission
  const handleSubmit = () => {
    if (!formData.name || !formData.role) return;

    if (editingEmployee) {
      updateEmployee(formData);
      setEditingEmployee(null);
    } else {
      addEmployee(formData);
    }

    setFormData({ name: "", role: "" });
  };

  return (
    <div style={styles.card}>
      <input name="name" placeholder="Name" value={formData.name} onChange={handleChange} />
      <input name="role" placeholder="Role" value={formData.role} onChange={handleChange} />
      <button onClick={handleSubmit}>{editingEmployee ? "Update" : "Add"}</button>
    </div>
  );
};

const styles = {
  card: {
    display: "flex",
    gap: "10px",
    padding: "15px",
    background: "#1e293b",
    borderRadius: "10px"
  }
};

export default EmployeeForm;