import { createSlice } from '@reduxjs/toolkit';

const employeeSlice = createSlice({
  name: 'employees',
  initialState: {
    list: [
      { id: 1, name: 'Priya Sharma',   dept: 'Engineering', role: 'Senior Dev',   salary: 95000, status: 'Active',   avatar: 'PS' },
      { id: 2, name: 'Rahul Mehta',    dept: 'Design',      role: 'UI/UX Lead',   salary: 82000, status: 'Active',   avatar: 'RM' },
      { id: 3, name: 'Ananya Singh',   dept: 'HR',          role: 'HR Manager',   salary: 70000, status: 'Active',   avatar: 'AS' },
      { id: 4, name: 'Vikram Nair',    dept: 'Finance',     role: 'CFO',          salary: 120000,status: 'On Leave', avatar: 'VN' },
      { id: 5, name: 'Deepika Rao',    dept: 'Engineering', role: 'DevOps Eng',   salary: 88000, status: 'Active',   avatar: 'DR' },
    ],
    selectedEmployee: null,
  },
  reducers: {
    addEmployee: (state, action) => {
      state.list.push({ id: Date.now(), ...action.payload });
    },
    editEmployee: (state, action) => {
      const idx = state.list.findIndex(e => e.id === action.payload.id);
      if (idx !== -1) state.list[idx] = { ...state.list[idx], ...action.payload };
    },
    deleteEmployee: (state, action) => {
      state.list = state.list.filter(e => e.id !== action.payload);
    },
    selectEmployee: (state, action) => {
      state.selectedEmployee = action.payload;
    },
    clearSelected: (state) => {
      state.selectedEmployee = null;
    },
  },
});

export const { addEmployee, editEmployee, deleteEmployee, selectEmployee, clearSelected } = employeeSlice.actions;
export default employeeSlice.reducer;