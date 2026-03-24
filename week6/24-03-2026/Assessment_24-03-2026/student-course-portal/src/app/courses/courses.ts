import { Component } from '@angular/core';
import { CourseService } from '../course.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h2>Courses</h2>

    <ul>
      <li *ngFor="let c of courses">
        {{ c.name }}
        <button (click)="viewDetails(c.id)">View</button>
      </li>
    </ul>
  `
})
export class CoursesComponent {

  courses: any[] = [];

  constructor(private cs: CourseService, private router: Router) {}

  ngOnInit() {
    this.courses = this.cs.getCourses();
  }

  viewDetails(id: number) {
    this.router.navigate(['/course', id]);
  }
}