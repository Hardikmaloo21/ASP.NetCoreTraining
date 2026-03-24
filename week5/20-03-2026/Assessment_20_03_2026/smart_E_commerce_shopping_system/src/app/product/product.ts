import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class ProductComponent {

  @Output() add = new EventEmitter<any>();

  search = '';
  category = '';

  products = [
    { id: 1, name: 'Laptop', price: 50000, category: 'Electronics', rating: 4, image: 'https://picsum.photos/100', quantity: 1 },
    { id: 2, name: 'Shoes', price: 2000, category: 'Fashion', rating: 5, image: 'https://picsum.photos/100', quantity: 1 }
  ];

  filtered() {
    return this.products.filter(p =>
      p.name.toLowerCase().includes(this.search.toLowerCase()) &&
      (this.category === '' || p.category === this.category)
    );
  }

  addToCart(p: any) {
    this.add.emit(p);
  }
}