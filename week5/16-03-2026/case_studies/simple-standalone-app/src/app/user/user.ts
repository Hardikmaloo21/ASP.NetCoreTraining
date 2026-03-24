import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule], // ✅ required for *ngFor
  templateUrl: './user.html',
  styleUrl: './user.css',
})
export class UserComponent {
  title = 'User Component';

  user = {
    name: 'John',
    age: 25,
  };

  users = ['Alice', 'Bob', 'Charlie']; // ✅ array for ngFor

  getGreeting() {
    return 'Hello from function!';
  }
}