import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClickBlockDirective } from './click-block.directive';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ClickBlockDirective],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {

  // ✅ class ke ANDAR hona chahiye
  isAllowed = false;

  // ✅ class ke ANDAR hona chahiye
  handleClick() {
    alert('Button clicked!');
  }

  
}