import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { deleteEmployee, selectEmployee } from '../features/employees/employeeSlice';
import { setNotification } from '../features/ui/uiSlice';

const DEPT_COLOR = {
  Engineering: '#3b82f6', Design: '#a855f7', HR: '#10b981',
  Finance: '#f59e0b', Marketing: '#ef4444', Operations: '#06b6d4',
};

const STATUS_STYLE = {
  Active:   { bg: '#10b98120', color: '#10b981' },
  'On Leave':{ bg: '#f59e0b20', color: '#f59e0b' },
  Inactive: { bg: '#ef444420', color: '#ef4444' },
};

export default function EmployeeList({ onEdit }) {
  const dispatch  = useDispatch();
  const employees = useSelector(s => s.employees.list);
  const role      = useSelector(s => s.auth.role);
  const [search, setSearch]   = useState('');
  const [deptFilter, setDept] = useState('All');

  const depts = ['All', ...new Set(employees.map(e => e.dept))];
  const filtered = employees.filter(e =>
    (deptFilter === 'All' || e.dept === deptFilter) &&
    (e.name.toLowerCase().includes(search.toLowerCase()) ||
     e.role.toLowerCase().includes(search.toLowerCase()))
  );

  const handleDelete = (id, name) => {
    if (window.confirm(`Remove ${name} from the system?`)) {
      dispatch(deleteEmployee(id));
      dispatch(setNotification({ type: 'success', message: `${name} removed successfully.` }));
    }
  };

  const handleEdit = (emp) => {
    dispatch(selectEmployee(emp));
    onEdit();
  };

  return (
    <div style={s.wrap}>
      {/* Stats Row */}
      <div style={s.statsRow}>
        {[
          { label: 'Total', value: employees.length, color: '#3b82f6' },
          { label: 'Active', value: employees.filter(e=>e.status==='Active').length, color: '#10b981' },
          { label: 'On Leave', value: employees.filter(e=>e.status==='On Leave').length, color: '#f59e0b' },
          { label: 'Departments', value: new Set(employees.map(e=>e.dept)).size, color: '#a855f7' },
        ].map(stat => (
          <div key={stat.label} style={s.statCard}>
            <span style={{...s.statVal, color: stat.color}}>{stat.value}</span>
            <span style={s.statLabel}>{stat.label}</span>
          </div>
        ))}
      </div>

      {/* Toolbar */}
      <div style={s.toolbar}>
        <div style={s.searchWrap}>
          <span style={s.searchIcon}>🔍</span>
          <input value={search} onChange={e=>setSearch(e.target.value)}
            placeholder="Search employees..." style={s.search} />
        </div>
        <div style={s.tabs}>
          {depts.map(d => (
            <button key={d} onClick={() => setDept(d)}
              style={{...s.tab, ...(deptFilter===d ? s.tabActive : {})}}>
              {d}
            </button>
          ))}
        </div>
      </div>

      {/* Table */}
      <div style={s.tableWrap}>
        <table style={s.table}>
          <thead>
            <tr>
              {['Employee','Department','Role','Salary','Status','Actions'].map(h => (
                <th key={h} style={s.th}>{h}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {filtered.length === 0 ? (
              <tr><td colSpan={6} style={{...s.td, textAlign:'center', color:'#475569', padding:40}}>
                No employees found.
              </td></tr>
            ) : filtered.map((emp, i) => (
              <tr key={emp.id} style={{...s.tr, animationDelay:`${i*0.05}s`}}>
                <td style={s.td}>
                  <div style={s.empCell}>
                    <div style={{...s.avatar, background: DEPT_COLOR[emp.dept] || '#3b82f6'}}>
                      {emp.avatar || emp.name.slice(0,2).toUpperCase()}
                    </div>
                    <div>
                      <div style={s.empName}>{emp.name}</div>
                      <div style={s.empId}>ID #{emp.id}</div>
                    </div>
                  </div>
                </td>
                <td style={s.td}>
                  <span style={{...s.deptBadge, color: DEPT_COLOR[emp.dept]||'#94a3b8', borderColor: DEPT_COLOR[emp.dept]||'#94a3b8'}}>
                    {emp.dept}
                  </span>
                </td>
                <td style={{...s.td, color:'#94a3b8', fontSize:13}}>{emp.role}</td>
                <td style={{...s.td, fontWeight:600, color:'#f1f5f9'}}>
                  ₹{emp.salary.toLocaleString()}
                </td>
                <td style={s.td}>
                  <span style={{...s.statusBadge, ...STATUS_STYLE[emp.status]}}>
                    {emp.status}
                  </span>
                </td>
                <td style={s.td}>
                  <div style={s.actions}>
                    <button onClick={() => handleEdit(emp)} style={s.editBtn}>Edit</button>
                    {role === 'admin' && (
                      <button onClick={() => handleDelete(emp.id, emp.name)} style={s.delBtn}>Delete</button>
                    )}
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div style={s.footer}>{filtered.length} of {employees.length} employees</div>
    </div>
  );
}

const s = {
  wrap: { display:'flex', flexDirection:'column', gap:20 },
  statsRow: { display:'grid', gridTemplateColumns:'repeat(4,1fr)', gap:14 },
  statCard: {
    background:'#111827', border:'1px solid #1e2d45', borderRadius:12,
    padding:'18px 20px', display:'flex', flexDirection:'column', gap:4,
  },
  statVal: { fontFamily:"'Syne',sans-serif", fontSize:28, fontWeight:800 },
  statLabel: { fontSize:12, color:'#475569', textTransform:'uppercase', letterSpacing:1 },
  toolbar: { display:'flex', gap:12, alignItems:'center', flexWrap:'wrap' },
  searchWrap: { position:'relative', flex:1, minWidth:200 },
  searchIcon: { position:'absolute', left:12, top:'50%', transform:'translateY(-50%)', fontSize:13 },
  search: {
    width:'100%', background:'#111827', border:'1px solid #1e2d45',
    borderRadius:10, padding:'10px 14px 10px 36px', color:'#f1f5f9',
    fontSize:13, outline:'none', fontFamily:"'DM Sans',sans-serif",
  },
  tabs: { display:'flex', gap:6, flexWrap:'wrap' },
  tab: {
    background:'none', border:'1px solid #1e2d45', borderRadius:8,
    padding:'7px 14px', color:'#94a3b8', fontSize:12, cursor:'pointer', fontFamily:"'DM Sans',sans-serif",
  },
  tabActive: { background:'#3b82f620', borderColor:'#3b82f6', color:'#3b82f6' },
  tableWrap: { background:'#111827', border:'1px solid #1e2d45', borderRadius:14, overflow:'hidden' },
  table: { width:'100%', borderCollapse:'collapse' },
  th: {
    padding:'13px 18px', textAlign:'left', fontSize:11, fontWeight:600,
    color:'#475569', letterSpacing:1, textTransform:'uppercase', borderBottom:'1px solid #1e2d45',
  },
  tr: { transition:'background .15s', cursor:'default' },
  td: { padding:'14px 18px', fontSize:13, color:'#cbd5e1', borderBottom:'1px solid #1e2d4540' },
  empCell: { display:'flex', alignItems:'center', gap:12 },
  avatar: { width:38, height:38, borderRadius:10, display:'flex', alignItems:'center', justifyContent:'center', fontSize:13, fontWeight:700, color:'#fff', flexShrink:0 },
  empName: { fontSize:14, fontWeight:600, color:'#f1f5f9' },
  empId: { fontSize:11, color:'#475569', marginTop:2 },
  deptBadge: { border:'1px solid', borderRadius:6, padding:'3px 10px', fontSize:11, fontWeight:600 },
  statusBadge: { borderRadius:6, padding:'4px 10px', fontSize:11, fontWeight:600 },
  actions: { display:'flex', gap:8 },
  editBtn: { background:'#3b82f620', border:'1px solid #3b82f6', color:'#3b82f6', borderRadius:7, padding:'5px 14px', fontSize:12, cursor:'pointer', fontWeight:600 },
  delBtn: { background:'#ef444420', border:'1px solid #ef4444', color:'#ef4444', borderRadius:7, padding:'5px 14px', fontSize:12, cursor:'pointer', fontWeight:600 },
  footer: { fontSize:12, color:'#475569', textAlign:'right' },
};