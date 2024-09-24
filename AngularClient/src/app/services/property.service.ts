import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property } from '../models/Property'; // Define this model
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {

  private apiUrl = 'https://localhost:7033/api/Property'; // Change to your backend URL

  constructor(private http: HttpClient, private authService: AuthService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
  getProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.apiUrl}`);
  }

  createProperty(property: Property): Observable<any> {
    const headers = this.getAuthHeaders();
    const body = {
      ...property
    };
    return this.http.post<Property>(this.apiUrl, body, { headers });
  }
  updateProperty(property: Property): Observable<Property> {
    const headers = this.getAuthHeaders();
    return this.http.put<Property>(`${this.apiUrl}/${property.pid}`, property, { headers });
  }
  
  deleteProperty(propertyId: number): Observable<void> {
    const headers = this.getAuthHeaders();
    return this.http.delete<void>(`${this.apiUrl}/${propertyId}`, { headers });
  }  

}
