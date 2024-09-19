import { Component,HostListener } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  navbarColor: string = 'transparent';
  isMobileView: boolean = false;
  isMenuOpen: boolean = false;
  isLoggedIn: boolean = false;  // Tracks login status

  constructor(private authService: AuthService, private router: Router) {}

  // ngOnInit(): void {
  //   // Check if the user is logged in when the component is initialized
  //   this.authService.isAuthenticated().subscribe((response) => {
  //     this.isLoggedIn = response.isAuthenticated;  // Use the isAuthenticated value from the response
  //   });
  // }

  ngOnInit(): void {
    // Call the check on initialization
    this.checkAuthentication();

    // Subscribe to router events to trigger check when route changes
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd) // Only trigger on NavigationEnd event
    ).subscribe(() => {
      this.checkAuthentication();
    });
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll() {
    const scrollPosition = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    if (scrollPosition > 0) {
      this.navbarColor = 'black';
    } else {
      this.navbarColor = 'transparent';
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.isMobileView = window.innerWidth < 768;
    if (!this.isMobileView) {
      this.isMenuOpen = false;
    }
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
    if (this.isMenuOpen) {
      this.navbarColor = 'rgba(0, 0, 0, 0.7)';
    } else {
      this.navbarColor = window.pageYOffset > 0 ? 'rgba(0, 0, 0, 0.7)' : 'transparent';
    }
  }

  checkAuthentication() {
    this.authService.isAuthenticated().subscribe((response) => {
      console.log('Auth Response:', response); // Debugging the API response
      this.isLoggedIn = response.isAuthenticated;
      console.log('Is Logged In:', this.isLoggedIn); // Check if this is correctly updated
    });
  }

  logout() {
    this.authService.logout().subscribe({
      next: (response) => {
        console.log(response);  // Optional: You can log the response for debugging
        this.isLoggedIn = false;
        this.router.navigate(['/signin']);  // Redirect to login page
      },
      error: (err) => {
        console.error('Logout failed', err);  // Handle errors if needed
      }
    });
  }

}
