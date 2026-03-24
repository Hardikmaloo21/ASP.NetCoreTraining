import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PriceHighlightDirective } from './price-highlight-directive';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, PriceHighlightDirective], 
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {

  products = [
    { name: 'Laptop', price: 70000 },
    { name: 'Mobile', price: 20000 },
    { name: 'TV', price: 55000 },
    { name: 'Headphones', price: 3000 }
  ];
}