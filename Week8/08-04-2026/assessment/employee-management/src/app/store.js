import { configureStore }           from '@reduxjs/toolkit';
import { persistStore, persistReducer } from 'redux-persist';
import storage                      from 'redux-persist/lib/storage';
import { combineReducers }          from 'redux';

import authReducer     from '../features/auth/authSlice';
import employeeReducer from '../features/employees/employeeSlice';
import uiReducer       from '../features/ui/uiSlice';
import logger          from '../middleware/logger';

const persistConfig = {
  key: 'emp-mgmt',
  storage,
  whitelist: ['auth', 'employees'],
};

const rootReducer = combineReducers({
  auth:      authReducer,
  employees: employeeReducer,
  ui:        uiReducer,
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ serializableCheck: false }).concat(logger),
});

export const persistor = persistStore(store);