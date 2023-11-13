import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartItem } from "src/utils/interfaces";
import {MatRippleModule} from '@angular/material/core';
import { Route, Router } from '@angular/router';
import { CartService } from '../cart.service';
import { MatButton, MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css'],
  standalone: true,
  imports: [MatRippleModule, MatButtonModule]
})
export class CartItemComponent implements OnInit{
  centered = false;
  disabled = false;
  unbounded = false;
  @Input()
  cartItem!: CartItem;
  cant : number = 0;

  @Output() changeItemCant = new EventEmitter<void>();


  constructor(private router : Router, private cartService : CartService){
  }
  ngOnInit(): void {
    this.cant = this.cartService.getCantOfItem(this.cartItem.id);
  }
  onDeleteClick($event : Event):void {
    $event.stopPropagation()
    this.cartService.decreaseItem(this.cartItem);
    this.cant = this.cartService.getCantOfItem(this.cartItem.id);
    this.changeItemCant.emit();
  }
  onCardClick(id : string){
    const url = 'products/' + id;
    this.router.navigate([url]);
  }
}
