import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

interface User {
  username: string;
  email: string;
  user_id: number;

  exp: number;
  iss: string;
  aud: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5013/api';
  private token: string | null = localStorage.getItem('auth_token');
  private user: User | null = null;

  constructor(private http: HttpClient) { }

  getUser(): User | null {
    return this.user || JSON.parse(localStorage.getItem('user') || 'null');
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Auth/login`, { username, password })
      .pipe(
        map(response => {
          if (response.token) {
            this.token = response.token;
            localStorage.setItem('auth_token', this.token || '');
          }
          return response;
        }),
        catchError(error => {
          console.error('Login failed', error);
          return of(null);
        })
      );
  }

  logout(): void {
    this.token = null;
    this.user = null;
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user');
  }

  isAuthenticated(): Observable<boolean> {
    return this.verifyToken().pipe(
      catchError(() => of(false))
    );
  }

  getToken(): string | null {
    return this.token;
  }

  verifyToken(): Observable<boolean> {
    const token = localStorage.getItem('auth_token');
    if (!token) {
      return of(false);
    }
    return this.http.post<any>(`${this.apiUrl}/Auth/verify`, { token }).pipe(
      map(response => {
        if (response.message === "Token is valid") {
          this.user = response.claims;
          localStorage.setItem('user', JSON.stringify(this.user));
          return true;
        }
        return false; // Token is invalid
      }),
      catchError(() => {
        return of(false);
      })
    );
  }

  refreshToken(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/refresh-token`, { token: this.token })
      .pipe(
        map(response => {
          if (response.token) {
            this.token = response.token;
            localStorage.setItem('auth_token', this.token || '');
          }
          return response;
        }),
        catchError(error => {
          console.error('Token refresh failed', error);
          return of(null);
        })
      );
  }
}
