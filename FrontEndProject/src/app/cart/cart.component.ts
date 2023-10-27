import { Component, OnInit } from '@angular/core';
import { CartItemComponent } from './cart-item/cart-item.component';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { CartItem } from './cart-item';
import { CartService } from './cart-service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDividerModule, MatIconModule, CartItemComponent]
})
export class CartComponent implements OnInit{

  cartItems: CartItem[] = this.cartService.items;

  constructor(private cartService : CartService){
}
  ngOnInit(): void {
  }
  deleteItem(ItemToDelete : CartItem) : void{
    this.cartItems = this.cartItems.filter((item)=> item.id != ItemToDelete.id);
    //pasarlo al service
  }

  get total():number{
    return this.cartItems.reduce(( acc, {price} ) => (acc += price), 0)
        //pasarlo al service
  }
}
