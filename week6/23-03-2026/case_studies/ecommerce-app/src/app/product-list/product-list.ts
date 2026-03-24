import { Component , OnInit } from '@angular/core';
import { Productservice } from '../product.service';
import { Cartservice } from '../cart.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList implements OnInit {
  products: any[] = [];

  constructor(private productService: Productservice, private cartService: Cartservice) {}

  ngOnInit() {
    this.products = this.productService.getProducts();
  }

  addToCart(product: any) {
    this.cartService.addToCart(product);
  }
}

