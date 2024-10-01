import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { CustReqService } from 'src/app/services/cust-req.service';
import { switchMap, map } from 'rxjs/operators';
import { forkJoin } from 'rxjs';
import { CustomerRequest } from 'src/app/models/CustomerRequest';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-exec-clients',
  templateUrl: './exec-clients.component.html',
  styleUrls: ['./exec-clients.component.css']
})
export class ExecClientsComponent implements OnInit {
  executiveId!: string;
  wishlist: any[] = [];

  constructor(private custReqService: CustReqService, private authService: AuthService, private http: HttpClient) {}

  ngOnInit(): void {
    this.executiveId = this.authService.getCustomerId() || '';

    // this.custReqService.getExecutiveRequests(this.executiveId).subscribe({
    //   next: (response) => {
    //     this.wishlist = response;
    //     console.log(this.wishlist);
    //   },
    //   error: (err) => {
    //     console.error('Failed to fetch wishlist', err);
    //   }
    // });

    this.custReqService.getExecutiveRequests(this.executiveId).pipe(
      switchMap((requests: CustomerRequest[]) => {
        const requestDetails = requests.map(request => {
          const customer$ = this.http.get<any>(`https://localhost:7033/api/users/${request.customerId}`);
    
          return customer$.pipe(
            map(customerDetails => ({
              ...request,
              customerDetails: { // Attach the entire customer details
                name: customerDetails.name,
                email: customerDetails.email,
                number: customerDetails.number
              }
            }))
          );
        });
    
        return forkJoin(requestDetails); // Combine all the requests into one observable
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

}