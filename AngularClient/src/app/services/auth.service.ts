import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7033/api/users';

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    return this.http.post(`${this.apiUrl}/login`, loginData)
      .pipe(
        tap((response: any) => {
          localStorage.setItem('jwt', response.token);  // Store JWT
          localStorage.setItem('userType', response.user.userType);  // Store user type
        })
      );
  }
  register(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData, { withCredentials: true });
  }

  logout(){
    localStorage.removeItem('jwt');
    localStorage.removeItem('userType');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('jwt');  // Check if the JWT exists
  }
  getUserRole(): string | null {
    return localStorage.getItem('userType');
  }
  
}