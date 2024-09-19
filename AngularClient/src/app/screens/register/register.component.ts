import { Component, OnInit, HostListener, Renderer2 } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { User } from '../../models/User'; 
import { AuthService } from 'src/app/services/auth.service';

function passwordMatchValidator(control: AbstractControl) {
  const password = control.get('password')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;
  return password === confirmPassword ? null : { mismatch: true };
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  signupForm!: FormGroup;
  formSubmitted: boolean = false;
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  icon01 = faEyeSlash;
  icon02 = faEye;

  constructor(private formBuilder: FormBuilder, private router: Router, private authService: AuthService,private renderer: Renderer2) {}

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.updateStyles();
  }

  ngOnInit(): void {
    this.signupForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, this.passwordValidator]],
      confirmPassword: ['', Validators.required],
      number: ['', Validators.required],
      location: ['', Validators.required],
      userType: ['user'] // Default value set to "user"
    }, { validators: passwordMatchValidator });

    this.signupForm.get('password')?.valueChanges.subscribe(() => {
      this.signupForm.get('confirmPassword')?.updateValueAndValidity();
    });

    this.updateStyles();
  }

  passwordValidator(control: AbstractControl) {
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    const isValid = passwordRegex.test(control.value);
    if (!isValid) {
      return { invalidPassword: true };
    }
    return null;
  }

  onSubmit() {
    this.formSubmitted = true;
    if (this.signupForm.valid) {
      const newUser: User = {
        name: this.signupForm.value.name,
        email: this.signupForm.value.email,
        password: this.signupForm.value.password,
        number: this.signupForm.value.number,
        location: this.signupForm.value.location,
        userType: 'user' // Ensure that userType is set to "user"
      };
      this.authService.register(newUser).subscribe({
        next: (response) => {
          console.log('Registration successful', response);
          // Redirect to the login page after successful registration
          this.router.navigate(['/signin']);
        },
        error: (err) => {
          console.error('Registration failed', err);
        }
      });
    }
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  toggleShowConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  updateStyles() {
    const screenWidth = window.innerWidth;
    const element1 = document.querySelector('form');

    if (element1) {
      if (screenWidth >= 1800 && screenWidth <= 2560) {
        this.renderer.addClass(element1, 'signup-form-pad');
      } else {
        this.renderer.removeClass(element1, 'signup-form-pad');
      }
    }

    if (element1) {
      if (screenWidth >= 1800 && screenWidth <= 2560) {
        this.renderer.addClass(element1, 'form-pad-top');
      } else {
        this.renderer.removeClass(element1, 'form-pad-top');
      }
    }
  }
}