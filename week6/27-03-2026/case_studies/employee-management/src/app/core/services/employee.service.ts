import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  employees = [
    { id: 1, name: 'John' , role : 'Developer' },
    { id: 2, name: 'Jane' , role : 'Designer' },
    { id: 3, name: 'Bob' , role : 'Tester' },
  ];

  getEmployees() {
    return this.employees;
  }

  getEmployee(id: number) {
    return this.employees.find((e) => e.id === id);
  }

  addEmployee(emp: any) {
    this.employees.push(emp);
  }

  updateEmployee(updateEmp: any) {
    const index = this.employees.findIndex((e) => e.id === updateEmp.id);
    if (index !== -1) {
      this.employees[index] = {...updateEmp};
    }
  }

  deleteEmployee(id: number) {
    this.employees = this.employees.filter((e) => e.id !== id);
  }

  searchEmployees(term: string) {
    return this.employees.filter(e =>
    e.name.toLowerCase().includes(term.toLowerCase()) ||
    e.role.toLowerCase().includes(term.toLowerCase())
    );
  }
}
