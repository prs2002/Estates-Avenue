import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-executives',
  templateUrl: './executives.component.html',
  styleUrls: ['./executives.component.css']
})
export class ExecutivesComponent implements OnInit {

  showAddExecutiveForm: boolean = false;
  newExecutive: User = {
    name: '',
    email: '',
    number: '',
    password: '',
    location: '',
    userType: 'executive'  // You want to default this to 'executive'
  };
  customers: User[] = [];  // Assuming you are listing executives

  constructor(private authService: AuthService,private userService: UserService) { }


  openForm(): void {
    console.log('Add New Executive button clicked');
    this.showAddExecutiveForm = !this.showAddExecutiveForm;
  }  
  createExecutive(): void {
    this.authService.register(this.newExecutive).subscribe({
      next: (response) => {
        console.log('Executive created successfully', response);
        this.customers.push(response); // Add new executive to the list
        this.showAddExecutiveForm = false; // Close the form after successful creation
      },
      error: (err) => {
        console.error('Failed to create executive', err);
      }
    });
  }
  closeForm(): void {
    this.showAddExecutiveForm = false;
  }
  ngOnInit(): void {
    this.loadExecutives();
  }

  loadExecutives(): void {
    this.userService.getExecutives().subscribe(
      (data) => {
        this.customers = data;
      },
      (error) => {
        console.error('Error fetching customers:', error);
      }
    );
  }

  deleteExecutive(email: string): void {
    if (confirm('Are you sure you want to delete this executive?')) {
      this.userService.deleteUserByEmail(email).subscribe(
        () => {
          this.loadExecutives(); // Refresh the list of executives after deletion
        },
        (error) => {
          console.error('Error deleting executive:', error);
        }
      );
    }
  }  

}