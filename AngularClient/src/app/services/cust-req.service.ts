import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, mergeMap } from 'rxjs/operators';
import { Observable,forkJoin } from 'rxjs';
import { CustomerRequest, Executive } from '../models/CustomerRequest';

@Injectable({
  providedIn: 'root'
})
export class CustReqService {

  private apiUrl = 'https://localhost:7033/api/CustomerRequest';
  private userApiUrl = 'https://localhost:7033/api/users/';

  constructor(private http: HttpClient) { 
  }
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwt');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  assignExecutive(requestId: string, executiveId: string): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.post(`${this.apiUrl}/${requestId}/assign-executive/${executiveId}`, null, { headers });
  }
  addToWishlist(customerRequest: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, customerRequest);
  }
  getAllCustomerRequests(): Observable<CustomerRequest[]> {
    return this.http.get<CustomerRequest[]>(`${this.apiUrl}/manager`).pipe(
      mergeMap(requests => {
        // For each request, if executiveId exists, fetch executive details
        const requestsWithExecutives$ = requests.map(req => {
          if (req.executiveId) {
            return this.http.get<Executive>(`${this.userApiUrl}${req.executiveId}`).pipe(
              map(execDetails => ({
                ...req,
                executiveDetails: execDetails // Add executive details to the request
              }))
            );
          } else {
            return [req]; // If no executive, return the request as is
          }
        });
        return forkJoin(requestsWithExecutives$); // Combine all observables into one
      })
    );
  }  
  getCustomerRequests(customerId: string): Observable<CustomerRequest[]> {
    return this.http.get<CustomerRequest[]>(`${this.apiUrl}/customer/${customerId}`).pipe(
      mergeMap(requests => {
        // For each request, if executiveId exists, fetch executive details
        const requestsWithExecutives$ = requests.map(req => {
          if (req.executiveId) {
            return this.http.get<Executive>(`${this.userApiUrl}${req.executiveId}`).pipe(
              map(execDetails => ({
                ...req,
                executiveDetails: execDetails // Add executive details to the request
              }))
            );
          } else {
            return [req]; // If no executive, return the request as is
          }
        });
        return forkJoin(requestsWithExecutives$); // Combine all observables into one
      })
    );
  }
  getExecutiveRequests(executiveId: string): Observable<CustomerRequest[]> {
    return this.http.get<CustomerRequest[]>(`${this.apiUrl}/executive/${executiveId}`).pipe(
      mergeMap(requests => {
        const requestsWithExecutives$ = requests.map(req => {
        return this.http.get<Executive>(`${this.userApiUrl}${req.customerId}`).pipe(
          map(custDetails => ({
            ...req,
            executiveDetails: custDetails // Add customer details to the request
          }))
        );
        });
        return forkJoin(requestsWithExecutives$); // Combine all observables into one
      })
    );
  }

}
