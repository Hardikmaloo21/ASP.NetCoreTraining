import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h2>Dashboard</h2>
    <p>Total Items: {{totalItems()}}</p>
    <p>Total Orders: {{cart.length}}</p>
    <p>Total Amount: ₹{{totalAmount()}}</p>
  `
})
export class DashboardComponent {

  @Input() cart: any[] = [];

  totalItems() {
    return this.cart.reduce((sum, i) => sum + i.quantity, 0);
  }

  totalAmount() {
    return this.cart.reduce((sum, i) => sum + i.price * i.quantity, 0);
  }
}