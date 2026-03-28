import { Component } from '@angular/core';
import { RecordTableComponent } from './record-table/record-table';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RecordTableComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {}