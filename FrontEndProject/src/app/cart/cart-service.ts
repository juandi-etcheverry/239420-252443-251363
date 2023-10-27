import { Injectable } from "@angular/core";
import { CartItem } from "./cart-item";


@Injectable({
    providedIn: 'root',
})
export class CartService{

    items: CartItem[] = [
        {id:1, name: "Product1", price: 30},
        {id:2, name: "Product2", price: 40},
        {id:3, name: "Product3", price: 50},
        {id:4, name: "Product4", price: 60},
      ];

    deleteItem(ItemToDelete : CartItem) : void{
        this.items = this.items.filter((item)=> item.id != ItemToDelete.id);
    }
    
    get total():number{
        return this.items.reduce(( acc, {price} ) => (acc += price), 0)
    }
}