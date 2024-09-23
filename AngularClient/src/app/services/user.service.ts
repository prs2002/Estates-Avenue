import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { Executive, User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7033/api/users';

  constructor(private http: HttpClient, private authService: AuthService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
  getToken(): string | null {
    return localStorage.getItem('jwt'); // Replace with your storage logic
  }
  getCustomers(): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.get(`${this.apiUrl}/getCustomers`, { headers });
  }  
  getExecutives(): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.get(`${this.apiUrl}/getExecutives`, { headers });
  }
  getExecutivesByLocation(location: string): Observable<User[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<User[]>(`${this.apiUrl}/by-location/${location}`, { headers });
  }
  fetchExecutivesByLocation(locality: string): Observable<Executive[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<Executive[]>(`${this.apiUrl}/by-location/${locality}`, { headers });
  }
  deleteUser(UserId: string): Observable<void> {
    const headers = this.getAuthHeaders();
    return this.http.delete<void>(`${this.apiUrl}/${UserId}`, { headers });
  }  
  deleteUserByEmail(email: string): Observable<any> {
    const headers = this.getAuthHeaders(); // Ensure you're using the correct headers
    return this.http.delete(`${this.apiUrl}/delete-by-email/${email}`, { headers });
  }
  
}
