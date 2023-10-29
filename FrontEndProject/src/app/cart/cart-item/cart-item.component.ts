import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartItem } from '../cart-item';
import {MatRippleModule} from '@angular/material/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css'],
  standalone: true,
  imports: [MatRippleModule]
})
export class CartItemComponent implements OnInit{
  centered = false;
  disabled = false;
  unbounded = false;
  @Input()
  cartItem!: CartItem;

  @Output()
  cartItemDelete = new EventEmitter<void>();
  constructor(private router : Router){
  }
  ngOnInit(): void {
    console.log(this.cartItem);
  }
  onDeleteClick():void {
    this.cartItemDelete.emit();
  }
  onCardClick(id : number){
    const url = 'products/' + id;
    this.router.navigate([url]);
  }
}
