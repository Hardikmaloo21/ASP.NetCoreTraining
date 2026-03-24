import { Component, signal } from '@angular/core';
import { OrderParent } from "./order-parent/order-parent";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ OrderParent ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  title = 'Lifecycle-Hook-demo';
}