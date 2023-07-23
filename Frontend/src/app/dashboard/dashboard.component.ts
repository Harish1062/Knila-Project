import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component,Directive,EventEmitter,Input,OnInit, Output } from '@angular/core';
import { UserListBO } from './UserDTO';
import { Router } from '@angular/router';
import { SharedService } from 'src/Services/shared.service';
import * as alertifyjs from 'alertifyjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  /**
   *
   */
/**
 *
 */



  constructor(private http:HttpClient,private router:Router,private sharedService:SharedService) {
   
    
  }
  currentPage = 1;
  collectionsize=0;
  pageSize = 10;
  sortProperty: string = 'id';
  sortOrder = true;
  user:Array<UserListBO>=new Array<UserListBO>();
  ngOnInit(){
    this.getContacts();
  }

  getPageCount(){
    return Math.ceil(this.collectionsize/this.pageSize)
  }
  getContacts(){
    this.http.post("http://localhost:7088/api/Contact/get-contact?page="+ this.currentPage+"&size="+this.pageSize,{}).subscribe((response:any)=>{
      this.user=response.contacts;
      this.collectionsize=response.count;
    })
  }

  editItem(item : any) {
    this.sharedService.setSharedObject(item);
    this.router.navigate(['/contact']);
  }

  deleteItem(item : any) {
    var _this=this;
    alertifyjs.confirm('Delete Contact', 'Are you sure want to delete?', function(){ 
      _this.http.post("http://localhost:7088/api/Contact/delete-contact?id="+item.id,{}).subscribe((response:any)=>{
      alertifyjs.success(response.message);
      _this.getContacts();
    })
    }
    , function(){
       
      });
  }



  sortBy(property: string) {
    this.sortOrder = !this.sortOrder;
    this.sortProperty = property;

    this.http.post("http://localhost:7088/api/Contact/get-contact?page="+ this.currentPage+"&size="+this.pageSize+"&isAsc="+this.sortOrder+"&column="+property,{}).subscribe((response:any)=>{
      this.user=response.contacts;
      this.collectionsize=response.count*2;
    })

    // this.user = [...this.user.sort((a: any, b: any) => {
    //     // sort comparison function
    //     let result = 0;
    //     if (a[property] < b[property]) {
    //         result = -1;
    //     }
    //     if (a[property] > b[property]) {
    //         result = 1;
    //     }
    //     return result * this.sortOrder;
    // })];
}

sortIcon(property: string) {
    if (property === this.sortProperty) {
        return this.sortOrder === true ? '☝️' : '👇';
    }
    return '';
}
}

