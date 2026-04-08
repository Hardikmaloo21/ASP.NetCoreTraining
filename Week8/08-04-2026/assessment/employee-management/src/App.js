import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
// import { logout } from './features/auth/authSlice';
// import { clearNotification, setActiveTab } from './features/ui/uiSlice';
import LoginForm from './components/LoginForm';
import EmployeeList from './components/EmployeeList';
import EmployeeForm from './components/EmployeeForm';
import { logout } from './features/auth/authSlice';
import { clearNotification, setActiveTab, setNotification } from './features/ui/uiSlice';

export default function App() {
  const dispatch  = useDispatch();
  const isAuth    = useSelector(s => s.auth.isAuthenticated);
  const user      = useSelector(s => s.auth.user);
  const role      = useSelector(s => s.auth.role);
  const notif     = useSelector(s => s.ui.notification);
  const activeTab = useSelector(s => s.ui.activeTab);
  const employees = useSelector(s => s.employees.list);

  const [showForm, setShowForm] = useState(false);

  // Auto-clear notification
  useEffect(() => {
    if (notif) {
      const t = setTimeout(() => dispatch(clearNotification()), 3500);
      return () => clearTimeout(t);
    }
  }, [notif, dispatch]);

  if (!isAuth) return <LoginForm />;

  const NOTIF_COLOR = { success:'#10b981', error:'#ef4444', info:'#3b82f6' };

  return (
    <div style={a.shell}>
      {/* Sidebar */}
      <aside style={a.sidebar}>
        <div style={a.logo}>
          <div style={a.logoBox}>
            <svg width="22" height="22" viewBox="0 0 28 28" fill="none">
              <rect width="12" height="12" x="2" y="2" rx="3" fill="#3b82f6"/>
              <rect width="12" height="12" x="14" y="2" rx="3" fill="#06b6d4" opacity="0.7"/>
              <rect width="12" height="12" x="2" y="14" rx="3" fill="#06b6d4" opacity="0.7"/>
              <rect width="12" height="12" x="14" y="14" rx="3" fill="#3b82f6"/>
            </svg>
          </div>
          <span style={a.brandName}>EmpDesk</span>
        </div>

        <nav style={a.nav}>
          {[
            { id:'dashboard', icon:'🏠', label:'Dashboard' },
            { id:'employees', icon:'👥', label:'Employees' },
            { id:'analytics', icon:'📊', label:'Analytics' },
            { id:'settings',  icon:'⚙️', label:'Settings'  },
          ].map(item => (
            <button key={item.id} onClick={() => dispatch(setActiveTab(item.id))}
              style={{...a.navItem, ...(activeTab===item.id ? a.navActive : {})}}>
              <span style={a.navIcon}>{item.icon}</span>
              <span>{item.label}</span>
            </button>
          ))}
        </nav>

        {/* User Card */}
        <div style={a.userCard}>
          <div style={a.userAvatar}>{user?.[0]?.toUpperCase()}</div>
          <div style={{flex:1, minWidth:0}}>
            <div style={a.userName}>{user}</div>
            <div style={{...a.userRole, color: role==='admin' ? '#3b82f6' : '#10b981'}}>
              {role}
            </div>
          </div>
          <button onClick={() => dispatch(logout())} style={a.logoutBtn} title="Logout">↩</button>
        </div>
      </aside>

      {/* Main */}
      <main style={a.main}>
        {/* Topbar */}
        <div style={a.topbar}>
          <div>
            <h1 style={a.pageTitle}>
              {activeTab === 'dashboard' && 'Dashboard'}
              {activeTab === 'employees' && 'Employees'}
              {activeTab === 'analytics' && 'Analytics'}
              {activeTab === 'settings'  && 'Settings'}
            </h1>
            <p style={a.pageDate}>
              {new Date().toLocaleDateString('en-IN',{weekday:'long',year:'numeric',month:'long',day:'numeric'})}
            </p>
          </div>
          {(activeTab === 'dashboard' || activeTab === 'employees') && role === 'admin' && (
            <button onClick={() => setShowForm(true)} style={a.addBtn}>
              + Add Employee
            </button>
          )}
        </div>

        {/* Content */}
        <div style={a.content}>
          {(activeTab === 'dashboard' || activeTab === 'employees') && (
            <EmployeeList onEdit={() => setShowForm(true)} />
          )}
          {activeTab === 'analytics' && <AnalyticsPage />}
          {activeTab === 'settings' && <SettingsPage />}
        </div>
      </main>

      {/* Employee Form Modal */}
      {showForm && <EmployeeForm onClose={() => setShowForm(false)} />}

      {/* Toast Notification */}
      {notif && (
        <div style={{...a.toast, borderColor: NOTIF_COLOR[notif.type], background:`${NOTIF_COLOR[notif.type]}15`}}>
          <span style={{color: NOTIF_COLOR[notif.type], fontWeight:700}}>
            {notif.type === 'success' ? '✅' : notif.type === 'error' ? '❌' : 'ℹ️'}
          </span>
          <span style={{color:'#f1f5f9', fontSize:13}}>{notif.message}</span>
        </div>
      )}
    </div>
  );
}

const a = {
  shell: { display:'flex', height:'100vh', overflow:'hidden', background:'#0a0e1a' },

  // Sidebar
  sidebar: {
    width:220, background:'#111827', borderRight:'1px solid #1e2d45',
    display:'flex', flexDirection:'column', padding:'24px 16px', flexShrink:0,
  },
  logo: { display:'flex', alignItems:'center', gap:10, marginBottom:36, paddingLeft:4 },
  logoBox: { width:38, height:38, background:'#0a0e1a', border:'1px solid #1e2d45', borderRadius:10, display:'flex', alignItems:'center', justifyContent:'center' },
  brandName: { fontFamily:"'Syne',sans-serif", fontSize:18, fontWeight:800, color:'#f1f5f9' },
  nav: { display:'flex', flexDirection:'column', gap:4, flex:1 },
  navItem: {
    display:'flex', alignItems:'center', gap:10, padding:'10px 14px', borderRadius:10,
    background:'none', border:'none', color:'#94a3b8', cursor:'pointer',
    fontSize:13, fontFamily:"'DM Sans',sans-serif", fontWeight:500,
    textAlign:'left', transition:'all .15s',
  },
  navActive: { background:'#3b82f620', color:'#3b82f6' },
  navIcon: { fontSize:16, width:20, textAlign:'center' },
  userCard: {
    display:'flex', alignItems:'center', gap:10, background:'#0a0e1a',
    border:'1px solid #1e2d45', borderRadius:12, padding:'12px',
  },
  userAvatar: {
    width:34, height:34, borderRadius:9, background:'linear-gradient(135deg,#3b82f6,#06b6d4)',
    display:'flex', alignItems:'center', justifyContent:'center',
    fontSize:14, fontWeight:700, color:'#fff', flexShrink:0,
  },
  userName: { fontSize:13, fontWeight:600, color:'#f1f5f9', overflow:'hidden', textOverflow:'ellipsis', whiteSpace:'nowrap' },
  userRole: { fontSize:11, fontWeight:600, textTransform:'uppercase', letterSpacing:0.5 },
  logoutBtn: { background:'none', border:'none', color:'#94a3b8', cursor:'pointer', fontSize:16, padding:4 },

  // Main
  main: { flex:1, display:'flex', flexDirection:'column', overflow:'hidden' },
  topbar: {
    display:'flex', justifyContent:'space-between', alignItems:'center',
    padding:'24px 32px 20px', borderBottom:'1px solid #1e2d45', flexShrink:0,
  },
  pageTitle: { fontFamily:"'Syne',sans-serif", fontSize:24, fontWeight:800, color:'#f1f5f9' },
  pageDate: { fontSize:12, color:'#475569', marginTop:3 },
  addBtn: {
    padding:'10px 22px', background:'linear-gradient(135deg,#3b82f6,#06b6d4)',
    border:'none', borderRadius:10, color:'#fff', fontSize:13, fontWeight:700,
    cursor:'pointer', fontFamily:"'Syne',sans-serif",
    boxShadow:'0 4px 14px #3b82f640',
  },
  content: { flex:1, overflowY:'auto', padding:'28px 32px' },
  placeholder: {
    display:'flex', flexDirection:'column', alignItems:'center', justifyContent:'center',
    height:300, background:'#111827', border:'1px solid #1e2d45', borderRadius:16,
  },

  // Toast
  toast: {
    position:'fixed', bottom:28, right:28, display:'flex', alignItems:'center', gap:10,
    background:'#111827', border:'1px solid', borderRadius:12,
    padding:'14px 20px', boxShadow:'0 8px 32px rgba(0,0,0,0.4)', zIndex:2000,
    animation:'slideIn .3s ease', maxWidth:360,
  },
};

// ─────────────────────────────────────────
// ANALYTICS PAGE
// ─────────────────────────────────────────
function AnalyticsPage() {
  const employees = useSelector(s => s.employees.list);

  // Dept breakdown
  const deptMap = employees.reduce((acc, e) => {
    acc[e.dept] = (acc[e.dept] || 0) + 1; return acc;
  }, {});

  // Salary ranges
  const ranges = { '0–50k':0, '50k–80k':0, '80k–100k':0, '100k+':0 };
  employees.forEach(e => {
    if (e.salary < 50000)       ranges['0–50k']++;
    else if (e.salary < 80000)  ranges['50k–80k']++;
    else if (e.salary < 100000) ranges['80k–100k']++;
    else                        ranges['100k+']++;
  });

  const totalSalary  = employees.reduce((s,e) => s + e.salary, 0);
  const avgSalary    = employees.length ? Math.round(totalSalary / employees.length) : 0;
  const maxSalary    = employees.length ? Math.max(...employees.map(e=>e.salary)) : 0;
  const activeCount  = employees.filter(e=>e.status==='Active').length;
  const activeRate   = employees.length ? Math.round((activeCount/employees.length)*100) : 0;

  const DEPT_COLORS = ['#3b82f6','#06b6d4','#10b981','#a855f7','#f59e0b','#ef4444'];
  const maxDeptCount = Math.max(...Object.values(deptMap), 1);
  const maxRange     = Math.max(...Object.values(ranges), 1);

  return (
    <div style={{display:'flex', flexDirection:'column', gap:20}}>

      {/* KPI Cards */}
      <div style={{display:'grid', gridTemplateColumns:'repeat(4,1fr)', gap:14}}>
        {[
          { label:'Total Payroll',    value:`₹${totalSalary.toLocaleString()}`,  icon:'💰', color:'#f59e0b', sub:'Monthly expense' },
          { label:'Avg Salary',       value:`₹${avgSalary.toLocaleString()}`,    icon:'📈', color:'#3b82f6', sub:'Per employee' },
          { label:'Highest Package',  value:`₹${maxSalary.toLocaleString()}`,    icon:'🏆', color:'#a855f7', sub:'Top earner' },
          { label:'Active Rate',      value:`${activeRate}%`,                    icon:'✅', color:'#10b981', sub:`${activeCount} of ${employees.length}` },
        ].map(k => (
          <div key={k.label} style={pg.kpiCard}>
            <div style={{display:'flex', justifyContent:'space-between', alignItems:'flex-start'}}>
              <div>
                <p style={{fontSize:11, color:'#475569', textTransform:'uppercase', letterSpacing:1, fontWeight:600}}>{k.label}</p>
                <p style={{fontFamily:"'Syne',sans-serif", fontSize:24, fontWeight:800, color: k.color, marginTop:6}}>{k.value}</p>
                <p style={{fontSize:11, color:'#94a3b8', marginTop:4}}>{k.sub}</p>
              </div>
              <span style={{fontSize:28}}>{k.icon}</span>
            </div>
          </div>
        ))}
      </div>

      <div style={{display:'grid', gridTemplateColumns:'1fr 1fr', gap:20}}>

        {/* Dept Bar Chart */}
        <div style={pg.chartCard}>
          <h3 style={pg.chartTitle}>👥 Employees by Department</h3>
          <div style={{display:'flex', flexDirection:'column', gap:12, marginTop:16}}>
            {Object.entries(deptMap).map(([dept, count], i) => (
              <div key={dept}>
                <div style={{display:'flex', justifyContent:'space-between', marginBottom:6}}>
                  <span style={{fontSize:13, color:'#cbd5e1', fontWeight:500}}>{dept}</span>
                  <span style={{fontSize:13, fontWeight:700, color: DEPT_COLORS[i % DEPT_COLORS.length]}}>{count}</span>
                </div>
                <div style={pg.barTrack}>
                  <div style={{
                    ...pg.barFill,
                    width:`${(count/maxDeptCount)*100}%`,
                    background: DEPT_COLORS[i % DEPT_COLORS.length],
                    boxShadow:`0 0 8px ${DEPT_COLORS[i%DEPT_COLORS.length]}60`,
                  }}/>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* Salary Range Chart */}
        <div style={pg.chartCard}>
          <h3 style={pg.chartTitle}>💼 Salary Distribution</h3>
          <div style={{display:'flex', flexDirection:'column', gap:12, marginTop:16}}>
            {Object.entries(ranges).map(([range, count], i) => (
              <div key={range}>
                <div style={{display:'flex', justifyContent:'space-between', marginBottom:6}}>
                  <span style={{fontSize:13, color:'#cbd5e1', fontWeight:500}}>{range}</span>
                  <span style={{fontSize:13, fontWeight:700, color:'#06b6d4'}}>{count} emp</span>
                </div>
                <div style={pg.barTrack}>
                  <div style={{
                    ...pg.barFill,
                    width: count === 0 ? '2%' : `${(count/maxRange)*100}%`,
                    background:'linear-gradient(90deg,#3b82f6,#06b6d4)',
                  }}/>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* Status Donut (CSS) */}
        <div style={pg.chartCard}>
          <h3 style={pg.chartTitle}>📋 Status Breakdown</h3>
          <div style={{display:'flex', gap:24, alignItems:'center', marginTop:16}}>
            {/* Visual donut via conic-gradient */}
            <div style={{
              width:120, height:120, borderRadius:'50%', flexShrink:0,
              background: (() => {
                const active   = employees.filter(e=>e.status==='Active').length;
                const onLeave  = employees.filter(e=>e.status==='On Leave').length;
                const inactive = employees.filter(e=>e.status==='Inactive').length;
                const total    = employees.length || 1;
                const a = (active/total)*360;
                const l = (onLeave/total)*360;
                return `conic-gradient(#10b981 0deg ${a}deg, #f59e0b ${a}deg ${a+l}deg, #ef4444 ${a+l}deg 360deg)`;
              })(),
              position:'relative',
            }}>
              <div style={{
                position:'absolute', inset:18, borderRadius:'50%',
                background:'#111827', display:'flex', flexDirection:'column',
                alignItems:'center', justifyContent:'center',
              }}>
                <span style={{fontFamily:"'Syne',sans-serif", fontSize:20, fontWeight:800, color:'#f1f5f9'}}>{employees.length}</span>
                <span style={{fontSize:9, color:'#475569', textTransform:'uppercase'}}>Total</span>
              </div>
            </div>
            <div style={{display:'flex', flexDirection:'column', gap:10}}>
              {[
                {label:'Active',   color:'#10b981', count: employees.filter(e=>e.status==='Active').length},
                {label:'On Leave', color:'#f59e0b', count: employees.filter(e=>e.status==='On Leave').length},
                {label:'Inactive', color:'#ef4444', count: employees.filter(e=>e.status==='Inactive').length},
              ].map(st => (
                <div key={st.label} style={{display:'flex', alignItems:'center', gap:8}}>
                  <div style={{width:10, height:10, borderRadius:'50%', background:st.color, flexShrink:0}}/>
                  <span style={{fontSize:13, color:'#94a3b8'}}>{st.label}</span>
                  <span style={{fontSize:13, fontWeight:700, color:st.color, marginLeft:'auto', minWidth:20}}>{st.count}</span>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* Top Earners */}
        <div style={pg.chartCard}>
          <h3 style={pg.chartTitle}>🏆 Top Earners</h3>
          <div style={{display:'flex', flexDirection:'column', gap:10, marginTop:16}}>
            {[...employees].sort((a,b)=>b.salary-a.salary).slice(0,4).map((emp,i) => (
              <div key={emp.id} style={{display:'flex', alignItems:'center', gap:12}}>
                <span style={{
                  width:22, height:22, borderRadius:'50%', fontSize:11, fontWeight:800,
                  display:'flex', alignItems:'center', justifyContent:'center',
                  background: i===0?'#f59e0b':i===1?'#94a3b8':i===2?'#cd7c2f':'#1e2d45',
                  color: i<3?'#0a0e1a':'#94a3b8', flexShrink:0,
                }}>#{i+1}</span>
                <div style={{
                  width:32, height:32, borderRadius:8, display:'flex', alignItems:'center',
                  justifyContent:'center', fontSize:11, fontWeight:700, color:'#fff', flexShrink:0,
                  background:'linear-gradient(135deg,#3b82f6,#06b6d4)',
                }}>{emp.avatar}</div>
                <div style={{flex:1, minWidth:0}}>
                  <div style={{fontSize:13, fontWeight:600, color:'#f1f5f9', overflow:'hidden', textOverflow:'ellipsis', whiteSpace:'nowrap'}}>{emp.name}</div>
                  <div style={{fontSize:11, color:'#475569'}}>{emp.dept}</div>
                </div>
                <span style={{fontSize:13, fontWeight:700, color:'#10b981', flexShrink:0}}>₹{emp.salary.toLocaleString()}</span>
              </div>
            ))}
          </div>
        </div>

      </div>
    </div>
  );
}

// ─────────────────────────────────────────
// SETTINGS PAGE
// ─────────────────────────────────────────
function SettingsPage() {
  const dispatch = useDispatch();
  const user     = useSelector(s => s.auth.user);
  const role     = useSelector(s => s.auth.role);
  const [notifOn, setNotifOn]   = React.useState(true);
  const [autoSave, setAutoSave] = React.useState(true);
  const [compact, setCompact]   = React.useState(false);
  const [saved, setSaved]       = React.useState(false);

  const handleSave = () => {
    setSaved(true);
    dispatch(setNotification({ type:'success', message:'Settings saved successfully!' }));
    setTimeout(() => setSaved(false), 2000);
  };

  const Toggle = ({ val, onToggle, color='#3b82f6' }) => (
    <div onClick={onToggle} style={{
      width:44, height:24, borderRadius:12, cursor:'pointer', flexShrink:0,
      background: val ? color : '#1e2d45', position:'relative', transition:'background .2s',
    }}>
      <div style={{
        position:'absolute', top:3, left: val ? 23 : 3,
        width:18, height:18, borderRadius:'50%', background:'#fff',
        transition:'left .2s', boxShadow:'0 2px 4px rgba(0,0,0,0.3)',
      }}/>
    </div>
  );

  return (
    <div style={{display:'flex', flexDirection:'column', gap:20, maxWidth:720}}>

      {/* Profile Card */}
      <div style={pg.settingSection}>
        <h3 style={pg.sectionTitle}>👤 Profile</h3>
        <div style={{display:'flex', alignItems:'center', gap:16, marginTop:16, padding:'16px', background:'#0a0e1a', borderRadius:12, border:'1px solid #1e2d45'}}>
          <div style={{
            width:56, height:56, borderRadius:14, background:'linear-gradient(135deg,#3b82f6,#06b6d4)',
            display:'flex', alignItems:'center', justifyContent:'center',
            fontSize:22, fontWeight:800, color:'#fff', flexShrink:0,
          }}>{user?.[0]?.toUpperCase()}</div>
          <div>
            <p style={{fontFamily:"'Syne',sans-serif", fontSize:18, fontWeight:700, color:'#f1f5f9'}}>{user}</p>
            <p style={{fontSize:12, color: role==='admin'?'#3b82f6':'#10b981', fontWeight:600, textTransform:'uppercase', letterSpacing:1, marginTop:2}}>{role} account</p>
          </div>
          <div style={{marginLeft:'auto', background:'#3b82f620', border:'1px solid #3b82f6', borderRadius:8, padding:'6px 16px', fontSize:12, color:'#3b82f6', fontWeight:600}}>
            {role==='admin' ? '🔑 Full Access' : '👁 View Only'}
          </div>
        </div>
      </div>

      {/* Preferences */}
      <div style={pg.settingSection}>
        <h3 style={pg.sectionTitle}>🎛️ Preferences</h3>
        <div style={{display:'flex', flexDirection:'column', gap:0, marginTop:12}}>
          {[
            { label:'Toast Notifications', desc:'Show success/error alerts after actions', val:notifOn,  set:()=>setNotifOn(!notifOn),  color:'#10b981' },
            { label:'Auto-Save State',     desc:'Persist data to localStorage on changes', val:autoSave, set:()=>setAutoSave(!autoSave), color:'#3b82f6' },
            { label:'Compact Table View',  desc:'Reduce row height in employee table',     val:compact,  set:()=>setCompact(!compact),   color:'#a855f7' },
          ].map((item, i, arr) => (
            <div key={item.label} style={{
              display:'flex', justifyContent:'space-between', alignItems:'center',
              padding:'16px 0', borderBottom: i<arr.length-1 ? '1px solid #1e2d45' : 'none',
            }}>
              <div>
                <p style={{fontSize:14, fontWeight:600, color:'#f1f5f9'}}>{item.label}</p>
                <p style={{fontSize:12, color:'#475569', marginTop:3}}>{item.desc}</p>
              </div>
              <Toggle val={item.val} onToggle={item.set} color={item.color} />
            </div>
          ))}
        </div>
      </div>

      {/* Redux State Info */}
      <div style={pg.settingSection}>
        <h3 style={pg.sectionTitle}>🗄️ Redux Store Info</h3>
        <div style={{display:'grid', gridTemplateColumns:'1fr 1fr 1fr', gap:12, marginTop:14}}>
          {[
            { slice:'auth',      color:'#3b82f6', desc:'Auth state, login/logout' },
            { slice:'employees', color:'#10b981', desc:'CRUD + selected employee' },
            { slice:'ui',        color:'#a855f7', desc:'Theme, loading, notifs' },
          ].map(s => (
            <div key={s.slice} style={{background:'#0a0e1a', border:`1px solid ${s.color}40`, borderRadius:10, padding:'14px'}}>
              <div style={{display:'flex', alignItems:'center', gap:8, marginBottom:6}}>
                <div style={{width:8, height:8, borderRadius:'50%', background:s.color}}/>
                <span style={{fontSize:12, fontWeight:700, color:s.color, textTransform:'uppercase', letterSpacing:1}}>{s.slice}</span>
              </div>
              <p style={{fontSize:11, color:'#475569'}}>{s.desc}</p>
              <p style={{fontSize:11, color:'#94a3b8', marginTop:4}}>Persisted ✅</p>
            </div>
          ))}
        </div>
      </div>

      {/* Danger Zone */}
      <div style={{...pg.settingSection, borderColor:'#ef444430'}}>
        <h3 style={{...pg.sectionTitle, color:'#ef4444'}}>⚠️ Danger Zone</h3>
        <div style={{display:'flex', alignItems:'center', justifyContent:'space-between', marginTop:14, padding:'14px', background:'#ef444410', borderRadius:10, border:'1px solid #ef444430'}}>
          <div>
            <p style={{fontSize:14, fontWeight:600, color:'#f1f5f9'}}>Sign out of account</p>
            <p style={{fontSize:12, color:'#475569', marginTop:2}}>You will be redirected to the login page</p>
          </div>
          <button onClick={() => dispatch(logout())} style={{
            background:'#ef444420', border:'1px solid #ef4444', color:'#ef4444',
            borderRadius:8, padding:'8px 20px', cursor:'pointer', fontWeight:700,
            fontSize:13, fontFamily:"'DM Sans',sans-serif",
          }}>Logout</button>
        </div>
      </div>

      {/* Save Button */}
      <div style={{display:'flex', justifyContent:'flex-end'}}>
        <button onClick={handleSave} style={{
          padding:'12px 32px', background: saved ? '#10b981' : 'linear-gradient(135deg,#3b82f6,#06b6d4)',
          border:'none', borderRadius:10, color:'#fff', fontSize:14, fontWeight:700,
          cursor:'pointer', fontFamily:"'Syne',sans-serif", transition:'background .3s',
          boxShadow:'0 4px 16px #3b82f640',
        }}>{saved ? '✅ Saved!' : 'Save Settings'}</button>
      </div>

    </div>
  );
}

// Shared page styles
const pg = {
  kpiCard: { background:'#111827', border:'1px solid #1e2d45', borderRadius:14, padding:'20px 22px' },
  chartCard: { background:'#111827', border:'1px solid #1e2d45', borderRadius:14, padding:'22px 24px' },
  chartTitle: { fontFamily:"'Syne',sans-serif", fontSize:15, fontWeight:700, color:'#f1f5f9' },
  barTrack: { height:8, background:'#1e2d45', borderRadius:4, overflow:'hidden' },
  barFill: { height:'100%', borderRadius:4, transition:'width .6s ease' },
  settingSection: { background:'#111827', border:'1px solid #1e2d45', borderRadius:14, padding:'22px 24px' },
  sectionTitle: { fontFamily:"'Syne',sans-serif", fontSize:15, fontWeight:700, color:'#f1f5f9' },
};