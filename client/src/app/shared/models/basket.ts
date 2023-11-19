import {createId} from '@paralleldrive/cuid2';

export interface Basket {
    id: string;
    items: BasketItem[];
  }
  
  export interface BasketItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
  }

  export class Basket implements Basket{
    id=createId();
    items: BasketItem[]=[];
  }
  export interface BasketTotals {
    shipping:number;
    subtotal: number;
    total:number;
  }