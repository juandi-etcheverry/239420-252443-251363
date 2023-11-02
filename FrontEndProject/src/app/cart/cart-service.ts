import { Injectable } from "@angular/core";
import { CartItem } from "./cart-item";
import { Product } from "src/utils/interfaces";


@Injectable({
    providedIn: 'root',
})
export class CartService{

    items: CartItem[] = [];

    get itemsCount(): number{
        return this.items.length;
    }
    deleteItem(ItemToDelete : CartItem){
        this.items = this.items.filter((item)=> item != ItemToDelete);
    }
    addItem(item : CartItem){
        this.items = [...this.items, item];
    }
    mapProductItemToCartItem(product: Product): CartItem {
        const cartItem: CartItem = {
          id: product.id,
          name: product.name,
          price: product.price,
        };
        return cartItem;
      }
    get total():number{
        return this.items.reduce(( acc, {price} ) => (acc += price), 0)
    }

}