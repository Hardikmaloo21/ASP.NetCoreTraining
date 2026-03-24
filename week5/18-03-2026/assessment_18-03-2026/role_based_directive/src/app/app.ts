import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RoleDirective } from './role';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule, RoleDirective],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {

  userRole = 'user'; // default

}