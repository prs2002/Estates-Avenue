import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { CustReqService } from 'src/app/services/cust-req.service';
import { CustomerRequest } from 'src/app/models/CustomerRequest';

@Component({
  selector: 'app-exec-clients',
  templateUrl: './exec-clients.component.html',
  styleUrls: ['./exec-clients.component.css']
})
export class ExecClientsComponent implements OnInit {
  executiveId!: string;
  wishlist: any[] = [];

  constructor(private custReqService: CustReqService, private authService: AuthService) {}

  ngOnInit(): void {
    this.executiveId = this.authService.getCustomerId() || '';

    this.custReqService.getExecutiveRequests(this.executiveId).subscribe({
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