import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  private apiUrl = 'http://localhost:5013/api/Activity'

  constructor(private http: HttpClient, private authService: AuthService) { }

  // Get all activities for the authenticated user
  getActivities(): Observable<any[]> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token || '878'}`);
    return this.http.get<any[]>(this.apiUrl, { headers });
  }

  getRecentActivities(): Observable<any[]> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token || '878'}`);
    return this.http.get<any[]>(this.apiUrl, { headers }).pipe(
      map((activities: any[]) => activities.slice(-3)) // last 3 items from the array
    );
  }

  // Get a single activity by ID
  getActivity(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // Create a new activity for the authenticated user
  createActivity(activity: any): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token || '878'}`);

    return this.http.post<any>(this.apiUrl, activity, { headers }); // Send the POST request to create an activity
  }

  // Update an existing activity
  updateActivity(id: number, activity: any): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token || '878'}`);

    return this.http.put<any>(`${this.apiUrl}/${id}`, activity, { headers });
  }

  // Delete an activity
  deleteActivity(id: number): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token || '878'}`);

    return this.http.delete<any>(`${this.apiUrl}/${id}`, { headers });
  }
}
