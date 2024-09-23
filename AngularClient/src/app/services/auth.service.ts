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

  getToken(): string | null {
    return localStorage.getItem('jwt'); // Replace with your storage logic
  }
  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    return this.http.post(`${this.apiUrl}/login`, loginData)
      .pipe(
        tap((response: any) => {
          localStorage.setItem('jwt', response.token);  // Store JWT
          localStorage.setItem('userType', response.user.userType);  // Store user type
          if(response.user.userType === "customer"){
            localStorage.setItem('customerId', response.user.id);     // Store user ID (or customer ID)
          }
          else if(response.user.userType === "executive"){
            localStorage.setItem('executiveId', response.user.id);     // Store user ID (or customer ID)
          }
        })
      );
  }
  register(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData, { withCredentials: true });
  }

  logout(){
    localStorage.removeItem('jwt');
    if(localStorage.getItem('userType') === "customer"){
      localStorage.removeItem('customerId');  // Clear customer ID from localStorage
    }
    else{
      localStorage.removeItem('executiveId');  // Clear customer ID from localStorage
    }
    localStorage.removeItem('userType');

  }
  getCustomerId(): string | null {
    if(localStorage.getItem('customerId')){
      return localStorage.getItem('customerId');     // Store user ID (or customer ID)
    }
    else{
      return localStorage.getItem('executiveId');     // Store user ID (or customer ID)
    }
  }  
  isAuthenticated(): boolean {
    return !!localStorage.getItem('jwt');  // Check if the JWT exists
  }
  getUserRole(): string | null {
    return localStorage.getItem('userType');
  }
  
}