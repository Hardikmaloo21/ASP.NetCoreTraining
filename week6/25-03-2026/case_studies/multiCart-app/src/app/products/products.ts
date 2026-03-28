import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CartService } from '../cart.service';

@Component({
  selector: 'app-products',
  imports: [CommonModule],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products {

  products = [
    { id: 1, name: 'laptop', price: 1000 },
    { id: 2, name: 'mobile', price: 500 },
    { id: 3, name: 'tablet', price: 800 }
  ];

  constructor(private cartService: CartService) {}

  addToCart(product: any) {
    this.cartService.addToCart(product);
  }

  getCartItems() {
    return this.cartService.getItems();
  }
}
