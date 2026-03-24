import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard';
import { CoursesComponent } from './courses/courses';
import { CourseDetailComponent } from './course-detail/course-detail';
import { ProfileComponent } from './profile/profile';

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'courses', component: CoursesComponent },
  { path: 'course/:id', component: CourseDetailComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
];