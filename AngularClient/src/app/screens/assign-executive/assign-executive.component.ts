import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Executive } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';
import { CustReqService } from 'src/app/services/cust-req.service';

@Component({
  selector: 'app-assign-executive',
  templateUrl: './assign-executive.component.html',
  styleUrls: ['./assign-executive.component.css']
})
export class AssignExecutiveComponent implements OnInit {
  executives: Executive[] = [];
  locality!: string;
  propertyId!: string;
  requestId!: string;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private userService: UserService,
    private custReqService: CustReqService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.locality = this.route.snapshot.paramMap.get('locality') || '';
    this.propertyId = this.route.snapshot.paramMap.get('propertyId') || '';
    this.requestId = this.route.snapshot.paramMap.get('rid') || '';
    
    this.userService.fetchExecutivesByLocation(this.locality).subscribe({
      next: (executives) => {
        this.executives = executives;
      },
      error: (err) => {
        console.error('Failed to fetch executives', err);
      }
    });
  }

  assignExecutive(executiveId: string): void {
    this.custReqService.assignExecutive(this.requestId, executiveId).subscribe({
      next: () => {
        console.log('Executive assigned successfully');
        this.router.navigate(['/cust-reqs']);
      },
      error: (err) => {
        console.error('Failed to assign executive', err);
      }
    });
  }
}
