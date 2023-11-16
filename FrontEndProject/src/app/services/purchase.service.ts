import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import url from "src/utils/url";
import { CartItem, GetAllPurchasesResponse, PurchaseRequest } from "src/utils/interfaces";
import { CartService } from "../components/cart/cart.service";

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

    getPurchases(id ?: string) {
        return this.http.get<GetAllPurchasesResponse>(`${url}/purchases${id? '/'.concat(id) : ''}`)
    }


}

