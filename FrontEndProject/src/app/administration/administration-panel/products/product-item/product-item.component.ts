import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatRippleModule } from '@angular/material/core';
import { ProductsService } from 'src/app/products/products.service';
import { Product } from 'src/utils/interfaces';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatRippleModule, MatSnackBarModule]
})
export class ProductItemComponent {
  @Input() product!: Product;
  centered = false;
  disabled = true;
  unbounded = false;

  @Output() refresh = new EventEmitter<void>();

  constructor(private productService : ProductsService, private _snackBar : MatSnackBar){}

  onDelete(){
    this.productService.deleteProduct(this.product.id).subscribe((response) => {
      this.refresh.emit();
      this._snackBar.open('Product Deleted', 'Close');
    });
  }

}

