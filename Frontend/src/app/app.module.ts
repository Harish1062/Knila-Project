import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TokenInterceptor } from 'src/Services/common.interceptor';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { AddcontactComponent } from './addcontact/addcontact.component';
import { SharedService } from 'src/Services/shared.service';
import { NavbarComponent } from './navbar/navbar.component';
import { DecimalPipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    AddcontactComponent,
    NavbarComponent
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgbModule,
    HttpClientModule,
    NgbPaginationModule,
    DecimalPipe
    
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    SharedService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
