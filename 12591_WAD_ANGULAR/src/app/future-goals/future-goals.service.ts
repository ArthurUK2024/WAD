import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { AuthService } from "../auth/auth.service"
import { FutureGoal, FutureGoalCreate } from './future-goals.model';

@Injectable({
  providedIn: 'root'
})
export class FutureGoalsService {
  private apiUrl = 'http://localhost:5013/api/FutureGoal';

  constructor(private http: HttpClient, private authService: AuthService) { }

  // GET all future goals
  getFutureGoals(): Observable<any[]> {
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<any[]>(this.apiUrl, { headers });
  }

  getRecentGoals(): Observable<any[]> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token || '878'}`);
    return this.http.get<any[]>(this.apiUrl, { headers }).pipe(
      map((activities: any[]) => activities.slice(-3))
    );
  }

  getFutureGoalById(id: number): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<any>(`${this.apiUrl}/${id}`, { headers });
  }

  // POST a new future goal
  createFutureGoal(futureGoal: FutureGoalCreate): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<any>(this.apiUrl, futureGoal, { headers });
  }

  // PUT to update an existing future goal
  updateFutureGoal(id: number, futureGoal: FutureGoal): Observable<void> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.put<void>(`${this.apiUrl}/${id}`, futureGoal, { headers });
  }

  // DELETE a future goal by ID
  deleteFutureGoal(id: number): Observable<void> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }
}
