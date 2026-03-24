import { Component, OnInit } from '@angular/core';
import { Product } from '../product';
import { ProductService } from '../product.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [CommonModule, RouterModule],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class ProductComponent implements OnInit{
  products: Product[] = [];

  constructor(private productServices : ProductService ) {}
    ngOnInit(): void {
      this.products = this.productServices.getProducts();
    }
  }