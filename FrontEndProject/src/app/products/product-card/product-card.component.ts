import { Component, OnInit, Input } from '@angular/core';
import { MatButtonModule} from '@angular/material/button';
import { MatCardModule} from '@angular/material/card'; 
import { MatRippleModule} from '@angular/material/core';
import { MatInputModule} from '@angular/material/input';
import { MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule} from '@angular/forms';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { Router, RouterModule } from '@angular/router';
import { CartService } from 'src/app/cart/cart-service';
import { CartItem } from 'src/app/cart/cart-item';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Product } from 'src/utils/interfaces';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatCheckboxModule, FormsModule, MatFormFieldModule, MatInputModule, MatRippleModule, RouterModule, MatSnackBarModule],
})
export class ProductCardComponent implements OnInit{
  @Input()
  productItem!: Product;
  cant: number = 0;
  centered = false;
  disabled = false;
  unbounded = false;
  constructor(private router:Router, private cartService : CartService, private _snackBar: MatSnackBar){
  }

  ngOnInit(): void {
    this.cant = this.cartService.getCantOfItem(this.productItem.id);
  }

  onIncrease($event : Event){
    $event.stopPropagation()
    const cartItem = this.cartService.mapProductItemToCartItem(this.productItem);
    this.cartService.addItem(cartItem);
    this.cant = this.cartService.getCantOfItem(this.productItem.id);
  }
  
  onDecrease($event : Event){
    $event.stopPropagation()
    if(this.cant > 0){
      this.cant--;
      this.cartService.getCantOfItem(this.productItem.id);
    }
  }

  onCardClick(id: string){
    const url = 'products/' + id;
    this.router.navigate([url]);
  }

  getColors(productItem: Product): string {
    return productItem.colors.join(', ');
  }
}
