import { Component, signal } from '@angular/core';
import { RxjsComponent } from "./rxjs/rxjs";

@Component({
  selector: 'app-root',
  imports: [RxjsComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('using_Rxjs_demo');
}
