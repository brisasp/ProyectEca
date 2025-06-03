import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginDTO } from '../models/loginDTO';
import { LoginResponse } from '../models/loginResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly baseUrl = 'https://localhost:7016/api/users';
  private loginUrl = `${this.baseUrl}/login`;
  private token: string | null = null;

  constructor() {
    
    this.token = localStorage.getItem('authToken');
  }

  login(credentials: LoginDTO): Observable<LoginResponse> {
  return new Observable<LoginResponse>(observer => {
    fetch(this.loginUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(credentials)
    })
    .then(response => response.json())
    .then((data: LoginResponse) => {
      if (data?.result?.token) {
        this.setToken(data.result.token);
      }
      observer.next(data);
      observer.complete();
    })
    .catch(error => {
      observer.error(error);
    });
  });
} 

  setToken(token: string): void {
    this.token = token;
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return this.token || localStorage.getItem('authToken');
  }

  logout(): void {
    this.token = null;
    localStorage.removeItem('authToken');
  }
  isAuthenticated(): boolean {
    return !!this.getToken();
  }
  
}