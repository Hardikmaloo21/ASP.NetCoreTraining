import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  isLoggedIn = false;

  private isBrowser(): boolean {
    return typeof window !== 'undefined' && typeof localStorage !== 'undefined';
  }

  login(username: string, password: string) {
    if (username === 'admin' && password === 'admin') {
      this.isLoggedIn = true;

      if (this.isBrowser()) {
        localStorage.setItem('token', 'dummy_token');
      }

      return true;
    }
    return false;
  }

  logout() {
    this.isLoggedIn = false;

    if (this.isBrowser()) {
      localStorage.removeItem('token');
    }
  }

  isAuthenticated(): boolean {
    if (!this.isBrowser()) {
      return false;
    }

    return !!localStorage.getItem('token');
  }
}