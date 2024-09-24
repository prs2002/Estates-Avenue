import { Component, OnInit } from '@angular/core';
import { CustReqService } from 'src/app/services/cust-req.service';
import { CustomerRequest } from 'src/app/models/CustomerRequest';
import { Router } from '@angular/router';
import { switchMap, map } from 'rxjs/operators';
import { forkJoin } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-cust-request',
  templateUrl: './cust-request.component.html',
  styleUrls: ['./cust-request.component.css']
})
export class CustRequestComponent implements OnInit {
  customerId!: string;
  wishlist: any[] = [];
  constructor(private router: Router,private custReqService: CustReqService,private http: HttpClient) {}

  ngOnInit(): void {

    this.custReqService.getAllCustomerRequests().pipe(
      switchMap((requests: CustomerRequest[]) => {
        const requestDetails = requests.map(request => {
          const customer$ = this.http.get<any>(`https://localhost:7033/api/users/${request.customerId}`);
          const property$ = this.http.get<any>(`https://localhost:7033/api/Property/${request.propertyId}`);
          
          return forkJoin([customer$, property$]).pipe(
            map(([customerDetails, propertyDetails]) => ({
              ...request,
              customerName: customerDetails.name,
              propertyName: propertyDetails.name
            }))
          );
        });
        return forkJoin(requestDetails);
      })
    ).subscribe({
      next: (response) => {
        this.wishlist = response;
        console.log(this.wishlist);
      },
      error: (err) => {
        console.error('Failed to fetch wishlist', err);
      }
    });
  }
  assignExecutive(request: CustomerRequest) {
    this.router.navigate(['/assign-executive', request.locality, request.propertyId, request.rid]);
  }

}