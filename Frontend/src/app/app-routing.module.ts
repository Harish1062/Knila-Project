import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { AddcontactComponent } from './addcontact/addcontact.component';

const routes: Routes = [
  {
    path:'dashboard',
    component:DashboardComponent
  },
  {
    path:'contact',
    component:AddcontactComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'**',
    component:LoginComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
