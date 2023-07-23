import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, finalize, of, tap, throwError } from "rxjs";
import * as alertifyjs from 'alertifyjs';
import { Router } from "@angular/router";
import { SharedService } from "./shared.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private router:Router,private sharedService:SharedService) { }
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    console.log('test')
      const token =  localStorage.getItem("Token") && localStorage.getItem("Token")!=null ? JSON.parse(localStorage.getItem("Token")+"").data :'';
      req = req.clone({
      headers: req.headers.set(
          "Authorization",
          "Bearer " + token 
      )      
      });

  return next.handle(req).pipe(
    catchError((error:HttpErrorResponse):Observable<HttpEvent<any>>=>{
      if(error.status!=200 && error.status!=204){
        if(error.status==401){
          this.sharedService.IsLoggegIn=false;
          this.router.navigate(['/login']);
          localStorage.clear();
          alertifyjs.error("Session expired..");
        }
        else{
           alertifyjs.error(error.message);
        }
        return throwError('Something went wrong');
      }
      return throwError('');
  }))
}
}
