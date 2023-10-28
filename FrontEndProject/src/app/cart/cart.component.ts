import { Component, OnInit } from '@angular/core';
import { CartItemComponent } from './cart-item/cart-item.component';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { CartItem } from './cart-item';
import { CartService } from './cart-service';
import { CommonModule } from '@angular/common';
import { PaymentmethodComponent } from './paymentmethod/paymentmethod.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  standalone: true,
  imports: [PaymentmethodComponent, MatButtonModule, MatDividerModule, MatIconModule, CartItemComponent, CommonModule]
})
export class CartComponent implements OnInit{

  cartItems: CartItem[] = this.cartService.items;

  constructor(private cartService : CartService){
}
  ngOnInit(): void {
  }

  deleteItem(ItemToDelete : CartItem) : void{
    this.cartService.deleteItem(ItemToDelete);
    this.cartItems = this.cartService.items;
  }

  get total():number{
    return this.cartService.total;
  }
}
