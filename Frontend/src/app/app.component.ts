import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from 'src/Services/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Crud_Angular_App';
  /**
   *
   */
  constructor(public sharedService:SharedService,private http:HttpClient,private router:Router) {
   
    
  }
token:String="";
  ngOnInit(){
    this.token=JSON.parse(localStorage.getItem('Token')+"").data;
      if(this.token){
        this.http.post("http://localhost:7088/api/login/check-token-valid?token="+this.token,{}).subscribe((res:any)=>{
          if(res==true){
            this.sharedService.IsLoggegIn=true;
            this.router.navigate(['/dashboard']);
          }
          else{
            this.sharedService.IsLoggegIn=false;
            localStorage.clear();
            this.router.navigate(['/login']);
          }
        })
      }
  }

}
