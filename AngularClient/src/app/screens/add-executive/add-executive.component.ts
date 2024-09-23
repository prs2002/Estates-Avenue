import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';  // Adjust path accordingly

@Component({
  selector: 'app-add-executive',
  templateUrl: './add-executive.component.html',
  styleUrls: ['./add-executive.component.css']
})
export class AddExecutiveComponent implements OnInit {
  addExecutiveForm!: FormGroup;
  showForm: boolean = false;

  constructor(private fb: FormBuilder, private authService: AuthService) {}

  ngOnInit(): void {
    this.addExecutiveForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      number: ['', Validators.required],
      location: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submit(): void {
    if (this.addExecutiveForm.valid) {
      const userData = {
        ...this.addExecutiveForm.value,
        userType: 'executive'  // Set userType to executive
      };

      this.authService.register(userData).subscribe({
        next: (response) => {
          console.log('Executive created successfully', response);
          this.closeForm(); // Close form after success
        },
        error: (err) => {
          console.error('Error creating executive', err);
        }
      });
    }
  }

  openForm(): void {
    this.showForm = true;
  }

  closeForm(): void {
    this.showForm = false;
  }
}