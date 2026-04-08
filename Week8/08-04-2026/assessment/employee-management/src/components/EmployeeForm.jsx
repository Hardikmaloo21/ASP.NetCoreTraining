import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { addEmployee, editEmployee, clearSelected } from '../features/employees/employeeSlice';
import { setNotification } from '../features/ui/uiSlice';

const DEPTS = ['Engineering','Design','HR','Finance','Marketing','Operations'];
const STATUSES = ['Active','On Leave','Inactive'];

const empty = { name:'', dept:'Engineering', role:'', salary:'', status:'Active', avatar:'' };

export default function EmployeeForm({ onClose }) {
  const dispatch  = useDispatch();
  const selected  = useSelector(s => s.employees.selectedEmployee);
  const [form, setForm] = useState(empty);
  const [errors, setErrors] = useState({});
  const isEdit = !!selected;

  useEffect(() => {
    if (selected) setForm({ ...selected, salary: String(selected.salary) });
    else setForm(empty);
  }, [selected]);

  const handle = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    setErrors({ ...errors, [e.target.name]: '' });
  };

  const validate = () => {
    const e = {};
    if (!form.name.trim())   e.name = 'Name is required';
    if (!form.role.trim())   e.role = 'Role is required';
    if (!form.salary || isNaN(form.salary) || Number(form.salary) <= 0)
      e.salary = 'Valid salary required';
    return e;
  };

  const submit = (e) => {
    e.preventDefault();
    const e2 = validate();
    if (Object.keys(e2).length) { setErrors(e2); return; }
    const initials = form.name.split(' ').map(n=>n[0]).join('').slice(0,2).toUpperCase();
    const payload = { ...form, salary: Number(form.salary), avatar: initials };

    if (isEdit) {
      dispatch(editEmployee(payload));
      dispatch(setNotification({ type: 'success', message: `${form.name} updated successfully.` }));
    } else {
      dispatch(addEmployee(payload));
      dispatch(setNotification({ type: 'success', message: `${form.name} added successfully.` }));
    }
    dispatch(clearSelected());
    onClose();
  };

  const cancel = () => { dispatch(clearSelected()); onClose(); };

  return (
    <div style={s.overlay}>
      <div style={s.modal}>
        <div style={s.header}>
          <div>
            <h2 style={s.title}>{isEdit ? '✏️ Edit Employee' : '➕ Add Employee'}</h2>
            <p style={s.sub}>{isEdit ? 'Update employee information' : 'Fill in the details below'}</p>
          </div>
          <button onClick={cancel} style={s.close}>✕</button>
        </div>

        <form onSubmit={submit} style={s.form}>
          <div style={s.grid}>
            {/* Full Name */}
            <div style={s.field}>
              <label style={s.label}>Full Name *</label>
              <input name="name" value={form.name} onChange={handle}
                placeholder="e.g. Priya Sharma" style={{...s.input, ...(errors.name ? s.inputErr : {})}} />
              {errors.name && <span style={s.err}>{errors.name}</span>}
            </div>

            {/* Role */}
            <div style={s.field}>
              <label style={s.label}>Job Role *</label>
              <input name="role" value={form.role} onChange={handle}
                placeholder="e.g. Senior Developer" style={{...s.input, ...(errors.role ? s.inputErr : {})}} />
              {errors.role && <span style={s.err}>{errors.role}</span>}
            </div>

            {/* Department */}
            <div style={s.field}>
              <label style={s.label}>Department</label>
              <select name="dept" value={form.dept} onChange={handle} style={s.select}>
                {DEPTS.map(d => <option key={d} value={d}>{d}</option>)}
              </select>
            </div>

            {/* Salary */}
            <div style={s.field}>
              <label style={s.label}>Salary (₹) *</label>
              <input name="salary" value={form.salary} onChange={handle}
                placeholder="e.g. 75000" type="number" style={{...s.input, ...(errors.salary ? s.inputErr : {})}} />
              {errors.salary && <span style={s.err}>{errors.salary}</span>}
            </div>

            {/* Status */}
            <div style={{...s.field, gridColumn:'1/-1'}}>
              <label style={s.label}>Status</label>
              <div style={s.radioGroup}>
                {STATUSES.map(st => (
                  <label key={st} style={{...s.radioLabel, ...(form.status===st ? s.radioActive : {})}}>
                    <input type="radio" name="status" value={st}
                      checked={form.status===st} onChange={handle} style={{display:'none'}} />
                    {st}
                  </label>
                ))}
              </div>
            </div>
          </div>

          <div style={s.btnRow}>
            <button type="button" onClick={cancel} style={s.cancelBtn}>Cancel</button>
            <button type="submit" style={s.submitBtn}>
              {isEdit ? 'Update Employee' : 'Add Employee'} →
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

const s = {
  overlay: {
    position:'fixed', inset:0, background:'rgba(0,0,0,0.7)',
    display:'flex', alignItems:'center', justifyContent:'center', zIndex:1000,
    backdropFilter:'blur(4px)',
  },
  modal: {
    width:540, background:'#111827', border:'1px solid #1e2d45',
    borderRadius:20, padding:'32px', boxShadow:'0 32px 80px rgba(0,0,0,0.6)',
    maxHeight:'90vh', overflowY:'auto',
  },
  header: { display:'flex', justifyContent:'space-between', alignItems:'flex-start', marginBottom:28 },
  title: { fontFamily:"'Syne',sans-serif", fontSize:20, fontWeight:700, color:'#f1f5f9' },
  sub: { fontSize:13, color:'#94a3b8', marginTop:4 },
  close: { background:'#1a2235', border:'1px solid #1e2d45', color:'#94a3b8', width:32, height:32, borderRadius:8, cursor:'pointer', fontSize:14, display:'flex', alignItems:'center', justifyContent:'center' },
  form: { display:'flex', flexDirection:'column', gap:0 },
  grid: { display:'grid', gridTemplateColumns:'1fr 1fr', gap:16, marginBottom:24 },
  field: { display:'flex', flexDirection:'column', gap:6 },
  label: { fontSize:11, fontWeight:600, color:'#94a3b8', letterSpacing:1, textTransform:'uppercase' },
  input: {
    background:'#0a0e1a', border:'1px solid #1e2d45', borderRadius:10,
    padding:'11px 14px', color:'#f1f5f9', fontSize:13, outline:'none',
    fontFamily:"'DM Sans',sans-serif",
  },
  inputErr: { borderColor:'#ef4444' },
  select: {
    background:'#0a0e1a', border:'1px solid #1e2d45', borderRadius:10,
    padding:'11px 14px', color:'#f1f5f9', fontSize:13, outline:'none',
    fontFamily:"'DM Sans',sans-serif", cursor:'pointer',
  },
  err: { fontSize:11, color:'#ef4444', marginTop:2 },
  radioGroup: { display:'flex', gap:10 },
  radioLabel: {
    padding:'8px 18px', border:'1px solid #1e2d45', borderRadius:8,
    fontSize:13, color:'#94a3b8', cursor:'pointer', transition:'all .15s',
  },
  radioActive: { background:'#3b82f620', borderColor:'#3b82f6', color:'#3b82f6', fontWeight:600 },
  btnRow: { display:'flex', gap:12, justifyContent:'flex-end' },
  cancelBtn: {
    padding:'11px 24px', background:'none', border:'1px solid #1e2d45',
    borderRadius:10, color:'#94a3b8', fontSize:14, cursor:'pointer', fontFamily:"'DM Sans',sans-serif",
  },
  submitBtn: {
    padding:'11px 28px', background:'linear-gradient(135deg,#3b82f6,#06b6d4)',
    border:'none', borderRadius:10, color:'#fff', fontSize:14, fontWeight:700,
    cursor:'pointer', fontFamily:"'Syne',sans-serif",
    boxShadow:'0 4px 16px #3b82f640',
  },
};