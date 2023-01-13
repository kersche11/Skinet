import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  //Title
  title = 'Skinet';
 

  //Constructor
  constructor(){}   //inject the htttpCLient here

  //Livecycle Hooks
  ngOnInit(): void 
  {
    
  }
}
