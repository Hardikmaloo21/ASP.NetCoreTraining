import { Component, signal } from '@angular/core';
import { Cart } from "./cart/cart";
import { ProductList } from "./product-list/product-list";
import { Checkout } from "./checkout/checkout";

@Component({
  selector: 'app-root',
  imports: [Cart, ProductList, Checkout],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ecommerce-app');
}
