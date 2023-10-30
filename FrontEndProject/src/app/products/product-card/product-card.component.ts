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
import { ProductItem } from 'src/utils/interfaces';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatCheckboxModule, FormsModule, MatFormFieldModule, MatInputModule, MatRippleModule, RouterModule, MatSnackBarModule],
})
export class ProductCardComponent implements OnInit{
  @Input()
  productItem!: ProductItem;
  centered = false;
  disabled = false;
  unbounded = false;
  constructor(private router:Router, private cartService : CartService, private _snackBar: MatSnackBar){
  }

  ngOnInit(): void {
  }
  addProductToCart(): void {
    const cartItem = this.cartService.mapProductItemToCartItem(this.productItem);
    this.cartService.addItem(cartItem);
  }

  onAddClick($event : Event){
    $event.stopPropagation()
    this.addProductToCart();
    this._snackBar.open("Product added to cart", "Close");
  }

  onCardClick(id: number){
    const url = 'products/' + id;
    this.router.navigate([url]);
  }

  getColors(productItem: ProductItem): string {
    return productItem.colors.map((color) => color).join(', ');
  }
}
