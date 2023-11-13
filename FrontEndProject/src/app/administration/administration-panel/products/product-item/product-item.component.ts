import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatRippleModule } from '@angular/material/core';
import { ProductsService } from 'src/app/products/products.service';
import { Product } from 'src/utils/interfaces';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ModifyProductComponent } from './modify-product/modify-product.component';

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

  @Output() refreshProducts = new EventEmitter<void>();

  constructor(private productService : ProductsService, private _snackBar : MatSnackBar, public dialog: MatDialog){}

  onDelete(){
    this.productService.deleteProduct(this.product.id).subscribe((response) => {
      this.refreshProducts.emit();
      this._snackBar.open('Product Deleted', 'Close', {duration: 2000});
    });
  }
  onModify(){
    const dialogRef = this.dialog.open(ModifyProductComponent, {data: this.product});
    dialogRef.afterClosed().subscribe(result => {
      this.refreshProducts.emit();
    });
  }

}

