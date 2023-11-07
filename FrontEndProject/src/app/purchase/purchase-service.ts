import { Injectable } from "@angular/core";
import { CartService } from "../cart/cart.service";
import { CartItem } from "../cart/cart-item";
import { HttpClient } from "@angular/common/http";
import url from "src/utils/url";
import { map } from "rxjs";
import { PurchaseRequest } from "src/utils/interfaces";

@Injectable({
    providedIn: 'root',
})

export class PurchaseService{

    cartItems : CartItem[] = [];
    cartItemsId : {id : string, cant : number}[]= [];
    constructor(private cartService : CartService, private http : HttpClient){
    }

    addPurchase(cart : PurchaseRequest){
        return this.http.post(`${url}/purchases`, {cart},
        {observe: 'response'});
    }

    mapCartToList(): {id : string, cant : number}[]{
        return this.cartItems.map(cartItem => {
            return {id : cartItem.id, cant : cartItem.cant}
        })

    }


}

