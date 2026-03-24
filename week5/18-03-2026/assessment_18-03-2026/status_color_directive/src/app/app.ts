import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatusColorDirective } from './status-color-directive';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, StatusColorDirective],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {

  students = [
    { name: 'Amit', marks: 85 },
    { name: 'Neha', marks: 45 },
    { name: 'Rahul', marks: 72 },
    { name: 'Priya', marks: 30 },
    { name: 'Sneha', marks: 50 }
  ];
}