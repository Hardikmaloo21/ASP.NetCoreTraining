import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  // protected readonly title = signal('databinding-demo');
  productName = 'Laptop';
  price = 50000;
  quantity = 1;
  isAvailable = true;
  imageUrl = 'https://picsum.photos/150';
  customerName = '';
  address = '';

  increaseQty(){
    this.quantity++;
  }

  decreaseQty(){
    if(this.quantity > 1){
      this.quantity--;
    }
  }
  toggleAvailability(){
    this.isAvailable = !this.isAvailable;
  }

  getTotal(){
    return this.price * this.quantity;
  }
}
