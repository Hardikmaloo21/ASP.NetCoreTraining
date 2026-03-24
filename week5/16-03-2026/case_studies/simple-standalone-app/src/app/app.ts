import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NewComponent } from './new-component/new-component';

@Component({
  selector: 'app-root',
  imports: [NewComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Angular Standalone Application');
}