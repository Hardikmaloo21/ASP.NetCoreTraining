import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],   
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {

  students = [
    { name: 'John', marks: 85 },
    { name: 'Alice', marks: 72 },
    { name: 'Bob', marks: 45 },
    { name: 'Emma', marks: 90 },
    { name: 'David', marks: 30 }
  ];

}