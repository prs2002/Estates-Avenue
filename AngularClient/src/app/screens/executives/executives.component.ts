import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-executives',
  templateUrl: './executives.component.html',
  styleUrls: ['./executives.component.css']
})
export class ExecutivesComponent implements OnInit {

  customers: User[] = [];

  constructor(private userService: UserService) { }

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

  deleteEmployee(id: string): void {
    if (confirm('Are you sure you want to delete this executives?')) {
      this.userService.deleteUser(id).subscribe(
        () => this.loadExecutives()
      );
    }
  }

}