import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common'; // ✅ ADD THIS
import { TaskService } from '../task.service';
import { Task } from '../task';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule], // ✅ ADD THIS
  templateUrl: './task-list.html',
  styleUrls: ['./task-list.css']
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];

  @Output() editTask = new EventEmitter<Task>();

  constructor(private taskService: TaskService) {}

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks().subscribe(data => {
      this.tasks = data.slice(0, 10);
    });
  }

  trackByFn(index: number, task: Task): number {
    return task.id!;
  }

  toggle(task: Task) {
    this.taskService
      .updateTaskStatus(task.id!, !task.completed)
      .subscribe(() => {
        task.completed = !task.completed;
      });
  }

  delete(id: number) {
    this.taskService.deleteTask(id).subscribe(() => {
      this.tasks = this.tasks.filter(t => t.id !== id);
    });
  }
}