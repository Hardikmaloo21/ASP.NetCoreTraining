import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CourseService } from '../course.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <h2>Course Detail</h2>

    <div *ngIf="course">
      <h3>{{ course.name }}</h3>
      <p>{{ course.description }}</p>
    </div>

    <a routerLink="/courses">Back</a>
  `
})
export class CourseDetailComponent {

  course: any;

  constructor(private route: ActivatedRoute, private cs: CourseService) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.course = this.cs.getCourseById(id);
  }
}