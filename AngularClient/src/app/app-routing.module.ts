import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingScreenComponent } from './screens/landing-screen/landing-screen.component';
import { RegisterComponent } from './screens/register/register.component';
import { LoginComponent } from './screens/login/login.component';
import { CustDashboardComponent } from './screens/cust-dashboard/cust-dashboard.component';
import { ExecDashboardComponent } from './screens/exec-dashboard/exec-dashboard.component';
import { PropertiesComponent } from './screens/properties/properties.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: 'home', component: LandingScreenComponent },
  { path: 'signin', component: LoginComponent },
  { path: 'signup', component: RegisterComponent },
  { path: 'cust-dashboard', component: CustDashboardComponent },
  { path: 'exec-dashboard', component: ExecDashboardComponent },
  { path: 'properties', component: PropertiesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }