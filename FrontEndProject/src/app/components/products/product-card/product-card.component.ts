import { Component, OnInit, Input } from '@angular/core';
import { MatButtonModule} from '@angular/material/button';
import { MatCardModule} from '@angular/material/card'; 
import { MatRippleModule} from '@angular/material/core';
import { MatInputModule} from '@angular/material/input';
import { MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule} from '@angular/forms';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { Router, RouterModule } from '@angular/router';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Product } from 'src/utils/interfaces';
import { CartService } from '../../cart/cart.service';

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
  @Input()
  cant!: number;
  centered = false;
  disabled = false;
  unbounded = false;
  constructor(private router:Router, private cartService : CartService, private _snackBar: MatSnackBar){
  }

  ngOnInit(): void {
    this.cant = this.cartService.getCantOfItem(this.productItem.id);
  }

  isBuyerInvalid() {
    if (this.cartService.user?.role === 2) {
      this._snackBar.open('Only buyers can add products to the cart', 'Close', {
        duration: 2000,
      });
      return true;
    } 
    return false;
  }

  onIncrease($event : Event){
    $event.stopPropagation()
    if (this.isBuyerInvalid()) return;
    this.cant = this.cartService.getCantOfItem(this.productItem.id);
    if(this.cant < this.productItem.stock){
      const cartItem = this.cartService.mapProductItemToCartItem(this.productItem);
      this.cartService.addItem(cartItem);
    }
    else{
      this._snackBar.open('No more stock', 'Close', {
        duration: 2000,
      });
    }
    this.cant = this.cartService.getCantOfItem(this.productItem.id);
  }
  
  onDecrease($event : Event){
    $event.stopPropagation()
    if (this.isBuyerInvalid()) return;
    this.cant = this.cartService.getCantOfItem(this.productItem.id);
    if(this.cant > 0){
      const cartItem = this.cartService.mapProductItemToCartItem(this.productItem);
      this.cartService.decreaseItem(cartItem);
      this.cant = this.cartService.getCantOfItem(this.productItem.id);
    }
    
  }

  onCardClick(id: string){
    const url = 'products/' + id;
    this.router.navigate([url]);
  }

  getColors(productItem: Product): string {
    return productItem.colors.map(color => color.name).join(', ');
  }
}
