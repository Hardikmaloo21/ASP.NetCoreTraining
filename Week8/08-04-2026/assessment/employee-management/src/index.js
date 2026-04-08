import React from 'react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import { store, persistor } from './app/store';
import App from './App';
import './index.css';

const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
  <Provider store={store}>
    <PersistGate loading={<div style={{color:'#f1f5f9',display:'flex',height:'100vh',alignItems:'center',justifyContent:'center',fontFamily:'DM Sans'}}>Loading...</div>} persistor={persistor}>
      <App />
    </PersistGate>
  </Provider>
);