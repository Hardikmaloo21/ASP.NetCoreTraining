import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Cartservice } from '../cart.service';

@Component({
  selector: 'app-cart',
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
})
export class Cart {
  constructor(public cartService: Cartservice) {}

  get total(){
    return this.cartService.getTotal();
  }
}
