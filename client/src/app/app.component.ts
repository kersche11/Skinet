import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  //Title
  title = 'Skinet';
 

  //Constructor
  constructor(private basketService:BasketService){}   //inject the htttpCLient here

  //Livecycle Hooks
  ngOnInit(): void 
  {
    //Check if a basketId exists. If yes load the Basket
    const basketId = localStorage.getItem("basket_id");
    if(basketId) this.basketService.getBasket(basketId);
  }
}
