import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { login, clearLoginError } from '../features/auth/authSlice';

export default function LoginForm() {
  const dispatch = useDispatch();
  const loginError = useSelector(s => s.auth.loginError);
  const [form, setForm] = useState({ username: '', password: '' });
  const [show, setShow] = useState(false);

  const handle = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    if (loginError) dispatch(clearLoginError());
  };

  const submit = (e) => {
    e.preventDefault();
    dispatch(login(form));
  };

  return (
    <div style={styles.page}>
      <div style={styles.bg} />
      <div style={styles.card}>
        {/* Logo */}
        <div style={styles.logoWrap}>
          <div style={styles.logoBox}>
            <svg width="28" height="28" viewBox="0 0 28 28" fill="none">
              <rect width="12" height="12" x="2" y="2" rx="3" fill="#3b82f6"/>
              <rect width="12" height="12" x="14" y="2" rx="3" fill="#06b6d4" opacity="0.7"/>
              <rect width="12" height="12" x="2" y="14" rx="3" fill="#06b6d4" opacity="0.7"/>
              <rect width="12" height="12" x="14" y="14" rx="3" fill="#3b82f6"/>
            </svg>
          </div>
          <span style={styles.brand}>EmpDesk</span>
        </div>

        <h1 style={styles.title}>Welcome back</h1>
        <p style={styles.sub}>Sign in to your dashboard</p>

        {/* Demo hint */}
        <div style={styles.hint}>
          <span style={{fontSize:11,color:'#94a3b8'}}>
            Demo — <b style={{color:'#3b82f6'}}>admin / admin123</b> &nbsp;|&nbsp; <b style={{color:'#06b6d4'}}>viewer / viewer123</b>
          </span>
        </div>

        <form onSubmit={submit} style={{width:'100%'}}>
          <label style={styles.label}>Username</label>
          <div style={styles.inputWrap}>
            <span style={styles.icon}>👤</span>
            <input name="username" value={form.username} onChange={handle}
              placeholder="Enter username" style={styles.input} autoComplete="off" />
          </div>

          <label style={{...styles.label, marginTop:16}}>Password</label>
          <div style={styles.inputWrap}>
            <span style={styles.icon}>🔒</span>
            <input name="password" type={show ? 'text' : 'password'}
              value={form.password} onChange={handle}
              placeholder="Enter password" style={styles.input} />
            <button type="button" onClick={() => setShow(!show)} style={styles.eye}>
              {show ? '🙈' : '👁️'}
            </button>
          </div>

          {loginError && (
            <div style={styles.error}>⚠️ {loginError}</div>
          )}

          <button type="submit" style={styles.btn}>
            Sign In →
          </button>
        </form>
      </div>
    </div>
  );
}

const styles = {
  page: {
    minHeight: '100vh', display: 'flex', alignItems: 'center',
    justifyContent: 'center', position: 'relative', overflow: 'hidden',
    background: '#0a0e1a',
  },
  bg: {
    position: 'absolute', inset: 0,
    background: 'radial-gradient(ellipse 80% 60% at 50% -20%, #3b82f630 0%, transparent 70%)',
    pointerEvents: 'none',
  },
  card: {
    width: 420, background: '#111827',
    border: '1px solid #1e2d45', borderRadius: 20,
    padding: '40px 40px 36px', display: 'flex',
    flexDirection: 'column', alignItems: 'center',
    boxShadow: '0 24px 80px rgba(0,0,0,0.5)', position: 'relative', zIndex: 1,
  },
  logoWrap: { display: 'flex', alignItems: 'center', gap: 10, marginBottom: 28 },
  logoBox: {
    width: 48, height: 48, background: '#0a0e1a',
    border: '1px solid #1e2d45', borderRadius: 12,
    display: 'flex', alignItems: 'center', justifyContent: 'center',
  },
  brand: { fontFamily: "'Syne',sans-serif", fontSize: 22, fontWeight: 800, color: '#f1f5f9' },
  title: { fontFamily: "'Syne',sans-serif", fontSize: 26, fontWeight: 700, color: '#f1f5f9', marginBottom: 6 },
  sub: { fontSize: 14, color: '#94a3b8', marginBottom: 20 },
  hint: {
    width: '100%', background: '#0a0e1a', border: '1px dashed #1e2d45',
    borderRadius: 8, padding: '8px 12px', marginBottom: 24, textAlign: 'center',
  },
  label: { display: 'block', fontSize: 12, fontWeight: 600, color: '#94a3b8', letterSpacing: 1, textTransform: 'uppercase', marginBottom: 8, width: '100%' },
  inputWrap: { width: '100%', position: 'relative', display: 'flex', alignItems: 'center' },
  icon: { position: 'absolute', left: 14, fontSize: 14, pointerEvents: 'none' },
  input: {
    width: '100%', background: '#0a0e1a', border: '1px solid #1e2d45',
    borderRadius: 10, padding: '12px 14px 12px 38px', color: '#f1f5f9',
    fontSize: 14, outline: 'none', fontFamily: "'DM Sans',sans-serif",
    transition: 'border .2s',
  },
  eye: { position: 'absolute', right: 12, background: 'none', border: 'none', cursor: 'pointer', fontSize: 14 },
  error: { marginTop: 14, background: '#ef444420', border: '1px solid #ef4444', borderRadius: 8, padding: '10px 14px', fontSize: 13, color: '#ef4444', width: '100%' },
  btn: {
    marginTop: 24, width: '100%', padding: '14px', background: 'linear-gradient(135deg,#3b82f6,#06b6d4)',
    border: 'none', borderRadius: 10, color: '#fff', fontSize: 15, fontWeight: 700,
    cursor: 'pointer', fontFamily: "'Syne',sans-serif", letterSpacing: 0.5,
    boxShadow: '0 4px 20px #3b82f640', transition: 'opacity .2s',
  },
};