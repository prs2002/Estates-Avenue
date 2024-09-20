import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class authGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: any): boolean {
    const isAuthenticated = this.authService.isAuthenticated();
    const userRole = this.authService.getUserRole();

    // Check if the user is authenticated
    if (!isAuthenticated) {
      this.router.navigate(['/signin']);
      return false;
    }

    // Check if the route requires a specific role
    if (route.data && route.data.roles && !route.data.roles.includes(userRole)) {
      // If the user's role is not allowed, redirect them
      this.router.navigate(['/home']);
      return false;
    }

    return true;
  }
}