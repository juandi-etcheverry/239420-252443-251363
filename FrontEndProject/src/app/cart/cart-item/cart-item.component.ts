import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartItem } from '../cart-item';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css'],
  standalone: true
})
export class CartItemComponent implements OnInit{

  @Input()
  cartItem!: CartItem;

  @Output()
  cartItemDelete = new EventEmitter<void>();
  constructor(){
  }
  ngOnInit(): void {
    console.log(this.cartItem);
  }
  onDeleteClick():void {
    this.cartItemDelete.emit();
  }
}
