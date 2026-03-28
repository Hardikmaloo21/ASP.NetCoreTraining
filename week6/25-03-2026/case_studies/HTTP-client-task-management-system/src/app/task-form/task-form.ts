import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Task } from '../task';
import { TaskService } from '../task.service';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './task-form.html',
  styleUrls: ['./task-form.css']
})
export class TaskFormComponent {
  @Input() task: Task | null = null;
  @Output() taskSaved = new EventEmitter<void>();

  newTask: Task = { title: '', completed: false };

  constructor(private taskService: TaskService) {}

  ngOnChanges() {
    if (this.task) {
      this.newTask = { ...this.task };
    }
  }

  saveTask() {
    if (!this.newTask.title.trim()) return;

    if (this.newTask.id) {
      this.taskService.updateTask(this.newTask).subscribe(() => {
        this.reset();
      });
    } else {
      this.taskService.addTask(this.newTask).subscribe(() => {
        this.reset();
      });
    }
  }

  reset() {
    this.newTask = { title: '', completed: false };
    this.taskSaved.emit();
  }
}