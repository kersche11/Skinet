import { InterpolationConfig } from "@angular/compiler";

let data1:any;
let data2:number;
let data3:string;
let data4:number|string;

data1 ="test";

data2=2;

data3="string";

data4=4;
data4="vier"


interface ICar {
    color:string;
    speed?:number;
}


const car1:ICar ={
    color:'red'

}

const car2:ICar ={
    color:'blue',
    speed: 100
}
const multiply=(x:number, y:number):number=>{
    return x*y;
};

const multiply1=(x:number, y:number):void=>{
    x*y;
};

const multiply2=(x:number, y:number):string=>{
    return (x*y).toString();
};
