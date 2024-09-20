import { Component, HostListener, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  loginSuccess: boolean = false;
  loginFailed: boolean = false;
  showPassword: boolean = false;

  icon01 = faEyeSlash;
  icon02 = faEye;

  constructor(private router: Router, private renderer: Renderer2,private authService: AuthService) {}

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.updateStyles();
  }

  ngOnInit(): void {
    this.updateStyles();
  }

  login() {
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        const userType = response.user.userType;
        console.log('Login successful', response);
        this.loginSuccess = true;
        this.loginFailed = false;
          // Redirect based on userType
          if (userType === "customer") {
            console.log("navigating to " +response.user.userType+" dashboard");
            this.router.navigate(['/cust-dashboard']);
          } else {
            this.router.navigate(['/exec-dashboard']);
          }   
      },
      error: (err) => {
        console.error('Login failed', err);
        this.loginFailed = true;
        this.loginSuccess = false;
      }
    });
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  updateStyles() {
    const screenWidth = window.innerWidth;
    const element1 = document.querySelector('form');

    if (element1) {
      if (screenWidth >= 1800 && screenWidth <= 2560) {
        this.renderer.addClass(element1, 'signin-form-pad');
      } else {
        this.renderer.removeClass(element1, 'signin-form-pad');
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