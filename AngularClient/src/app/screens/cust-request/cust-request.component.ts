import { Component, OnInit } from '@angular/core';
import { CustReqService } from 'src/app/services/cust-req.service';
import { CustomerRequest } from 'src/app/models/CustomerRequest';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cust-request',
  templateUrl: './cust-request.component.html',
  styleUrls: ['./cust-request.component.css']
})
export class CustRequestComponent implements OnInit {
  wishlist: CustomerRequest[] = [];

  constructor(private router: Router,private custReqService: CustReqService) {}

  ngOnInit(): void {
    this.custReqService.getAllCustomerRequests().subscribe({
      next: (response) => {
        this.wishlist = response;
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