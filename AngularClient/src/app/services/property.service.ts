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

  createProperty(property: Property, image: string): Observable<any> {
    const headers = this.getAuthHeaders();
    const body = {
      ...property,
      imageUrl: image // Include the image URL in the request body
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
  // createProperty(property: Property, imageFile: File): Observable<any> {
  //   const formData = new FormData();
  //   formData.append('name', property.name);
  //   formData.append('location', property.location);
  //   formData.append('rate', property.rate.toString());
  //   formData.append('propertyType', property.propertyType);
  //   formData.append('desc', property.desc);
  //   formData.append('executiveId', property.executiveId);
  //   formData.append('customerId', property.customerId);
  //   formData.append('imageFile', imageFile); // Append image file to the form data

  //   return this.http.post(this.apiUrl, formData);
  // }

}
