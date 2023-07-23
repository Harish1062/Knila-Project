import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient ,HttpHeaders} from '@angular/common/http'
import { Router } from '@angular/router';
import { SharedService } from 'src/Services/shared.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  /**
   *
   */
  header:any=new HttpHeaders();
  loginForm:FormGroup=new FormGroup({
      email:new FormControl('',[Validators.required,Validators.email]),
      password:new FormControl('',[Validators.required,Validators.minLength(8)])
  });
  constructor(private fb:FormBuilder,private http: HttpClient,private router:Router,private sharedService:SharedService) {
      
  }


  // OnLogin(){
  //   if(this.loginForm.valid){
  //     this.http.get('http://localhost:7088/api/Login').subscribe(response => {
  //       // Handle the API response
  //       console.log(response);
  //     }, error => {
  //       // Handle any errors
  //       console.error(error);
  //     });
  //   }
  // }
  OnLogin(){
    if(this.loginForm.valid){
      this.header.set('Content-Type','application/json')
      this.http.post('http://localhost:7088/api/login/login',this.loginForm.value,this.header).subscribe((response:any)=>{
        // Handle the API response
        this.sharedService.IsLoggegIn=true;
        localStorage.setItem("Token",JSON.stringify(response));
        this.router.navigate(['/dashboard']);
      });
    }
  }
}
