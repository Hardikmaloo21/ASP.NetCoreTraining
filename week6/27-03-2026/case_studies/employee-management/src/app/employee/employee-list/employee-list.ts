import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../../core/services/employee.service';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h2>Employee List</h2>
    <ul>
      <li *ngFor="let employee of employees">
        {{ employee.name }} - {{ employee.role }}
      </li>
    </ul>
  `
})
export class EmployeeList {
  employees : any[] = [];

  constructor(private employeeService: EmployeeService) {
    this.employees = this.employeeService.getEmployees();
  }
}
