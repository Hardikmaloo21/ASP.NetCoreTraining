import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';
import { Login } from './auth/login/login';
import { EmployeeList } from './employee/employee-list/employee-list';


export const routes: Routes = [
    { path: 'login', component: Login },
    { path: 'employees', component: EmployeeList, canActivate: [authGuard] },
    { path: '', redirectTo: '/employees', pathMatch: 'full' }
];
