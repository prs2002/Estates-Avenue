import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  customers: User[] = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.userService.getCustomers().subscribe(
      (data) => {
        this.customers = data;
      },
      (error) => {
        console.error('Error fetching customers:', error);
      }
    );
  }


  deleteCustomer(email: string): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.userService.deleteUserByEmail(email).subscribe(
        () => {
          this.loadCustomers(); // Refresh the list of executives after deletion
        },
        (error) => {
          console.error('Error deleting customer:', error);
        }
      );
    }
  }  

}