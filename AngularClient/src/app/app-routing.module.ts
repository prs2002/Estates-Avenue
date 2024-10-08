import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingScreenComponent } from './screens/landing-screen/landing-screen.component';
import { RegisterComponent } from './screens/register/register.component';
import { LoginComponent } from './screens/login/login.component';
import { CustDashboardComponent } from './screens/cust-dashboard/cust-dashboard.component';
import { ExecDashboardComponent } from './screens/exec-dashboard/exec-dashboard.component';
import { PropertiesComponent } from './screens/properties/properties.component';
import { authGuard } from './guards/auth.guard';
import { CustomersComponent } from './screens/customers/customers.component';
import { ExecutivesComponent } from './screens/executives/executives.component';
import { WishlistComponent } from './screens/wishlist/wishlist.component';
import { ExecClientsComponent } from './screens/exec-clients/exec-clients.component';
import { CustRequestComponent } from './screens/cust-request/cust-request.component';
import { AssignExecutiveComponent } from './screens/assign-executive/assign-executive.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: 'home', component: LandingScreenComponent },
  { path: 'signin', component: LoginComponent },
  { path: 'signup', component: RegisterComponent },
  { path: 'wishlist', component: WishlistComponent, canActivate: [authGuard], data: { roles: 'customer' } },
  { path: 'cust-dashboard', component: CustDashboardComponent, canActivate: [authGuard], data: { roles: 'customer' } },
  { path: 'cust-reqs', component: CustRequestComponent,canActivate: [authGuard], data: { roles: ['manager'] } },
  { path: 'assign-executive/:locality/:propertyId/:rid', component: AssignExecutiveComponent },
  { path: 'exec-dashboard', component: ExecDashboardComponent,canActivate: [authGuard], data: { roles: ['manager','executive'] } },
  { path: 'exec-clients', component: ExecClientsComponent,canActivate: [authGuard], data: { roles: ['executive'] } },
  { path: 'properties', component: PropertiesComponent,canActivate: [authGuard], data: { roles: ['manager','customer'] } },
  { path: 'customers', component: CustomersComponent,canActivate: [authGuard], data: { roles: ['manager','executive'] } },
  { path: 'executives', component: ExecutivesComponent,canActivate: [authGuard], data: { roles: ['manager'] } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }