import {configureStore} from '@reduxjs/toolkit';
import counterReducer from '../features_counter/counterSlice';

export const store = configureStore({
    reducer: {
        counter: counterReducer,
    },
});