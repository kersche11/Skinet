import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {
   baseUrl = environment.apiUrl;
   validationErrors:string[]=[];
   

   constructor(private htpp:HttpClient){
    console.log(this.baseUrl);
   }

   get404Error(){
    this.htpp.get(this.baseUrl+'products/42').subscribe({
      next:response=>console.log(response),
      error: error=>console.error(error)  
    })
   }
   get500Error(){
    this.htpp.get(this.baseUrl+'buggy/servererror').subscribe({
      next:response=>console.log(response),
      error: error=>console.error(error)  
    })
   }
   get400Error(){
    this.htpp.get(this.baseUrl+'buggy/badrequest').subscribe({
      next:response=>console.log(response),
      error: error=>console.error(error)  
    })
   }
   get400ValidationError(){
    this.htpp.get(this.baseUrl+'products/stringError').subscribe({
      next:response=>console.log(response),
      error: error=>{console.error(error),
      this.validationErrors = error.errors;  }
    })
   }
  }
