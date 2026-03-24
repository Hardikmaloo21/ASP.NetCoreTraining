import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  template: `
    <h1>Student Course Portal 🎓</h1>

    <nav>
      <a routerLink="/dashboard">Dashboard</a> |
      <a routerLink="/courses">Courses</a> |
      <a routerLink="/profile">Profile</a>
    </nav>

    <hr>

    <router-outlet></router-outlet>
  `
})
export class AppComponent {}