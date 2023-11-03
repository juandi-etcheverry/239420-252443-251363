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
    private cartKey: string = 'AnonymusCart';
    items: CartItem[] = [];
    isLoggedIn : boolean = this.authService.hasAuthToken();
    user : User | null = null;

    constructor(private authService: AuthService, private userService : UsersService, private router : Router){
        router.events.subscribe((event) => {
            this.isLoggedIn = this.authService.hasAuthToken();
            this.updatecartKey();
            if (this.user === null && this.isLoggedIn) {
              this.fetchUserData(() => {
                this.updatecartKey();
              });
            }
          })
          this.loadCartFromLocalStorage();
    }

    updatecartKey(){
        this.cartKey = this.user?.id ?? 'AnonymusCart';
    }
    get itemsCount(): number{
        return this.items.length;
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
        this.cartKey = 'AnonymusCart';
        this.loadCartFromLocalStorage();
    }

    deleteItem(ItemToDelete : CartItem){
        this.items = this.items.filter((item)=> item != ItemToDelete);
        this.saveCartToLocalStorage();
    }
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
    decreaseItem(item : CartItem){
        if(this.items.find((i)=> i.id == item.id)){
            this.items = this.items.map((i)=>{
                if(i.id == item.id){
                    i.cant--; 
                }
                return i;
            })
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

}