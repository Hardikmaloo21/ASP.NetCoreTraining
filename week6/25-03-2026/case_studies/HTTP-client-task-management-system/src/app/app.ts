import { Component, ViewChild } from '@angular/core';
import { Task } from './task';
import { TaskFormComponent } from './task-form/task-form';
import { TaskListComponent } from './task-list/task-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [TaskFormComponent, TaskListComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent {
  selectedTask: Task | null = null;

  @ViewChild(TaskListComponent) list!: TaskListComponent;

  onEdit(task: Task) {
    this.selectedTask = task;
  }

  onSaved() {
    this.selectedTask = null;
    this.list.loadTasks(); // ✅ refresh list
  }
}