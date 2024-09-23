import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { CustReqService } from 'src/app/services/cust-req.service';
import { CustomerRequest } from 'src/app/models/CustomerRequest';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  customerId!: string;
  wishlist: CustomerRequest[] = [];

  constructor(private custReqService: CustReqService, private authService: AuthService) {}

  ngOnInit(): void {
    this.customerId = this.authService.getCustomerId() || '';

    this.custReqService.getCustomerRequests(this.customerId).subscribe({
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