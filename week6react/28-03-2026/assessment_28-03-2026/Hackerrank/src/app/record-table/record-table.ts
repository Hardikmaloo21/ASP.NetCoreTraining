import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

interface Transaction {
  id?: number;
  date: string;
  description: string;
  type: number;
  amount: number;
  balance: string;
}

@Component({
  selector: 'app-record-table',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './record-table.html',
  styleUrls: ['./record-table.css']
})
export class RecordTableComponent implements OnInit {
  transactions: Transaction[] = [];
  displayedTransactions: Transaction[] = [];
  selectedDate: string = '';

  showForm: boolean = false;
  isSubmitting: boolean = false;
  formError: string = '';
  formSuccess: string = '';

  newTransaction: Transaction = {
    date: '',
    description: '',
    type: 0,
    amount: 0,
    balance: ''
  };

  private apiUrl = 'http://localhost:5205/api/transactions';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.http.get<Transaction[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.transactions = data;
        this.displayedTransactions = [...data];
      },
      error: (err) => console.error('Failed to load transactions', err)
    });
  }

  onFilter(): void {
    if (!this.selectedDate) return;
    this.http.get<Transaction[]>(`${this.apiUrl}/filter?date=${this.selectedDate}`).subscribe({
      next: (data) => this.displayedTransactions = data,
      error: (err) => console.error('Failed to filter', err)
    });
  }

  onSortByAmount(): void {
    this.http.get<Transaction[]>(`${this.apiUrl}/sorted`).subscribe({
      next: (data) => this.displayedTransactions = data,
      error: (err) => console.error('Failed to sort', err)
    });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    this.formError = '';
    this.formSuccess = '';
    this.resetForm();
  }

  resetForm(): void {
    this.newTransaction = {
      date: '',
      description: '',
      type: 0,
      amount: 0,
      balance: ''
    };
  }

  // Validates and formats date to YYYY-MM-DD
  private formatDate(date: string): string {
    const d = new Date(date);
    const yyyy = d.getFullYear();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    return `${yyyy}-${mm}-${dd}`;
  }

  addTransaction(): void {
    this.formError = '';
    this.formSuccess = '';

    // Validation
    if (!this.newTransaction.date) {
      this.formError = 'Date is required.'; return;
    }
    if (!this.newTransaction.description.trim()) {
      this.formError = 'Description is required.'; return;
    }
    if (!this.newTransaction.amount || this.newTransaction.amount <= 0) {
      this.formError = 'Amount must be greater than 0.'; return;
    }
    if (!this.newTransaction.balance.trim()) {
      this.formError = 'Balance is required (e.g. $5,000.00).'; return;
    }

    // Format date to YYYY-MM-DD
    this.newTransaction.date = this.formatDate(this.newTransaction.date);

    this.isSubmitting = true;

    this.http.post<Transaction>(this.apiUrl, this.newTransaction).subscribe({
      next: (saved) => {
        this.formSuccess = `Transaction added successfully for ${saved.date}!`;
        this.isSubmitting = false;
        this.resetForm();
        this.loadAll(); // Refresh table with new data
        setTimeout(() => {
          this.showForm = false;
          this.formSuccess = '';
        }, 1500);
      },
      error: (err) => {
        this.formError = 'Failed to save transaction. Please try again.';
        this.isSubmitting = false;
        console.error(err);
      }
    });
  }
}