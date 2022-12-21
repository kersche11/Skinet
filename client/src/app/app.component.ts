import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {IProduct} from './models/product'
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  //Title
  title = 'Skinet';
  products: IProduct[];

  //Constructor
  constructor(private http: HttpClient){}   //inject the htttpCLient here

  //Livecycle Hooks
  ngOnInit(): void 
  {
    this.http.get('https://localhost:5001/api/products?pageSize=10').subscribe((response:IPagination)=>{
      this.products=response.data      //console.log(response);
    }, 
    error=>{
      console.log(error);
    });
  }
}
