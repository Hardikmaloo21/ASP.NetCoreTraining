import { createSlice } from '@reduxjs/toolkit';

const authSlice = createSlice({
  name: 'auth',
  initialState: {
    isAuthenticated: false,
    user: null,
    role: null,
    loginError: null,
  },
  reducers: {
    login: (state, action) => {
      // Hardcoded demo credentials
      const { username, password } = action.payload;
      if (username === 'admin' && password === 'admin123') {
        state.isAuthenticated = true;
        state.user = username;
        state.role = 'admin';
        state.loginError = null;
      } else if (username === 'viewer' && password === 'viewer123') {
        state.isAuthenticated = true;
        state.user = username;
        state.role = 'viewer';
        state.loginError = null;
      } else {
        state.loginError = 'Invalid username or password.';
      }
    },
    logout: (state) => {
      state.isAuthenticated = false;
      state.user = null;
      state.role = null;
      state.loginError = null;
    },
    clearLoginError: (state) => {
      state.loginError = null;
    },
  },
});

export const { login, logout, clearLoginError } = authSlice.actions;
export default authSlice.reducer;