const API = "http://localhost:5210/api"

function hideAll(){
document.getElementById("loginSection").classList.add("hidden")
document.getElementById("registerSection").classList.add("hidden")
document.getElementById("dashboardSection").classList.add("hidden")
document.getElementById("applySection").classList.add("hidden")
document.getElementById("leaveSection").classList.add("hidden")
}

function showLogin(){
hideAll()
document.getElementById("loginSection").classList.remove("hidden")
}

function showRegister(){
hideAll()
document.getElementById("registerSection").classList.remove("hidden")
}

function showDashboard(){
hideAll()
document.getElementById("dashboardSection").classList.remove("hidden")
}

function showApplyLeave(){
hideAll()
document.getElementById("applySection").classList.remove("hidden")
}

function logout(){
localStorage.removeItem("token")
showLogin()
}



async function register(){

const username=document.getElementById("regUsername").value
const password=document.getElementById("regPassword").value
const role=document.getElementById("regRole").value

const res=await fetch(API+"/auth/register",{
method:"POST",
headers:{
"Content-Type":"application/json"
},
body:JSON.stringify({
Username:username,
Password:password,
Role:role
})
})

if(!res.ok){
alert("Register failed")
return
}

alert("Registered")
showLogin()
}



async function login(){

const username=document.getElementById("loginUsername").value
const password=document.getElementById("loginPassword").value

const res=await fetch(API+"/auth/login",{
method:"POST",
headers:{
"Content-Type":"application/json"
},
body:JSON.stringify({
Username:username,
Password:password
})
})

if(!res.ok){
alert("Login failed")
return
}

const data=await res.json()

localStorage.setItem("token",data.token)

showDashboard()
}



async function applyLeave(){

const leaveType=document.getElementById("leaveType").value
const startDate=document.getElementById("startDate").value
const endDate=document.getElementById("endDate").value
const reason=document.getElementById("reason").value

const res=await fetch(API+"/leave/apply",{
method:"POST",
headers:{
"Content-Type":"application/json",
"Authorization":"Bearer "+localStorage.getItem("token")
},
body:JSON.stringify({
LeaveType:leaveType,
StartDate:startDate,
EndDate:endDate,
Reason:reason
})
})

if(!res.ok){
const err=await res.text()
console.log(err)
alert("Leave request failed")
return
}

alert("Leave applied")
showDashboard()
}



async function loadLeaves(){

hideAll()
document.getElementById("leaveSection").classList.remove("hidden")

const res=await fetch(API+"/leave/all",{
headers:{
"Authorization":"Bearer "+localStorage.getItem("token")
}
})

if(!res.ok){
alert("Unauthorized")
logout()
return
}

const leaves=await res.json()

let html=""

leaves.forEach(l=>{
html+=`
<tr>
<td>${l.id}</td>
<td>${l.leaveType}</td>
<td>${l.status}</td>
<td>
<button onclick="approve(${l.id})">Approve</button>
<button onclick="reject(${l.id})">Reject</button>
</td>
</tr>
`
})

document.getElementById("leaveTable").innerHTML=html
}



async function approve(id){

await fetch(API+"/leave/approve/"+id,{
method:"PUT",
headers:{
"Authorization":"Bearer "+localStorage.getItem("token")
}
})

loadLeaves()
}



async function reject(id){

await fetch(API+"/leave/reject/"+id,{
method:"PUT",
headers:{
"Authorization":"Bearer "+localStorage.getItem("token")
}
})

loadLeaves()
}