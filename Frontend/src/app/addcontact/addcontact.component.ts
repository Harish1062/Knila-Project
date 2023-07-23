import { HttpClient } from '@angular/common/http';
import { Component ,Input,OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { SharedService } from 'src/Services/shared.service';
import * as alertifyjs from 'alertifyjs';

@Component({
  selector: 'app-addcontact',
  templateUrl: './addcontact.component.html',
  styleUrls: ['./addcontact.component.css']
})
export class AddcontactComponent {

  // ContactForm!: FormGroup;
  fb!: FormBuilder;
  receivedObject:any;
  /**
   *
   */

  constructor(private router:Router,private sharedService:SharedService,private http:HttpClient) {
  
  }

  ContactForm:FormGroup=new FormGroup({
    id:new FormControl(0),
    firstName:new FormControl('',[Validators.required]),
    lastName:new FormControl('',[Validators.required,]),
    email:new FormControl('',[Validators.required]),
    phoneNumber:new FormControl('',[Validators.required,]),
    address:new FormControl('',[Validators.required]),
    city:new FormControl('',[Validators.required,]),
    state:new FormControl('',[Validators.required]),
    country:new FormControl('',[Validators.required,]),
    postalCode:new FormControl('',[Validators.required]),
});

  ngOnInit(){
    
this.ContactForm.patchValue(this.sharedService.getSharedObject());

    console.log(this.sharedService.getSharedObject());


   

    // this.router.paramMap.subscribe(params => {
    //   const id = params.get('id');
    //   const data = params.get('data');
    //   this.receivedObject = data ? JSON.parse(data) : null;
    //   console.log(this.receivedObject);
    // });
  }
  AddContact(){
    if(this.ContactForm.valid){
      console.log(this.ContactForm.value);
      this.http.post("http://localhost:7088/api/Contact/add-contact",this.ContactForm.value).subscribe((response:any)=>{
      this.router.navigate(["/dashboard"]);
    })
    }    
  }

  ngOnDestroy(){
    
  }

}
