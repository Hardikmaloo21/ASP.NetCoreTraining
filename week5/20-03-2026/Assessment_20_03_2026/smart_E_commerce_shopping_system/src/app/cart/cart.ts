import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class CartComponent {

  @Input() cart: any[] = [];
  @Output() remove = new EventEmitter<number>();
  @Output() clear = new EventEmitter<void>();

  total() {
    return this.cart.reduce((sum, i) => sum + i.price * i.quantity, 0);
  }
}