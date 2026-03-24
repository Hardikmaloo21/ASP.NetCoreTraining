import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // ✅ ADD THIS

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, CommonModule], // ✅ ADD HERE
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  patientName: string = '';
  doctor: string = '';
  appointmentDate: string = '';
  consultationType: string = '';
  symptoms: string = '';

  fee: number = 0;
  submitted: boolean = false;

  doctors: string[] = [
    'Dr. Sharma',
    'Dr. Mehta',
    'Dr. Rao'
  ];

  today: string = new Date().toISOString().split('T')[0];

  updateFee() {
    if (this.consultationType === 'Online') {
      this.fee = 300;
    } else if (this.consultationType === 'Offline') {
      this.fee = 500;
    } else {
      this.fee = 0;
    }
  }

  submitForm() {
    this.submitted = true;
  }
}