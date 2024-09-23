import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustDashboardComponent } from './screens/cust-dashboard/cust-dashboard.component';
import { ExecDashboardComponent } from './screens/exec-dashboard/exec-dashboard.component';
import { LandingScreenComponent } from './screens/landing-screen/landing-screen.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './screens/login/login.component';
import { RegisterComponent } from './screens/register/register.component';
import { PropertiesComponent } from './screens/properties/properties.component';
import { CustomersComponent } from './screens/customers/customers.component';
import { ExecutivesComponent } from './screens/executives/executives.component';
import { WishlistComponent } from './screens/wishlist/wishlist.component';
import { CustRequestComponent } from './screens/cust-request/cust-request.component';
import { ExecClientsComponent } from './screens/exec-clients/exec-clients.component';
import { AssignExecutiveComponent } from './screens/assign-executive/assign-executive.component';
import { AddExecutiveComponent } from './screens/add-executive/add-executive.component';

@NgModule({
  declarations: [
    AppComponent,
    CustDashboardComponent,
    ExecDashboardComponent,
    LandingScreenComponent,
    FooterComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    PropertiesComponent,
    CustomersComponent,
    ExecutivesComponent,
    WishlistComponent,
    CustRequestComponent,
    ExecClientsComponent,
    AssignExecutiveComponent,
    AddExecutiveComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }