import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { ErrorStatus, GetProductReponse, Product } from 'src/utils/interfaces';
import { CartService } from '../cart/cart.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ProductsService } from '../products/products.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDividerModule, MatIconModule, MatSnackBarModule],
})
export class ProductDetailComponent implements OnInit {

  product!: Product;
  productId: string = '';
  cant : number = 0;

  constructor(private router : Router, private productService : ProductsService, 
    private route: ActivatedRoute, private cartService :CartService,
    private _snackBar: MatSnackBar){
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productId = params['id'];
  });
  this.productService.getProduct(this.productId).subscribe({
    next: (response: GetProductReponse) => {
      this.product = response;
      this.cant = this.cartService.getCantOfItem(this.product.id);
    },
    error: (error: ErrorStatus) => {
      if(error.status == 400) this.goToPage('/products');
      if(error.status == 401) this.goToPage('/login');
    }
  });
  }

  goToPage(url: string){
    this.router.navigate([url]);
  }

  onDecrease(){
    this.cant = this.cartService.getCantOfItem(this.product.id);
    if(this.cant > 0){
      const cartItem = this.cartService.mapProductItemToCartItem(this.product);
      this.cartService.decreaseItem(cartItem);
      this.cant = this.cartService.getCantOfItem(this.product.id);
    }
  }

  onIncrease(){
    this.cant = this.cartService.getCantOfItem(this.product.id);
    const cartItem = this.cartService.mapProductItemToCartItem(this.product);
    if(this.cant < this.product.stock){
      this.cartService.addItem(cartItem);
    }
    else{
      this._snackBar.open('No more stock', 'Close', {
        duration: 2000,
      });
    }
    this.cant = this.cartService.getCantOfItem(this.product.id);
  }

  getColors(productItem: Product): string {
    return productItem.colors.map(color => color.name).join(', ');
  }
}

