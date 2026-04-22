import { Injectable } from '@angular/core';
import { Product } from './product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private products: Product[] = [
    { id: 1, name: 'Laptop', category: 'Electronics', price: 850.50, stock: 10 },
    { id: 2, name: 'Phone', category: 'Electronics', price: 500, stock: 0 },
    { id: 3, name: 'Shirt', category: 'Clothing', price: 30, stock: 25 },
    { id: 4, name: 'Shoes', category: 'Clothing', price: 60, stock: 5 },
    { id: 5, name: 'Book', category: 'Education', price: 15, stock: 0 }
  ];

  getProducts(): Product[] {
    return this.products;
  }
}