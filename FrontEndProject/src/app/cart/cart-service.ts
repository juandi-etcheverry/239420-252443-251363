import { Injectable } from "@angular/core";
import { CartItem } from "./cart-item";
import { Product } from "src/utils/interfaces";
import { AuthService } from "../auth.service";
import { UsersService } from "../user/users.service";
import { User } from "../user/user-model";
import { Router } from "@angular/router";


@Injectable({
    providedIn: 'root',
})
export class CartService{
    public cartKey: string = 'AnonymousCart';
    items: CartItem[] = [];
    isLoggedIn : boolean = this.authService.hasAuthToken();
    user : User | null = null;

    constructor(private authService: AuthService, private userService : UsersService, private router : Router){
        this.fetchUserData(() => {
            this.sayHello();
          });
    }

    signIn(){
        this.fetchUserData(() => {
            this.mergeInOne();
          });
    }

    mergeInOne(){
        const sourceKey: string = 'AnonymousCart';
        const targetKey: string = this.user?.id ?? 'AnonymousCart';
        const sourceCartItems: CartItem[] = JSON.parse(localStorage.getItem(sourceKey) || '[]');
        const targetCartItems: CartItem[] = JSON.parse(localStorage.getItem(targetKey) || '[]');

        sourceCartItems.forEach((sourceItem) => {
            const existingItem = targetCartItems.find(targetItem => targetItem.id === sourceItem.id);
            if(existingItem){
                existingItem.cant += sourceItem.cant;
            } else{
                targetCartItems.push(sourceItem);
            }
        });
        localStorage.setItem(targetKey, JSON.stringify(targetCartItems));
        localStorage.removeItem(sourceKey);
        this.loadCartFromLocalStorage();
    }

    get itemsCount(): number{
        return this.items.length;
    }
    removeCartId(id : string){
        localStorage.removeItem(id);
    }
    
    saveCartToLocalStorage(){
        localStorage.setItem(this.cartKey, JSON.stringify(this.items));
    }
    loadCartFromLocalStorage(){
        const cartItemsStr = localStorage.getItem(this.cartKey);
        if(cartItemsStr){
            this.items = JSON.parse(cartItemsStr);
        }
    }
    logout(){
        this.user = null;
        this.cartKey = 'AnonymousCart';
        localStorage.setItem(this.cartKey, JSON.stringify([]));
        this.loadCartFromLocalStorage();
    }

    /*deleteItem(ItemToDelete : CartItem){
        this.items = this.items.filter((item)=> item != ItemToDelete);
        this.saveCartToLocalStorage();
    }*/
    addItem(item : CartItem){
        if(this.items.find((i)=> i.id == item.id)){
            this.items = this.items.map((i)=>{
                if(i.id == item.id){
                    i.cant++;
                }
                return i;
            })
        }
        else{
            this.items = [...this.items, item];
        }
        this.saveCartToLocalStorage();
    }
    /*decreaseItem(item : CartItem){
        if(this.items.find((i)=> i.id == item.id)){
            this.items = this.items.map((i)=>{
                if(i.id == item.id){
                    i.cant--; 
                }
                return i;
            })
        }
        this.saveCartToLocalStorage();
    }*/

    decreaseItem(itemToDecrease : CartItem){
        this.loadCartFromLocalStorage();
        for (let item of this.items){
            if(itemToDecrease.id == item.id){
                item.cant--;
                if(item.cant == 0){
                    this.items = this.items.filter((item)=> item.id != itemToDecrease.id);
                }
            }
        }
        this.saveCartToLocalStorage();
    }

    mapProductItemToCartItem(product: Product): CartItem {
        const cartItem: CartItem = {
          id: product.id,
          name: product.name,
          price: product.price,
          cant: 1,
        };
        return cartItem;
      }
    get total():number{
        return this.items.reduce(( acc, {price} ) => (acc += price), 0)
    }
    getCantOfItem(id : string) : number{
        this.loadCartFromLocalStorage();
        if(this.items.find((item)=> item.id == id)){
            return this.items.find((item)=> item.id == id)!.cant;
        }
        else{
            return 0;
        }
    }
    private fetchUserData(internalSubscribeLogic : () => void) {
        return this.userService.getLoggedUser()?.subscribe(({id, email, address, role}) => {
          this.user = {
            id, email, address, role
          }
          this.cartKey = this.user.id;
          internalSubscribeLogic();
        })
      }
    private sayHello(){
    }

}