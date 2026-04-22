import { Component, OnInit } from '@angular/core';
import { ProductService } from './product.service';
import { Product } from './product.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  products: Product[] = [];
  filteredProducts: Product[] = [];

  selectedCategory: string = '';
  showInStockOnly: boolean = false;
  sortAscending: boolean = true;
  isLoading = true;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
  setTimeout(() => {
    this.products = this.productService.getProducts();
    this.filteredProducts = [...this.products];
    this.isLoading = false;
  }, 800);
}

applyFilter() {
  let temp = [...this.products];

  if (this.selectedCategory) {
    temp = temp.filter(p => p.category === this.selectedCategory);
  }

  if (this.showInStockOnly) {
    temp = temp.filter(p => p.stock > 0);
  }

  // 🔍 Search filter
  if (this.searchText) {
    temp = temp.filter(p =>
      p.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  this.filteredProducts = temp;
}

resetFilters() {
  this.selectedCategory = '';
  this.searchText = '';
  this.showInStockOnly = false;
  this.filteredProducts = [...this.products];
}

  sortByPrice() {
    this.sortAscending = !this.sortAscending;

    this.filteredProducts.sort((a, b) =>
      this.sortAscending ? a.price - b.price : b.price - a.price
    );
  }
  searchText: string = '';

get totalProducts() {
  return this.filteredProducts.length;
}

get inStockCount() {
  return this.filteredProducts.filter(p => p.stock > 0).length;
}

get outStockCount() {
  return this.filteredProducts.filter(p => p.stock === 0).length;
}

}