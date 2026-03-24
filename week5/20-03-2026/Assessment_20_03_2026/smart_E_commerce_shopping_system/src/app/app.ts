import { Component } from '@angular/core';
import { ProductComponent } from './product/product';
import { CartComponent } from './cart/cart';
import { CheckoutComponent } from './checkout/checkout';
import { DashboardComponent } from './dashboard/dashboard';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule,ProductComponent, CartComponent, CheckoutComponent, DashboardComponent],
  templateUrl: './app.html'
})
export class App {

  cart: any[] = [];
  page = 'products';

  addToCart(product: any) {
    const item = this.cart.find(p => p.id === product.id);
    if (item) item.quantity += product.quantity;
    else this.cart.push({ ...product });
  }

  removeItem(id: number) {
    this.cart = this.cart.filter(i => i.id !== id);
  }

  clearCart() {
    this.cart = [];
  }
}