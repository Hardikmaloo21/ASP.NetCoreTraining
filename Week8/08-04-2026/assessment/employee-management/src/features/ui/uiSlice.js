import { createSlice } from '@reduxjs/toolkit';

const uiSlice = createSlice({
  name: 'ui',
  initialState: {
    theme: 'dark',
    isLoading: false,
    notification: null,    // { type: 'success'|'error'|'info', message: '' }
    activeTab: 'dashboard',
  },
  reducers: {
    toggleTheme: (state) => {
      state.theme = state.theme === 'dark' ? 'light' : 'dark';
    },
    setLoading: (state, action) => {
      state.isLoading = action.payload;
    },
    setNotification: (state, action) => {
      state.notification = action.payload;
    },
    clearNotification: (state) => {
      state.notification = null;
    },
    setActiveTab: (state, action) => {
      state.activeTab = action.payload;
    },
  },
});

export const { toggleTheme, setLoading, setNotification, clearNotification, setActiveTab } = uiSlice.actions;
export default uiSlice.reducer;