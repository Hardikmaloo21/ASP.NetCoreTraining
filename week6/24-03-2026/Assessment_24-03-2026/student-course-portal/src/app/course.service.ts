import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private courses = [
    { id: 1, name: 'Angular', description: 'Learn Angular basics' },
    { id: 2, name: 'React', description: 'Learn React JS' },
    { id: 3, name: '.NET Core', description: 'Backend development' }
  ];

  getCourses() {
    return this.courses;
  }

  getCourseById(id: number) {
    return this.courses.find(c => c.id === id);
  }
}