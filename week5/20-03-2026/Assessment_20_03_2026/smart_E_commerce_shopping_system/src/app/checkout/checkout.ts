import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './checkout.html'
})
export class CheckoutComponent {

  user: any = {
    name: '',
    email: '',
    phone: '',
    gender: '',
    payment: ''
  };
}